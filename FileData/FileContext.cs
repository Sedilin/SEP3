using System.Text.Json;
using Domain.Model;


namespace FileData;

public class FileContext
{
    private const string FilePath = "data.json";

    private DataContainer? dataContainer;
    

    public ICollection<User?> Users
    {
        get
        {
            LazyLoadData();
            return dataContainer!.Users;
        }
    }

    private void LazyLoadData()
    {
        if (dataContainer == null)
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        if (dataContainer != null) return;
        
        if (!File.Exists(FilePath))
        {
            dataContainer = new ()
            {
            
                Users = new List<User>()
            };
            return;
        }
        string content = File.ReadAllText(FilePath);
        dataContainer = JsonSerializer.Deserialize<DataContainer>(content, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath, serialized);
        dataContainer = null;
    }
}