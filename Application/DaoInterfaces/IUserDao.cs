
using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto? searchParameters);
    Task<User> PostNewTutorAsync(UserToTutorDto dto);
    Task<TutorInformationDto> GetTutorAsync(SearchUserParametersDto parameters);
    Task<User?> GetTutorByUsername(SearchUserParametersDto dto);
    Task<User> UpdateProfile(TutorInformationDto dto);
    Task RemoveAccount(int userId);
}