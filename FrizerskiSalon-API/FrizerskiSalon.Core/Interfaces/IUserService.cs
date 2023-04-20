using FrizerskiSalon.Core.Models;

namespace FrizerskiSalon.Core.Interfaces;

public interface IUserService
{
    public Task<Guid> CreateUser(UserModel user);

    public Task<UserModel?> LoginUser(string email, string password);

    Task<bool> ReserveTerm(Guid userId, Guid termId, Guid serviceId, int xmin);

}
