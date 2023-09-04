
using FichaCadastroRabbitMQ.Interfaces;
using FichaCadastroRabbitMQ.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FichaCadastroRabbitMQ
{
    public class MessageRabbitMQ : IMessageRabbitMQ
    {
        private IModel _channel;
        private readonly IFactoryConnectionRabbitMQ _factoryConnectionRabbitMQ;

        public ConfigureRabbitMQ ConfigureRabbitMQ { get; set; }

        public MessageRabbitMQ(IFactoryConnectionRabbitMQ factoryConnectionRabbitMQ)
        {
            _factoryConnectionRabbitMQ = factoryConnectionRabbitMQ;
        }

        public IModel ConfigureVirtualHost()
        {
            _channel = _factoryConnectionRabbitMQ.CriarConexao(ConfigureRabbitMQ.VirtualHost);
            return _channel;
        }

        public void ExchangeDeclare()
        {//cria uma conexão entre o emissor ou produtor de mensagens com uma fila ou queues ou outra exchanges
            _channel.ExchangeDeclare(ConfigureRabbitMQ.Exchange, ConfigureRabbitMQ.Type, ConfigureRabbitMQ.Durable, ConfigureRabbitMQ.AutoDelete);
        }

        public void QueueDeclare()
        {//declarar uma fila com suas configurações e propriedades
            _channel.QueueDeclare(ConfigureRabbitMQ.Queue, durable: ConfigureRabbitMQ.Durable, false, ConfigureRabbitMQ.AutoDelete);
        }

        public void QueueBind()
        {
            _channel.QueueBind(ConfigureRabbitMQ.Queue, ConfigureRabbitMQ.Exchange, ConfigureRabbitMQ.RouteKey);
        }

        public void BasicPublish()
        {//vincula uma fila com uma exchange
            _channel.BasicPublish(exchange: ConfigureRabbitMQ.Exchange, routingKey: ConfigureRabbitMQ.RouteKey, body: ConfigureRabbitMQ.Message);
        }

        public EventingBasicConsumer InstanciarEventingBasicConsumer()
        { // instancia a classe orientada a eventos para  consummir as mensagens no rabbit
            return new EventingBasicConsumer(_channel);
        }

        public void BasicConsume(IBasicConsumer consumer)
        {//permite um cliente se inscrever numa fila e começar a consuir as mensagens
            _channel.BasicConsume(ConfigureRabbitMQ.Queue, autoAck: true, consumer);
        }
    }
}