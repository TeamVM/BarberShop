using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;
using FrizerskiSalon.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FrizerskiSalon.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{

    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService; 
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser(UserDTO user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userModel = new UserModel
        {
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Surname = user.Surname
        };

        var createdUser = await userService.CreateUser(userModel);

        return Ok(createdUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDTO userForLoginDto)
    {
        // Obrisi komentare kad implementiras logiku da pokupis user-a iz baze
        //var userFromRepo = await _repo.Login(userForLoginDto.Username, userForLoginDto.Password);
        //if (userFromRepo == null)
        //    return Unauthorized();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, string.Empty),
            new Claim(ClaimTypes.Name, string.Empty)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes("secret token key"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var user = new UserDTO();
        //var user = _mapper.Map<UserForListDto>(userFromRepo);

        return Ok(new { token = tokenHandler.WriteToken(token), user });
    }
}
