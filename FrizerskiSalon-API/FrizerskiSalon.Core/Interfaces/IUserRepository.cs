using FrizerskiSalon.Core.Models;

namespace FrizerskiSalon.Core.Interfaces;

public interface IUserRepository
{
    Task<Guid> CreateUser(UserModel user);

    Task<UserModel?> LoginUser(string email, string password);
}
