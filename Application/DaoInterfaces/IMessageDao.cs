using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface IMessageDao
{
    Task<bool> ArchiveMessage(MessageDto dto);
    Task<List<MessageDto>> ShowMessages(int loggedUserId, int otherUserId);
}