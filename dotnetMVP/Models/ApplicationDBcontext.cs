using dotnetMVP.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace dotnetMVP.Models
{
    public class ApplicationDBcontext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<dotnetMVP.Models.Entities.AppUser> AppUser { get; set; } = default!;
        public DbSet<WriteUp> WriteUps => Set<WriteUp>();
        public DbSet<Challenge> Challenges => Set<Challenge>();
        public DbSet<Platform> Platforms => Set<Platform>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options):base(options) { }
    }
}
