using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IMessageLogic
{
    Task<bool> ArchiveMessage(MessageDto? dto);
    Task<List<MessageDto>> ShowMessages(int loggedUserId, int otherUserId);
    Task<List<User>> GetConversations(int loggedUserId);
    Task<bool> DeleteConversation(int loggedUserId, int otherUserId);
}