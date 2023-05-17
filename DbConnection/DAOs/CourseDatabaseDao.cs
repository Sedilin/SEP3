using System.Text.Json;
using Application.DaoInterfaces;
using Domain.DTOs;

namespace DbConnection.DAOs;

public class CourseDatabaseDao : ICourseDao
{
    private readonly HttpClient client;
    
    public CourseDatabaseDao(HttpClient client)
    {
        this.client = client;
    }

    public async Task<IEnumerable<string>> GetAsync(SearchCourseParameterDto? parameter)
    {
        string uri = "/course";
        if (!string.IsNullOrEmpty(parameter.CourseContains))
        {
            uri += $"?course={parameter.CourseContains}";
        }
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<string> courses = JsonSerializer.Deserialize<IEnumerable<string>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return courses;
    }
}