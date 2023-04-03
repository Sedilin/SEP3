namespace Domain.DTOs;

public class UserCreationDto
{
    public string UserName { get;}
    public string Password { get;}

    public UserCreationDto(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}