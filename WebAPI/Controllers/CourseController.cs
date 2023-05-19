using Application.LogicInterfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseLogic courseLogic;

    public CourseController(ICourseLogic courseLogic)
    {
        this.courseLogic = courseLogic;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> GetAsync(string? username = null)
    {
        try
        {
            SearchCourseParameterDto parameter = new(username);
            IEnumerable<string> users = await courseLogic.GetAsync(parameter);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("tutorByCourse")]
    public async Task<ActionResult<IEnumerable<UserToTutorDto>>> GetTutorByCourse(string course)
    {
        try
        {
            IEnumerable<UserToTutorDto> users = await courseLogic.GetTutorByCourse(course);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}