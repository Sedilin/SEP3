using Domain.DTOs;
using Shared;

namespace Application.LogicInterfaces;

public interface IUserLogic
{ 
    Task<User> CreateAsync(UserCreationDto userToCreate);
}