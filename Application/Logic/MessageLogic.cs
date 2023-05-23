using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;

namespace Application.Logic;

public class MessageLogic : IMessageLogic
{
    private readonly IMessageDao _messageDao;

    public MessageLogic(IMessageDao messageDao)
    {
        _messageDao = messageDao;
    }
    public Task<bool> ArchiveMessage(MessageDto dto)
    {
       return _messageDao.ArchiveMessage(dto);
    }

    public Task<IEnumerable<MessageDto>> ShowMessages(int loggedUserId, int otherUserId)
    {
        return _messageDao.ShowMessages(loggedUserId, otherUserId);
    }
}