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

    public async Task<Guid> CreateUsers(UserModel user) =>
        await userReository.CreateUsers(user);
}
