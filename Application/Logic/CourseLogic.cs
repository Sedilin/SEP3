using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;

namespace Application.Logic;

public class CourseLogic : ICourseLogic
{
    
    private readonly ICourseDao courseDao;

    public CourseLogic(ICourseDao courseDao)
    {
        this.courseDao = courseDao;
    }
    
    public async Task<IEnumerable<string>> GetAsync(SearchCourseParameterDto? parameter)
    {
        return await courseDao.GetAsync(parameter);
    }
}