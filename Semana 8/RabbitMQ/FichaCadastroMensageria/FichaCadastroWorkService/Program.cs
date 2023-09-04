using FichaCadastroRabbitMQ;
using FichaCadastroRabbitMQ.Factory;
using FichaCadastroRabbitMQ.Interfaces;
using FichaCadastroWorkService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>()
                .AddSingleton<IFactoryConnectionRabbitMQ, FactoryConnectionRabbitMQ>()
                .AddSingleton<IMessageRabbitMQ, MessageRabbitMQ>(); ;
    })
    .Build();

host.Run();
