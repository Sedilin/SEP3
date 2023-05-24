using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class MessageLogic : IMessageLogic
{
    private readonly IMessageDao _messageDao;

    public MessageLogic(IMessageDao messageDao)
    {
        _messageDao = messageDao;
    }
    public Task<bool> ArchiveMessage(MessageDto? dto)
    {
       return _messageDao.ArchiveMessage(dto);
    }

    public Task<List<MessageDto>> ShowMessages(int loggedUserId, int otherUserId)
    {
        return _messageDao.ShowMessages(loggedUserId, otherUserId);
    }

    public Task<List<User>> GetConversations(int loggedUserId)
    {
        return _messageDao.GetConversations(loggedUserId);
    }

    public Task<bool> DeleteConversation(int loggedUserId, int otherUserId)
    {
        return _messageDao.DeleteConversation(loggedUserId, otherUserId);
    }
}