namespace Domain.Model;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int SecurityLevel { get; set; }

    public User(string userName, string password, int securityLevel)
    {
        UserName = userName;
        Password = password;
        SecurityLevel = 4;
    }

    public User(string userName, string password)
    {
        userName = UserName;
        Password = password;
    }

    public User()
    {
        
    }
}