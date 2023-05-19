using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface ICourseDao
{
    Task<IEnumerable<UserToTutorDto>> GetTutorByCourse(string course);
    Task<IEnumerable<string>> GetAsync(SearchCourseParameterDto? parameter);
}