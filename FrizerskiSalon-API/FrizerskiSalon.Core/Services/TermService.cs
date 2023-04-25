using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;
using FrizerskiSalon.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;


namespace FrizerskiSalon.Core.Services
{
    public class TermService : ITermService
    {
        private readonly ITermRepository termRepository;
        private readonly AppConfig appConfig;


        public TermService(ITermRepository termRepository, IOptions<AppConfig> appConfig)
        {
            this.termRepository = termRepository;
            this.appConfig = appConfig.Value;
        }

        public Task<Guid> CreateTerm(Term term)
        {
            throw new NotImplementedException();
        }


        public async Task<List<Term>> GetAvaliableTerms(Guid barberShopId, DateTime dateTime, Guid serviceId)
        {
            int duration = await termRepository.GetServiceDuration(serviceId);

            List<Term> terms = await termRepository.GetAvaliableTerms(barberShopId, dateTime, serviceId);

            List<Term> avaliableTerms = new List<Term>();
            TimeSpan minTermDuration = new TimeSpan(0, appConfig.MinTermDuration, 0);

            for (int i = 0; i < terms.Count - 1; i++)
            {
                int susedni = 0;
                for (int j = i + 1; j < terms.Count; j++)
                {
                    if (terms[j].StartTime - terms[i].StartTime == minTermDuration * (j - i))
                    {
                        susedni++;
                        if (susedni == duration / appConfig.MinTermDuration)
                        {
                            avaliableTerms.Add(terms[i]);
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return avaliableTerms;
        }

        public async Task<int> GetServiceDuration(Guid serviceId)
        {
            return await termRepository.GetServiceDuration(serviceId);  
        }

        public async Task<bool> ReserveTerm(Guid userId, Guid serviceId, Guid termId, int xmin)
        {
            return await termRepository.ReserveTerm(userId, serviceId, termId, xmin);
        }

        public async Task<bool> UnreserveTerm(Guid termId)
        {
            return await termRepository.UnreserveTerm(termId);  
        }
    }
}
