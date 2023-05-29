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
        if (existing?.UserName != null)
        {
            throw new Exception("Username already taken!");
        }

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
            u.UserName.Equals(username));

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

    public Task<User> PostNewTutorAsync(UserToTutorDto dto)
    {
        if (dto.Description == null)
        {
            throw new Exception("Description cannot be empty!");
        }
        
        return userDao.PostNewTutorAsync(dto);
    }

    public Task<TutorInformationDto> GetTutorAsync(SearchUserParametersDto parameters)
    {
        return userDao.GetTutorAsync(parameters);
    }

    public async Task<User?> GetTutorByUsername(SearchUserParametersDto dto)
    {
        User? user = await userDao.GetTutorByUsername(dto);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return user;
    }

    public async Task<User> UpdateUser(TutorInformationDto dto)
    {
        User user = await userDao.UpdateProfile(dto);
        return user;
    }

    public async Task RemoveAccount(int userId)
    {
        await userDao.RemoveAccount(userId);
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;
        string password = userToCreate.Password;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        
        if (password.Length < 4)
            throw new Exception("Password must be at least 4 characters!");
    }
}