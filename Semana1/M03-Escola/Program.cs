
using Microsoft.OpenApi.Models;
using M03_Escola.DataBase;
using Microsoft.EntityFrameworkCore;
using M03_Escola.Interfaces.Services;
using M03_Escola.Services;
using M03_Escola.Interfaces.Repositories;
using M03_Escola.DataBase.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using M03_Escola.Config;

namespace M03_Escola
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Escola.API", Version = "v1" });
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                              Escreva 'Bearer' [espaço] e o token gerado no login na caixa abaixo.
                                              Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                                          {
                                            {
                                              new OpenApiSecurityScheme
                                              {
                                                Reference = new OpenApiReference
                                                  {
                                                    Type = ReferenceType.SecurityScheme,
                                                    Id = JwtBearerDefaults.AuthenticationScheme
                                                  },
                                                },
                                                new List<string>()
                                              }
                                            });             

        });

            //injeção de interfaces e classes para a transação de dados
            //Services
            builder.Services.AddScoped<IAlunoService, AlunoService>();
            builder.Services.AddScoped<IBoletimService, BoletimService>();
            builder.Services.AddScoped<IMateriaService, MateriaService>();
            builder.Services.AddScoped<INotasMateriaService, NotasMateriaService>();

            //repositorios
            builder.Services.AddDbContext<EscolaDBContexto>(options =>
                                options.UseSqlServer(
                                    builder.Configuration.GetConnectionString("ServerConnection")));
            builder.Services.AddDbContext<EscolaDBContexto>();

            
            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();
            builder.Services.AddScoped<IBoletimRepository, BoletimRepository>();
            builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
            builder.Services.AddScoped<INotasMateriaRepository, NotasMateriaRepository>();

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Escola.API v1");
                    
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ErrorMiddleware>(); //habilitado uso do middleware criado


            app.MapControllers();

            app.Run();
        }
    }
}