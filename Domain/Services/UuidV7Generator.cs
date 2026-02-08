using Domain.Common;

namespace Domain.Services
{
    /// <summary>
    /// Implementação de gerador de UUIDv7
    /// UUIDv7 usa timestamp Unix em milissegundos nos primeiros 48 bits, permitindo:
    /// - Ordenação natural por tempo de criação
    /// - Recuperação da data de criação
    /// - Melhor performance em índices de banco de dados
    /// </summary>
    public class UuidV7Generator : IIdentityGenerator
    {
        private static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        private static readonly Random Random = new Random();
        private static readonly object Lock = new object();

        public Guid Generate()
        {
            lock (Lock)
            {
                var timestamp = DateTimeOffset.UtcNow;
                var unixTimeMs = (long)(timestamp - UnixEpoch).TotalMilliseconds;

                // Buffer de 16 bytes para o UUID
                var guidBytes = new byte[16];

                // Bytes 0-5: timestamp (48 bits / 6 bytes)
                guidBytes[0] = (byte)((unixTimeMs >> 40) & 0xFF);
                guidBytes[1] = (byte)((unixTimeMs >> 32) & 0xFF);
                guidBytes[2] = (byte)((unixTimeMs >> 24) & 0xFF);
                guidBytes[3] = (byte)((unixTimeMs >> 16) & 0xFF);
                guidBytes[4] = (byte)((unixTimeMs >> 8) & 0xFF);
                guidBytes[5] = (byte)(unixTimeMs & 0xFF);

                // Bytes 6-7: versão e bits aleatórios
                var randomBytes = new byte[10];
                Random.NextBytes(randomBytes);

                guidBytes[6] = (byte)((randomBytes[0] & 0x0F) | 0x70); // Versão 7
                guidBytes[7] = randomBytes[1];

                // Bytes 8-15: variante e bits aleatórios
                guidBytes[8] = (byte)((randomBytes[2] & 0x3F) | 0x80); // Variante RFC 4122
                Array.Copy(randomBytes, 3, guidBytes, 9, 7);

                return new Guid(guidBytes);
            }
        }

        public DateTimeOffset ExtractTimestamp(Guid uuidv7)
        {
            var bytes = uuidv7.ToByteArray();

            // Extrai os primeiros 48 bits (6 bytes) que contêm o timestamp
            long unixTimeMs = 0;
            unixTimeMs |= (long)bytes[0] << 40;
            unixTimeMs |= (long)bytes[1] << 32;
            unixTimeMs |= (long)bytes[2] << 24;
            unixTimeMs |= (long)bytes[3] << 16;
            unixTimeMs |= (long)bytes[4] << 8;
            unixTimeMs |= bytes[5];

            return UnixEpoch.AddMilliseconds(unixTimeMs);
        }
    }
}
