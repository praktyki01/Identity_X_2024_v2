using Identity_X_2024_v2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Identity_X_2024_v2.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();           

            //sprawdzamy czy są rekordy w bazie danych
            if (context.Sport.Any())
            {
                return;   //są rekordy więc wychodzimy
            }
            var sporty = new Sport[]
            {
                new Sport{Nazwa="Bieganie",ImgUrl="https://"},
                new Sport{Nazwa="Kolarstwo", ImgUrl="https://"},
                new Sport{Nazwa="Pływanie",ImgUrl="https://"}
            };
            foreach (var sport in sporty)
            {
                context.Sport.Add(sport);
            }
            
            context.SaveChanges();
        }
      
    }
}
