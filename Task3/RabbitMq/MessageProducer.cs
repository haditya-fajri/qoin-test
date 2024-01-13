using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Task3.RabbitMq;

public class MessageProducer:IMessageProducer
{
    public void SendMessage<T>(T message)
    {
        const string queueName = "test01Queue";
        var factory = new ConnectionFactory { HostName = "localhost" };
        var connection = factory.CreateConnection();
        
        using var channel = connection.CreateChannel();
        channel.QueueDeclare(queueName,exclusive:false);
        
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange:"", routingKey:queueName,body:body );
    }
}