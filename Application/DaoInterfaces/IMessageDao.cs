using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface IMessageDao
{
    Task<bool> ArchiveMessage(MessageDto dto);
}