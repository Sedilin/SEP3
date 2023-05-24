using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Model;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class MessageHttpClient : IMessageService
{
    private readonly HttpClient client;

    public MessageHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<MessageDto> Receive(string userName)
    {
        string uri = $"/Message/receive?userName={userName}";
        HttpResponseMessage response = await client.GetAsync(uri);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        if (!String.IsNullOrEmpty(result))
        {
            MessageDto message = JsonSerializer.Deserialize<MessageDto>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            return message;
        }

        return null;
    }

    public async Task<MessageDto> Send(MessageDto message)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Message/send", message);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        MessageDto messageResult = JsonSerializer.Deserialize<MessageDto>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return messageResult;
    }
    
    
    public async Task<List<MessageDto>> ShowMessages(int loggedUserId, int otherUserId)
    {
        HttpResponseMessage response = await client.GetAsync($"/Message/{loggedUserId}/{otherUserId}");
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        List<MessageDto> dtos = JsonSerializer.Deserialize<List<MessageDto>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return dtos;
    }
    
    public async Task<List<User>> GetConversations(int loggedUserId)
    {
        HttpResponseMessage response = await client.GetAsync($"/Message/getConversations/{loggedUserId}");
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        List<User> users = JsonSerializer.Deserialize<List<User>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return users;
    }
    
    public async Task<bool> DeleteConversation(int loggedUserId, int otherUserId)
    {
        HttpResponseMessage response = await client.DeleteAsync($"/Message/{loggedUserId}/{otherUserId}");
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        bool deletedUser = JsonSerializer.Deserialize<bool>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return deletedUser;
    }
}