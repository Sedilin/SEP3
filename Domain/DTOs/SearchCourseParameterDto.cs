namespace Domain.DTOs;

public class SearchCourseParameterDto
{
    public string? CourseContains { get;  }

    public SearchCourseParameterDto(string? courseContains)
    {
        CourseContains = courseContains;
    }
}