using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface IMessageLogic
{
    Task<bool> ArchiveMessage(MessageDto dto);
    Task<IEnumerable<MessageDto>> ShowMessages(int loggedUserId, int otherUserId);
}