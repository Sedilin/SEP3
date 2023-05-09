using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IUserLogic userLogic;

    public AuthController(IConfiguration config, IUserLogic userLogic)
    {
        this.config = config;
        this.userLogic = userLogic;
    }

    [HttpPost, Route("register")]
    public async Task<ActionResult> Register([FromBody] UserCreationDto user)
    {
        await userLogic.CreateAsync(user);
        return Ok();
    }

    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody]UserLoginDto userLoginDto)
    {
        try
        {
            User user = await userLogic.ValidateUser(userLoginDto.UserName, userLoginDto.Password);
            string token = GenerateJwt(user);
        
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
        
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        
        JwtHeader header = new JwtHeader(signIn);
        
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
        
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
        
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }

    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim("SecurityLevel", user.SecurityLevel.ToString()),
            new Claim("Type", user.UserType.ToString())
        };
        return claims.ToList();
    }
}