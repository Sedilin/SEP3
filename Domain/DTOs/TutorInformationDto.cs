using Domain.Model;

namespace Domain.DTOs;

public class TutorInformationDto
{
    public User User { get; }
    public List<string> Courses { get; set; }
    public string Description { get; }


    public TutorInformationDto(User user, string description)
    {
        User = user;
        Courses = new List<string>();
        Description = description;
    }
}