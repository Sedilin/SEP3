using Domain.RabbitMQChat;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{

    [HttpGet("send")]
    public string SendMessage(string message)
    {
        RabbitMQConnection obj = new RabbitMQConnection();
        IConnection con = obj.GetConnection();
        bool flag = obj.send(con, message, "Gabi");
        return message;
    }
    
    [HttpGet("receive")]
    public async Task<ActionResult<string>> ReceiveMessage(string userName)
    {
        try
        {
            RabbitMQConnection obj = new RabbitMQConnection();
            IConnection con = obj.GetConnection();
            string userqueue = userName;
            string message = obj.receive(con, userqueue);
            return Ok(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}