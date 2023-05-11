using Domain.Model;

namespace Domain.DTOs;

public class UserToTutorDto
{
    public User User { get; }
    public string Course { get; }
    public string Description { get; }


    public UserToTutorDto(User user, string course, string description)
    {
        User = user;
        Course = course;
        Description = description;
    }
}