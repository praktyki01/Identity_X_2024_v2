using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Identity_X_2024_v2.Models;

namespace Identity_X_2024_v2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Identity_X_2024_v2.Models.Sport> Sport { get; set; } = default!;
        public DbSet<Identity_X_2024_v2.Models.Waga> Waga { get; set; } = default!;
        public DbSet<Identity_X_2024_v2.Models.Trening> Trening { get; set; } = default!;
    }
}
