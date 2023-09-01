using FichaCadastroRabbitMQ;
using FichaCadastroRabbitMQ.Factory;
using FichaCadastroRabbitMQ.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IFactoryConnectionRabbitMQ, FactoryConnectionRabbitMQ>();
builder.Services.AddScoped<IMessageRabbitMQ, MessageRabbitMQ>();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true; //altera formato no routing que era a url, alterando tudo para lowercase
    options.LowercaseQueryStrings = true; // importante por padronização, quanto a nome da controller que é escrito maisculo
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();