namespace HttpClients.ClientInterfaces;

public interface ICourseService
{
    Task<IEnumerable<string>> GetCourses(string? course);
}