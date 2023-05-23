using System.Net.Http.Json;
using System.Text.Json;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace DbConnection.DAOs;

public class UserDatabaseDao : IUserDao
{
    private readonly HttpClient client;

    public UserDatabaseDao(HttpClient client)
    {
        this.client = client;
    }

    public async Task<User> CreateAsync(User user)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/user", user);

        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User userBack = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return userBack;
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

    public async Task<User> PostNewTutorAsync(UserToTutorDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/user/newTutor", dto);

        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return user;
    }

    public async Task<TutorInformationDto> GetTutorAsync(SearchUserParametersDto parameters)
    {
        string uri = $"/user/tutor?userName={parameters.UsernameContains}";
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        TutorInformationDto user = JsonSerializer.Deserialize<TutorInformationDto>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return user;
    }

    public async Task<User?> GetTutorByUsername(SearchUserParametersDto dto)
    {
        string uri = $"/user/tutorByUsername?userName={dto.UsernameContains}";
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        User? user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }

    public async Task<User> UpdateProfile(TutorInformationDto dto)
    {
        string uri = "/user/profile";
        HttpResponseMessage response = await client.PutAsJsonAsync(uri, dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }
        
        User user = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return user;
    }
}