using FrizerskiSalon.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrizerskiSalon.Core.Interfaces
{
    public interface ITermRepository
    {
        Task<Guid> CreateTerm(Term term);
        Task<bool> UnreserveTerm(Guid termId);
        Task<bool> ReserveTerm(Guid userId, Guid serviceId, Guid termId, int xmin);
        Task<List<Term>> GetAvaliableTerms(Guid barberShopId, DateTime dateTime);
        Task<int> GetServiceDuration(Guid serviceId);
    }
}
