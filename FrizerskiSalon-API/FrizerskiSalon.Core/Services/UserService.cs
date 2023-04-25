using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;
using FrizerskiSalon.Models.Configuration;
using Microsoft.Extensions.Options;

namespace FrizerskiSalon.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;


    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<Guid> CreateUser(UserModel user) =>
        await userRepository.CreateUser(user);

    public async Task<UserModel?> LoginUser(string email, string password)
    {
        return await userRepository.LoginUser(email, password);
    }  
}
