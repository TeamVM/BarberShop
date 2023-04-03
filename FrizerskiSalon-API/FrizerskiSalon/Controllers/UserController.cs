using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;
using FrizerskiSalon.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDTO user) =>
        Ok(await userService.CreateUsers(new UserModel()
        {
            Name= user.Name,
            Email = user.Email,
            Phone = user.Phone,
            Surname = user.Surname
        }));
}
