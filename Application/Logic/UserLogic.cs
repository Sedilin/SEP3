using Application.DaoInterfaces;
using Application.LogicInterfaces;

using Domain.DTOs;
using Domain.Model;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User userToCreate = new User
        {
            UserName = dto.UserName,
            Password = dto.Password,
            UserType = dto.UserType
        };
        
        User createdUser = await userDao.CreateAsync(userToCreate);
    
        return createdUser;
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto? searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        SearchUserParametersDto parameters = new(username);
        IEnumerable<User> users = await GetAsync(parameters);
        
        User? existingUser = users.FirstOrDefault(u => 
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }
}