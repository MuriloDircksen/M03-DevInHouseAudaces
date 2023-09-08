using FichaCadastroRabbitMQ.Interfaces;
using FichaCadastroRabbitMQ.Model;
using System.Text;

namespace FichaCadastroWorkService
{
    public class Worker : BackgroundService
    { //precisa  criar o virtualhost no rabbit para ele conseguir se conectar
        private readonly ILogger<Worker> _logger;
        private readonly IMessageRabbitMQ _messageRabbitMQ;

        public Worker(ILogger<Worker> logger, IMessageRabbitMQ messageRabbitMQ)
        {
            _logger = logger;
            _messageRabbitMQ = messageRabbitMQ;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now); //cria mensagem do evento

            _messageRabbitMQ.ConfigureRabbitMQ = new ConfigureRabbitMQ(
                VirtualHost: "ficha",
                Exchange: "ficha-exchange-topic",
                Type: "topic",
                Queue: "ficha-cadastro-novo-queue-topic",
                RouteKey: "ficha-cadastro.novo-routeKey-topic"
            );

            _messageRabbitMQ.ConfigureVirtualHost();
            
            var basicConsumer = _messageRabbitMQ.InstanciarEventingBasicConsumer();

            try
            { //manipular o evento de consumo de mensagens, cada vez que recebe uma mensagem esse codigo e ativado
                basicConsumer.Received += (model, basicDeliverEventArgs) =>
                {
                    basicDeliverEventArgs.RoutingKey = _messageRabbitMQ.ConfigureRabbitMQ.RouteKey;
                    basicDeliverEventArgs.Exchange = _messageRabbitMQ.ConfigureRabbitMQ.Exchange;
                    var body = basicDeliverEventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"{message}");
                };

                _messageRabbitMQ.BasicConsume(basicConsumer);
            }
            catch (Exception)
            {
                //throw;
            }


            while (!stoppingToken.IsCancellationRequested) 
                //verifica se o token de cancelamento n�o foi solicitado para cancelar excecu��o do servi�o
            {
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}