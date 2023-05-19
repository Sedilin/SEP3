using Domain.DTOs;

namespace Application.LogicInterfaces;

public interface ICourseLogic
{
    Task<IEnumerable<string>> GetAsync(SearchCourseParameterDto? parameter);
    Task<IEnumerable<UserToTutorDto>> GetTutorByCourse(string course);
}