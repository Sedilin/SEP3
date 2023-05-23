using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IMessageService
{
    Task<MessageDto> Receive(string username);
    Task<MessageDto> Send(MessageDto message);
    Task<IEnumerable<MessageDto>> ShowMessages(int loggedUserId, int otherUserId);
}