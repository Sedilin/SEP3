using System.Net.Http.Json;
using System.Text.Json;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace FileData.DAOs;

public class UserFileDao : IUserDao
{
    private readonly HttpClient client;

    public UserFileDao(HttpClient client)
    {
        this.client = client;
    }

    public async Task<User> CreateAsync(User user)
    {
        int userId = 1;
        IEnumerable<User> users = await GetAsync();
        
        if (users.Any())
        {
            userId = users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;
        
        HttpResponseMessage response = await client.PostAsJsonAsync("/user", user);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        return user;

    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        string uri = $"/user/userName?userName={userName}";
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<IEnumerable<User>> GetAsync(SearchUserParametersDto? searchParameters = null)
    {
        HttpResponseMessage response = await client.GetAsync("/user");
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        IEnumerable<User> users = JsonSerializer.Deserialize<IEnumerable<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;

    }
}