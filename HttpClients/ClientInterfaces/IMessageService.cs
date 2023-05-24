using Domain.DTOs;
using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IMessageService
{
    Task<MessageDto> Receive(string username);
    Task<MessageDto> Send(MessageDto message);
    Task<List<MessageDto>> ShowMessages(int loggedUserId, int otherUserId);
    Task<List<User>> GetConversations(int loggedUserId);
    Task<bool> DeleteConversation(int loggedUserId, int otherUserId);
}