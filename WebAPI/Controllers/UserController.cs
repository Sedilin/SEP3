using System.Text.Json;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic userLogic;

    public UserController(IUserLogic userLogic)
    {
        this.userLogic = userLogic;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await userLogic.CreateAsync(dto);
            return Created($"/user/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAsync( string? username = null)
    {
        try
        {
            SearchUserParametersDto parameters = new(username);
            IEnumerable<User> users = await userLogic.GetAsync(parameters);
            return Ok(users);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("newTutor")]
    public async Task<ActionResult<User>> PostNewTutorAsync(UserToTutorDto dto)
    {
        try
        {
            User user = await userLogic.PostNewTutorAsync(dto);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("tutor")]
    public async Task<ActionResult<TutorInformationDto>> GetTutorAsync( string? username = null)
    {
        try
        {
            SearchUserParametersDto parameters = new(username);
            TutorInformationDto description = await userLogic.GetTutorAsync(parameters);
            return Ok(description);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("tutorByUsername")]
    public async Task<ActionResult<User?>> GetTutorByUsername(string userName)
    {
        try
        {
            SearchUserParametersDto parameters = new(userName);
            User? user = await userLogic.GetTutorByUsername(parameters);
            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}