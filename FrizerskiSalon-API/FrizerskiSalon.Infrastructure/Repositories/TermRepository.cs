using Dapper;
using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;
using FrizerskiSalon.Infrastructure.Configuration;
using FrizerskiSalon.Infrastructure.Models;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizerskiSalon.Infrastructure.Repositories
{
    public class TermRepository : ITermRepository
    {
        private readonly IOptions<ConnectionConfiguration> confugiration;

        public TermRepository(IOptions<ConnectionConfiguration> confugiration)
        {
            this.confugiration = confugiration;
        }
        //koristicu hangfire  i kreiracu job koji ce jednom nedeljno kreirati termine za sop
        //dovlacenje usluga po sopu kao za termine slicno
        //
        public async Task<Guid> CreateTerm(Term term)
        {
            // Logika za dobavljanje barber_shop_id-a
            // var barberShopId = await GetBarberShopIdFromDb();

            var entity = new DMTerm()
            {
                TermID = Guid.NewGuid(),
                BarberShopID = Guid.Parse("141795c2-d265-11ed-b69f-fb6f22e8114e"),
                DateTime = term.DateTime,
                StartTime = term.StartTime,
                UserId = term.UserId,
                ServiceID = term.ServiceID,
                Busy = false,
                WorkerID = term.WorkerID,
                // Za testiranje - obrisi kasnije ili dodaj logiku da dobavlja pravi BarberShopId

            };

            using var connection = new NpgsqlConnection(confugiration.Value.MyPostgresConnection);
            await connection.OpenAsync();

            var sql = "INSERT INTO Users (TermID, barber_shop_id, DateTime, StartTime, UserId, Busy, WorkerID) " +
                      "VALUES (@TermID, @barber_shop_id, @DateTime, @StartTime, @UserId, @Busy, @WorkerID); ";

            var termId = Guid.NewGuid();
            var parameters = new
            {
                entity.TermID,
                entity.BarberShopID,
                entity.DateTime,
                entity.StartTime,
                entity.UserId,
                entity.Busy,
                entity.WorkerID,
            };

            await connection.ExecuteAsync(sql, parameters);

            await connection.CloseAsync();

            return termId;
        }

        public async Task<bool> ReserveTerm(Guid userId, Guid serviceId, Guid termId, int xmin)
        {
            using var connection = new NpgsqlConnection(confugiration.Value.MyPostgresConnection);
            await connection.OpenAsync();

            var sql = @"UPDATE Term 
                        SET
                            UserId = @UserId,
                            ServiceId = @ServiceId
                        WHERE
                            TermId = @TermId
                        AND
                            Xmin = @Xmin
                       ";

            var parameters = new
            {
                UserId = userId,
                ServiceId = serviceId,
                TermId = termId,
                Xmin = xmin
            };

            var affectedRows = await connection.ExecuteAsync(sql, parameters);

            await connection.CloseAsync();

            return affectedRows > 0;
        }

        public async Task<bool> UnreserveTerm(Guid termId)
        {
            using var connection = new NpgsqlConnection(confugiration.Value.MyPostgresConnection);
            await connection.OpenAsync();

            var sql = @"UPDATE Term 
                        SET
                            UserId = NULL,
                            ServiceId = NULL
                        WHERE
                            TermId = @TermId
                       ";

            var parameters = new
            {
                TermId = termId
            };

            var affectedRows = await connection.ExecuteAsync(sql, parameters);

            await connection.CloseAsync();

            return affectedRows > 0;
        }

        public async Task<List<Term>> GetAvaliableTerms(Guid barberShopId, DateTime dateTime)
        {
            using var connection = new NpgsqlConnection(confugiration.Value.MyPostgresConnection);
            await connection.OpenAsync();

            var sql = @"SELECT 
                            *
                        FROM Term
                        WHERE
                            BarberShopId = @BarberShopId
                        AND
                            DateTime = @DateTime
                        AND
                            Busy = 'false'
                        ";

            var parameters = new
            {
                DateTime = dateTime,
                BarberShopId = barberShopId
            }; 

            IEnumerable<Term> result = await connection.QueryAsync<Term>(sql, parameters);

            return result.OrderBy(x => x.StartTime).ToList();
        }

        public async Task<int> GetServiceDuration(Guid serviceId)
        {
            using var connection = new NpgsqlConnection(confugiration.Value.MyPostgresConnection);
            await connection.OpenAsync();

            var sql = @"SELECT 
                            Duration
                        FROM Service
                        WHERE
                            BarberShopId = @BarberShopId
                        ";

            var parameters = new
            {
                ServiceId = serviceId
            };

            int duration = await connection.QuerySingleOrDefaultAsync<int>(sql, parameters);

            return duration;
        }
    }
}
