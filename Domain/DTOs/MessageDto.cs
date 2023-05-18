using Domain.Model;

namespace Domain.DTOs;

public class MessageDto
{
    public User Sender { get; }
    public User Receiver { get; }
    public string Message { get; }

    public MessageDto(User sender, User receiver, string message)
    {
        Sender = sender;
        Receiver = receiver;
        Message = message;
    }
}