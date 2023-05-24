using System.Net.Http.Json;
using System.Text.Json;
using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;

namespace DbConnection.DAOs;

public class MessageDao : IMessageDao
{
    private readonly HttpClient client;

    public MessageDao(HttpClient client)
    {
        this.client = client;
    }

    public async Task<bool> ArchiveMessage(MessageDto? dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/archive", dto);

        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        bool responded = JsonSerializer.Deserialize<bool>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return responded;
    }

    public async Task<List<MessageDto>> ShowMessages(int loggedUserId, int otherUserId)
    {
        HttpResponseMessage response =
            await client.GetAsync($"/archive?LoggedUserId={loggedUserId}&OtherUserId={otherUserId}");
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        List<MessageDto> dtos = JsonSerializer.Deserialize<List<MessageDto>>(result)!;
        return dtos;
    }

    public async Task<List<User>> GetConversations(int loggedUserId)
    {
        HttpResponseMessage response =
            await client.GetAsync($"/archive/getUsers?LoggedUserId={loggedUserId}");
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        List<User> users = JsonSerializer.Deserialize<List<User>>(result)!;
        return users;
    }

    public async Task<bool> DeleteConversation(int loggedUserId, int otherUserId)
    {
        HttpResponseMessage response =
            await client.DeleteAsync($"/archive?LoggedUserId={loggedUserId}&OtherUserId={otherUserId}");
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        bool deletedUser = JsonSerializer.Deserialize<bool>(result);
        return deletedUser;
    }
}