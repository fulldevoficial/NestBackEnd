using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Data.Services
{
    public class MigrationService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MigrationService> _logger;

        public MigrationService(IServiceProvider serviceProvider, ILogger<MigrationService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task AplicarMigration(int versaoDesejada)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                _logger.LogInformation("[Init] - Iniciando aplicação da Migration");
                _logger.LogInformation("[001] - Versão desejada: {VersaoDesejada}", versaoDesejada);
                _logger.LogInformation("[002] - Aplicando migrations até a versão {VersaoDesejada}", versaoDesejada);
                var versaoAtual = await ObterVersaoAtual(context);

                if (versaoDesejada <= versaoAtual)
                    return;

                await context.Database.MigrateAsync();
                await GarantirTabelaVersao(context);

                versaoAtual = await ObterVersaoAtual(context);

                _logger.LogInformation("[003] - Versão atual do banco: {VersaoAtual}", versaoAtual);

                if (versaoDesejada <= versaoAtual)
                {
                    _logger.LogInformation("[004] - Banco de dados já está na versão {VersaoAtual}. Nenhuma migration necessária.", versaoAtual);
                    return;
                }

                await SalvarVersao(context, versaoDesejada);

                _logger.LogInformation("[FIM] - Migrations aplicadas com sucesso. Versão atualizada para {VersaoDesejada}.", versaoDesejada);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao aplicar migrations.");
                throw;
            }
        }

        private async Task GarantirTabelaVersao(AppDbContext context)
        {
            try
            {
                await context.Database.ExecuteSqlRawAsync("CREATE SCHEMA IF NOT EXISTS dbo");

                await context.Database.ExecuteSqlRawAsync(@"
                    CREATE TABLE IF NOT EXISTS dbo.schemaversion (
                        id SERIAL PRIMARY KEY,
                        versao INTEGER NOT NULL,
                        dataatualizacao TIMESTAMP WITH TIME ZONE NOT NULL
                    )");
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Erro ao criar schema/tabela schemaversion.");
            }
        }

        private async Task<int> ObterVersaoAtual(AppDbContext context)
        {
            try
            {
                var connection = context.Database.GetDbConnection();
                await using var command = connection.CreateCommand();
                command.CommandText = "SELECT versao FROM dbo.schemaversion ORDER BY dataatualizacao DESC LIMIT 1";

                if (connection.State != System.Data.ConnectionState.Open)
                    await connection.OpenAsync();

                var result = await command.ExecuteScalarAsync();
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch
            {
                return 0;
            }
        }

        private async Task SalvarVersao(AppDbContext context, int versao)
        {
            var connection = context.Database.GetDbConnection();

            if (connection.State != System.Data.ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = $"INSERT INTO dbo.schemaversion (versao, dataatualizacao) VALUES ({versao}, '{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}')";
            await command.ExecuteNonQueryAsync();
        }
    }
}
