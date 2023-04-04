using Domain.DTOs;
using Domain.Model;


namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
    Task<User> ValidateUser(string userName, string password);
}