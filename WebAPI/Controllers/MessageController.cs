using Domain.DTOs;
using Domain.RabbitMQChat;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{

    [HttpPost("send")]
    public async Task<ActionResult<MessageDto>> SendMessage(MessageDto message)
    {
        try
        {
            RabbitMQConnection obj = new RabbitMQConnection();
            IConnection con = obj.GetConnection();
            bool flag = obj.send(con, message);
            return Ok(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("receive")]
    public async Task<ActionResult<MessageDto>> ReceiveMessage(string userName)
    {
        try
        {
            RabbitMQConnection obj = new RabbitMQConnection();
            IConnection con = obj.GetConnection();
            string userqueue = userName;
            MessageDto message = obj.receive(con, userqueue);
            return Ok(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}