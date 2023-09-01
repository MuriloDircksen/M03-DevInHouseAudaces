using FichaCadastroRabbitMQ.Model;
using RabbitMQ.Client;


namespace FichaCadastroRabbitMQ.Interfaces
{
    public interface IMessageRabbitMQ
    {
        ConfigureRabbitMQ ConfigureRabbitMQ { get; set; }

        IModel ConfigureVirtualHost();
        void ExchangeDeclare();
        void QueueDeclare();
    }
}
