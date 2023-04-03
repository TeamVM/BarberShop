using FrizerskiSalon.Core.Models;

namespace FrizerskiSalon.Core.Interfaces;

public interface IUserRepository
{
    Task<Guid> CreateUsers(UserModel user);
}
