namespace Domain.DTOs;

public class UserCreationDto
{
    public string UserName { get;}
    public string Password { get;}
    public string UserType { get; set; }

    public UserCreationDto(string userName, string password, string userType)
    {
        UserName = userName;
        Password = password;
        UserType = userType;
    }
}