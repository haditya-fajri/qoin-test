namespace Task3.RabbitMq;

public interface IMessageProducer
{
    void SendMessage<T>(T message);
}