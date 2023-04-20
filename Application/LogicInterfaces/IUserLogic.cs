
using Domain.DTOs;
using Domain.Model;

namespace Application.LogicInterfaces;

public interface IUserLogic
{ 
    Task<User> CreateAsync(UserCreationDto userToCreate);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDto? searchParameters);
}