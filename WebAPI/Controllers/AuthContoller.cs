using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Services;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration config;
    private readonly IAuthService authService;

    public AuthController(IConfiguration config, IAuthService authService)
    {
        this.config = config;
        this.authService = authService;
    }

    [HttpPost, Route("register")]
    public async Task<ActionResult> Register([FromBody] User user)
    {
        await authService.RegisterUser(user);
        return Ok();
    }

    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            User user = await authService.ValidateUser(userLoginDto.UserName, userLoginDto.Password);
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
            new Claim("id", user.Id.ToString()),
        };
        return claims.ToList();
    }
    [HttpGet("allowanon"), AllowAnonymous]
    public ActionResult GetAsAnon()
    {
        return Ok("This was accepted as anonymous");
    }
}