using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SocialApp.Data;
using SocialApp.Interfaces;
using SocialApp.Services;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<SocialAppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependência
builder.Services.AddScoped<ITemaRepository, TemaRepository>();
builder.Services.AddScoped<IPostagemRepository, PostagemRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITemaService, TemaService>();
builder.Services.AddScoped<IPostagemService, PostagemService>();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Projeto AWS API",
        Version = "v1",
        Description = "Esta é a documentação da minha API",
        Contact = new OpenApiContact
        {
            Name = "Clayton Rocha",
            Email = "clayton.will@gmail.com",
            Url = new Uri("https://github.com/clayton-oly")
        }
    });
});

// Controllers + JSON config
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<SocialAppDbContext>();
        context.Database.Migrate();
    }
}


// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto SocialApp API V1");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
