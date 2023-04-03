using System.Text.Json;
using Domain;

namespace FileData;

public class FileContext
{
    private const string FilePath = "data.json";
    private DataContainer? _dataContainer;

    public ICollection<User> Users
    {
        get
        {
            LoadData();
            return _dataContainer!.Users;
        }
    }
    
    private void LoadData()
    {
        if (_dataContainer != null) return;
    
        if (!File.Exists(FilePath))
        {
            _dataContainer = new ()
            {
                Users = new List<User>()
            };
            return;
        }
        var content = File.ReadAllText(FilePath);
        _dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }
    
    public void SaveChanges()
    {
        var serialized = JsonSerializer.Serialize(_dataContainer);
        File.WriteAllText(FilePath, serialized);
        _dataContainer = null;
    }
}