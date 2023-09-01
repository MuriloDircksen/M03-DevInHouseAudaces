using RabbitMQ.Client;


namespace FichaCadastroRabbitMQ.Interfaces
{
    public interface IFactoryConnectionRabbitMQ
    {
        IModel CriarConexao(string virtualHost);
    }
}
