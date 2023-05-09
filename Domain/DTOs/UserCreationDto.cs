namespace Domain.DTOs;

public class UserCreationDto
{
    public string UserName { get;}
    public string Password { get;}
    public int SecurityLevel { get; set; }
    public string UserType { get; set; }

    public UserCreationDto(string userName, string password, int securityLevel, string userType)
    {
        UserName = userName;
        Password = password;
        SecurityLevel = securityLevel;
        UserType = userType;
    }
}