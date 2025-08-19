using Microsoft.EntityFrameworkCore;
using SocialApp.Data;
using SocialApp.Interfaces;
using SocialApp.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configuração do banco de dados
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SocialAppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Injeção de dependência
builder.Services.AddScoped<ITemaRepository, TemaRepository>();
builder.Services.AddScoped<IPostagemRepository, PostagemRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITemaService, TemaService>();
builder.Services.AddScoped<IPostagemService, PostagemService>();


// Controllers + JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true; // Opcional: deixa JSON mais legível
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

//usar autenticação futuramente:
// app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
