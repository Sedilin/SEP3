using Domain.DTOs;

namespace Application.DaoInterfaces;

public interface ICourseDao
{
    Task<IEnumerable<string>> GetAsync(SearchCourseParameterDto? parameter);
}