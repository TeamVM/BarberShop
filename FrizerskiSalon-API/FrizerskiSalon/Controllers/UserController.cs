using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;
using FrizerskiSalon.Core.Services;
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
    private readonly ITermService termService;

    public UserController(IUserService userService, ITermService termService)
    {
        this.userService = userService;
        this.termService = termService; 
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
            Password = user.Password
        };

        var createdUser = await userService.CreateUser(userModel);

        return Ok(createdUser);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDTO loginUser)
    {
        var userFromRepo = new UserModel();
        if (loginUser.Email != null && loginUser.Password != null)
        {
            userFromRepo = await userService.LoginUser(loginUser.Email, loginUser.Password);
        }
        else
        {
            return BadRequest(ModelState);  
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userFromRepo.Email),
            new Claim(ClaimTypes.Role, userFromRepo.Password)
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


        var user = userFromRepo;        //var user = _mapper.Map<UserForListDto>(userFromRepo);

        return Ok(new { token = tokenHandler.WriteToken(token), user });
    }

    [HttpPost("reserveTerm")]
    public async Task<IActionResult> ReserveTerm(TermDTO term)
    {
        try
        {
            if (term.UserId.HasValue && term.ServiceID.HasValue)
            {
                bool success = await termService.ReserveTerm(
                    term.UserId.Value,
                    term.TermID,
                    term.ServiceID.Value,
                    term.Xmin);
                //xmin je za concurrency check - 

                return Ok(new
                {
                    Success = success,
                    Message = "Term is already reserved."
                });
                //opisi error
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
            //log
        }
    }
}
