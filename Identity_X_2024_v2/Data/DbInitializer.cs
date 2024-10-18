using Identity_X_2024_v2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System;
using Identity_X_2024_v2.Models.Enums;

namespace Identity_X_2024_v2.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
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

            var cos = await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            var defaultUser = new ApplicationUser
            {
                UserName = "email0@email.pl",
                Email = "email0@email.pl",
                Imie = "Jan",
                Nazwisko = "Kowalski",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (context.Users.All(u => u.Id != defaultUser.Id))
            {
                var user =  await userManager.FindByEmailAsync(defaultUser.Email);
                var result = await userManager.CreateAsync(defaultUser, "Haslo1!");
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());             

            }


            context.SaveChanges();
        }
      
    }
}
