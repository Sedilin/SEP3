using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IMessageDao
{
    Task<bool> ArchiveMessage(MessageDto? dto);
    Task<List<MessageDto>> ShowMessages(int loggedUserId, int otherUserId);
    Task<List<User>> GetConversations(int loggedUserId);
    Task<bool> DeleteConversation(int loggedUserId, int otherUserId);
}