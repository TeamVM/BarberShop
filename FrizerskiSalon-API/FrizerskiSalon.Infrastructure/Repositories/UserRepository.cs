using Dapper;
using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;
using FrizerskiSalon.Infrastructure.Configuration;
using FrizerskiSalon.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data.SqlClient;

namespace FrizerskiSalon.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IOptions<ConnectionConfiguration> confugiration;

    public UserRepository(IOptions<ConnectionConfiguration> confugiration)
    {
        this.confugiration = confugiration;
    }

    public async Task<Guid> CreateUsers(UserModel user)
    {
        // Logika za dobavljanje barber_shop_id-a
        // var barberShopId = await GetBarberShopIdFromDb();

        var entity = new User()
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Surname = user.Surname,
            Phone = user.Phone,
            Email = user.Email,
            BarberShopId = Guid.Parse("141795c2-d265-11ed-b69f-fb6f22e8114e")
        };

        using var connection = new NpgsqlConnection(confugiration.Value.MyPostgresConnection);
        await connection.OpenAsync();

        var sql = "INSERT INTO Users (id, name, surname, email, phone, barber_shop_id) " +
                  "VALUES (@Id, @Name, @Surname, @Email, @Phone, @BarberShopId); ";

        var userId = Guid.NewGuid();
        var parameters = new
        {
            entity.Id,
            entity.Name,
            entity.Surname,
            entity.Email,
            entity.Phone,
            entity.BarberShopId,
        };

        await connection.ExecuteAsync(sql, parameters);
        return userId;
    }
}
