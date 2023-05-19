using Domain.DTOs;

namespace HttpClients.ClientInterfaces;

public interface ICourseService
{
    Task<IEnumerable<string>> GetCourses(string? course);
    Task<IEnumerable<UserToTutorDto>> GetTutorByCourse(string course);
}