using System.Text.Json;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class CourseHttpClient : ICourseService
{
    private readonly HttpClient client;
    
    
    public async Task<IEnumerable<string>> GetCourses(string? course)
    {
        string uri = "/course";
        if (!string.IsNullOrEmpty(course))
        {
            uri += $"?course={course}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable <string> courses = JsonSerializer.Deserialize<IEnumerable<string>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return courses;
    }
}