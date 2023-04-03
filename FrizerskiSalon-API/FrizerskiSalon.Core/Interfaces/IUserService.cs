using FrizerskiSalon.Core.Models;

namespace FrizerskiSalon.Core.Interfaces;

public interface IUserService
{
    public Task<Guid> CreateUsers(UserModel user);
}
