using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Task2.Models;

namespace Task2;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    
    private readonly IConfiguration _configuration;
    
    private IConnection _connection;
    private IChannel _channel;
    private const string QueueName = "test01Queue";

    public Worker(ILogger<Worker> logger,IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    private void InitRabbitMq()
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
            DispatchConsumersAsync = true
        };
        
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateChannel();
        _channel.QueueDeclare(QueueName,exclusive:false);
        
        _logger.LogInformation($"Queue has been initiate");
    }
    
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        InitRabbitMq();
        return base.StartAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Dispose();
        base.Dispose();
    }


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();   
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (model, eventArgs) =>
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            Console.WriteLine(message);
            _logger.LogInformation($"Processing msg: '{message}'.");
            try
            {
                var data = JsonConvert.DeserializeObject<QueueDto<Test01Dto>>(message);
                if (data?.Command != null) MessageHandling(data.Command, data.Data);
                await _channel.BasicAckAsync(eventArgs.DeliveryTag, false);

            }
            catch (AlreadyClosedException)
            {
                _logger.LogInformation("RabbitMQ is closed!");
            }
            catch (Exception e)
            {
                _logger.LogError(default, e, e.Message);
            }

        };

        await  _channel.BasicConsumeAsync(queue: QueueName, autoAck: false, consumer: consumer);
        
        await Task.CompletedTask;
    }

    

    private void MessageHandling(string command, Test01Dto data)
    {
        var context = new QoinContext(_configuration);
        switch (command)
        {
            case "create":
                var test01 = new Test01
                {
                    Name = data.Name,
                    Status = data.Status,
                    Created = DateTime.Now
                };
                context.Test01s?.Add(test01);
                break;
            case "update":
            {
                var existingData = context.Test01s?.Find(data.Id);
                if (existingData != null) 
                {
                    existingData.Name = data.Name;
                    existingData.Status = data.Status;
                    existingData.Updated = DateTime.Now;
                }

                break;
            }
            case "delete":
            {
                var existingData = context.Test01s?.Find(data.Id);
                if (existingData != null)
                    context.Test01s?.Remove(existingData);
                break;
            }
        }

        context.SaveChanges();
    }
}