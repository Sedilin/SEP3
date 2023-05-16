
using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{ 
    Task<User> CreateAsync(UserCreationDto userToCreate);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto? searchParameters);
    Task<User> ValidateUser(string username, string password);
    Task<User> PostNewTutorAsync(UserToTutorDto dto);
    Task<string> GetDescriptionAsync(SearchUserParametersDto parameters);
    Task<User?> GetTutorByUsername(SearchUserParametersDto dto);
}