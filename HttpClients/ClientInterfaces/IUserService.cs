using Domain.DTOs;
using Domain.Model;


namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<User> Create(UserCreationDto dto);
    Task<IEnumerable<User>> GetUsers(string? usernameContains = null);
    Task<User> BecomeTutor(UserToTutorDto dto);
    Task<TutorInformationDto> GetTutorAsync(string userName);
    Task<User> SearchTutorByUsername(string userName);
    Task<User> UpdateProfile(TutorInformationDto dto);

    Task DeleteAccount(int userId);
}