using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Model;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class CourseHttpClient : ICourseService
{
    private readonly HttpClient client;

    public CourseHttpClient(HttpClient client)
    {
        this.client = client;
    }
    public async Task<IEnumerable<string>> GetCourses(string? course)
    {
        string uri = "/Course";
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

    public async Task<IEnumerable<UserToTutorDto>> GetTutorByCourse(string course)
    {
        string uri = $"/Course/tutorByCourse?course={course}";
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<UserToTutorDto> users = JsonSerializer.Deserialize<IEnumerable<UserToTutorDto>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
}