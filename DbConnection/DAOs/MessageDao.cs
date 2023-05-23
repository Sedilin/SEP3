using System.Net.Http.Json;
using System.Text.Json;
using Application.DaoInterfaces;
using Domain.DTOs;

namespace DbConnection.DAOs;

public class MessageDao : IMessageDao
{
    private readonly HttpClient client;

    public MessageDao(HttpClient client)
    {
        this.client = client;
    }

    public async Task<bool> ArchiveMessage(MessageDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/archive", dto);
        
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        bool responded = JsonSerializer.Deserialize<bool>(result, new JsonSerializerOptions());
        return responded;
    }
}