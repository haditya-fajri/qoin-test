using Microsoft.AspNetCore.Mvc;
using Task3.Models;
using Task3.RabbitMq;

namespace Task3.Controllers;

[ApiController]
[Route("api/test-01")]
public class Test01Controller:ControllerBase
{
    private readonly IMessageProducer _messageProducer;
    public Test01Controller(IMessageProducer messageProducer)
    {
        _messageProducer = messageProducer;
    }

    [HttpPost]
    public IActionResult Add([FromBody]Test01Dto model)
    {
        QueueDto<Test01Dto> queueDto = new()
        {
            Command = "create",
            Data = model
        };
        
        _messageProducer.SendMessage(queueDto);
        return Ok();
    }
    
    [HttpPut]
    public IActionResult Update(int id,[FromBody]Test01Dto model)
    {
        model.Id = id;
        QueueDto<Test01Dto> queueDto = new()
        {
            Command = "update",
            Data = model
        };
        
        _messageProducer.SendMessage(queueDto);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Remove(int id)
    {
        QueueDto<Test01Dto> queueDto = new()
        {
            Command = "delete",
            Data = new Test01Dto
            {
                Id = id
            }
        };
        
        _messageProducer.SendMessage(queueDto);
        return Ok();
    }
}