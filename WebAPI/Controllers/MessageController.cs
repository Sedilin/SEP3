using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Domain.RabbitMQChat;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageLogic _messageLogic;

    public MessageController(IMessageLogic messageLogic)
    {
        _messageLogic = messageLogic;
    }

    [HttpPost("send")]
    public async Task<ActionResult<MessageDto>> SendMessage(MessageDto message)
    {
        try
        {
            IConnection con = RabbitMQConnection.Instance.GetConnection();
            bool flag = RabbitMQConnection.Instance.send(con, message);
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
            IConnection con = RabbitMQConnection.Instance.GetConnection();
            MessageDto? dto = RabbitMQConnection.Instance.receive(con, userName);


            if (dto != null)
            {
               await _messageLogic.ArchiveMessage(dto);
            }

            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{LoggedUserId:int}/{OtherUserId:int}")]
    public async Task<ActionResult<List<MessageDto>>> ShowMessages([FromRoute] int loggedUserId, int otherUserId)
    {
        try
        {
           List<MessageDto> dtos = await _messageLogic.ShowMessages(loggedUserId, otherUserId);
            return Ok(dtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("getConversations/{LoggedUserId:int}")]
    public async Task<ActionResult<List<User>>> GetConversations([FromRoute] int loggedUserId)
    {
        try
        {
            List<User> users = await _messageLogic.GetConversations(loggedUserId);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{LoggedUserId:int}/{OtherUserId:int}")]
    public async Task<ActionResult<bool>> DeleteConversation([FromRoute] int loggedUserId, int otherUserId)
    {
        try
        {
            bool deletedUser = await _messageLogic.DeleteConversation(loggedUserId, otherUserId);
            return Ok(deletedUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}