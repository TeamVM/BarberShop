using FrizerskiSalon.Core.Models;


public interface IUserRepository
{
    Task<Guid> CreateUser(UserModel user);

    Task<UserModel?> LoginUser(string email, string password);
}
