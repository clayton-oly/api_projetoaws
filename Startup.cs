using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SocialApp.Data;
using System.Text.Json.Serialization;

namespace SocialApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure o serviço de banco de dados usando Entity Framework Core com o provedor PostgreSQL
            services.AddDbContext<SocialAppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"), npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions?.SetPostgresVersion(new Version(9, 6));
                }));

            services.AddScoped<TemaRepository>();
            services.AddScoped<PostagemRepository>();
            services.AddScoped<UsuarioRepository>();

            // Configurar o Swagger
            services.AddSwaggerGen(c =>
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

            // Configurar opções para serialização JSON
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Adicionando o Swagger middleware aqui para estar disponível em todos os ambientes
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto AWS API V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
