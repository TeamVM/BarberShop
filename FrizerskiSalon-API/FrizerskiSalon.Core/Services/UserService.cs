using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;

namespace FrizerskiSalon.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userReository;

    public UserService(IUserRepository userReository)
    {
        this.userReository = userReository;
    }

    public async Task<Guid> CreateUser(UserModel user) =>
        await userReository.CreateUser(user);

    public async Task<UserModel?> LoginUser(string email, string password)
    {
        return await userReository.LoginUser(email, password);
    }
}
