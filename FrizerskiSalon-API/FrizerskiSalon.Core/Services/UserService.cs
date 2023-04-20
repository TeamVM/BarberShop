using FrizerskiSalon.Core.Interfaces;
using FrizerskiSalon.Core.Models;
using FrizerskiSalon.Models.Configuration;
using Microsoft.Extensions.Options;

namespace FrizerskiSalon.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userReository;
    private readonly ITermRepository termRepository;
    private readonly AppConfig appConfig;

    public UserService(
        IUserRepository userReository,
        ITermRepository termRepository,
        IOptions<AppConfig> appConfig)
    {
        this.userReository = userReository;
        this.termRepository = termRepository;
        this.appConfig = appConfig.Value
    }

    public async Task<Guid> CreateUser(UserModel user) =>
        await userReository.CreateUser(user);

    public async Task<UserModel?> LoginUser(string email, string password)
    {
        return await userReository.LoginUser(email, password);
    }

    public async Task<bool> ReserveTerm(Guid userId, Guid termId, Guid serviceId, int xmin)
    {
        return await termRepository.ReserveTerm(userId, serviceId, termId, xmin);
    }

    public async Task<List<Term>> GetAvaliableTerms(Guid barberShopId, DateTime dateTime, Guid serviceId)
    {
        int duration = await termRepository.GetServiceDuration(serviceId);

        List<Term> terms = await termRepository.GetAvaliableTerms(barberShopId, dateTime);

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
}
