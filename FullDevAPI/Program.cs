using Application.Ports.Input;
using Application.Ports.Output;
using Application.UseCases;
using Application.Implementations;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Extensions;
using Services.Interfaces.Login;
using Services.Interfaces.Usuario;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var chaveJwt = builder.Configuration["Jwt:Key"];
var emissor = builder.Configuration["Jwt:Issuer"];

if (string.IsNullOrWhiteSpace(chaveJwt) || string.IsNullOrWhiteSpace(emissor))
    throw new Exception("JWT Key ou Issuer não estão configurados corretamente.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = emissor,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveJwt))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServices();
builder.Services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddScoped<IUsuarioUseCase, UsuarioUseCase>();
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
