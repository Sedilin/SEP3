namespace Domain.Model;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int SecurityLevel { get; set; }
    public string UserType { get; set; }

    public User(string userName, string password, int securityLevel, string userType)
    {
        UserName = userName;
        Password = password;
        SecurityLevel = 4;
        UserType = userType;
    }
    
    public void ChangeUserType (string newType )
    {
        UserType = newType;
    }
    
}