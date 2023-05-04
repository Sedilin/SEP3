
using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto? searchParameters);
}