namespace HttpClients.ClientInterfaces;

public interface IMessageService
{
    Task<string> Receive(string username);
    Task<string> Send(string message);
}