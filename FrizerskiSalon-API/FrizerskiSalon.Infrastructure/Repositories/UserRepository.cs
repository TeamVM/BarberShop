using Dapper;
using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;
using FrizerskiSalon.Infrastructure.Configuration;
using FrizerskiSalon.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data.SqlClient;
using System.Threading;

namespace FrizerskiSalon.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{

    private readonly IOptions<ConnectionConfiguration> confugiration;

    public UserRepository(IOptions<ConnectionConfiguration> confugiration)
    {
        this.confugiration = confugiration;
    }

    public async Task<Guid> CreateUser(UserModel user)
    {
        // Logika za dobavljanje barber_shop_id-a
        // var barberShopId = await GetBarberShopIdFromDb();

        var entity = new DMUser()
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            Password = user.Password,
            Phone = user.Phone,
            Email = user.Email,
            Role = "client",   
            // Za testiranje - obrisi kasnije ili dodaj logiku da dobavlja pravi BarberShopId
            BarberShopId = Guid.Parse("141795c2-d265-11ed-b69f-fb6f22e8114e")
        };

        using var connection = new NpgsqlConnection(confugiration.Value.MyPostgresConnection);
        await connection.OpenAsync();

        var sql = "INSERT INTO Users (id, name, password, email, phone, barber_shop_id, role) " +
                  "VALUES (@Id, @Name, @Password, @Email, @Phone, @BarberShopId, @Role); ";

        var userId = Guid.NewGuid();
        var parameters = new
        {
            entity.Id,
            entity.Name,
            entity.Password,
            entity.Email,
            entity.Phone,
            entity.BarberShopId,
            entity.Role,
        };

        await connection.ExecuteAsync(sql, parameters);
        return userId;
     }

    public async Task<UserModel> LoginUser(string email, string password)
    {
        try
        {
            using var connection = new NpgsqlConnection(confugiration.Value.MyPostgresConnection);
            var query = @"
            SELECT Id, Name, Password, Phone, Email, barber_shop_id, Role
            FROM Users
            WHERE Email = @Email AND Password = @Password
            ";

            var parameters = new { Email = email, Password = password };

            return await connection.QuerySingleOrDefaultAsync<UserModel>(query, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new UserModel();
        }
    }

}

