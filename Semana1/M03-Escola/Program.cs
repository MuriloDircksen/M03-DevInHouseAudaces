
using Microsoft.OpenApi.Models;
using M03_Escola.DataBase;
using Microsoft.EntityFrameworkCore;
using M03_Escola.Interfaces.Services;
using M03_Escola.Services;
using M03_Escola.Interfaces.Repositories;
using M03_Escola.DataBase.Repositories;

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
            });
            builder.Services.AddDbContext<EscolaDBContexto>(options =>
                                options.UseSqlServer(
                                    builder.Configuration.GetConnectionString("ServerConnection")));
            builder.Services.AddDbContext<EscolaDBContexto>();

            builder.Services.AddScoped<IAlunoService, AlunoService>();
            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<ITurmaRepository, TurmaRepository>();

            builder.Services.AddMemoryCache();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Escola.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}