using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IMessageLogic
{
    Task<bool> ArchiveMessage(MessageDto dto);
    Task<List<MessageDto>> ShowMessages(int loggedUserId, int otherUserId);
}