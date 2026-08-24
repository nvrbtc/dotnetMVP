using dotnetMVP.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace dotnetMVP.Models
{
    public class SeedDatabase
    {
        private readonly ApplicationDBcontext _context;
        private readonly UserManager<AppUser> _userManager;
        public SeedDatabase(ApplicationDBcontext context, UserManager<AppUser> manager) 
        { 
            _context = context;
            _userManager = manager;
        }
        public void SeedDatabaseDemo()
        {
            List<Platform> platforms = new List<Platform>();
            if (!_context.Platforms.Any())
            {
                 platforms = new List<Platform>()
                {
                    new Platform()
                    {
                        Id = Guid.NewGuid(),
                        Name = "HTB",
                        Description = "Hack The Box is an online platform that allows you to test your penetration testing skills.",
                        AvailableChallenges = new()
                    },
                    new Platform()
                    {
                        Id = Guid.NewGuid(),
                        Name = "THM",
                        Description = "TryHackMe is an online platform that allows you to learn and practice cybersecurity skills.",
                        AvailableChallenges = new()
                    },
                    new Platform()
                    {
                        Id = Guid.NewGuid(),
                        Name = "cryptohack",
                        Description = "Cryptohack is an online platform that allows you to learn and practice cryptography skills.",
                        AvailableChallenges = new()
                    }
                };
            }
            List<Challenge> tasks = new List<Challenge>();
            if (!_context.Challenges.Any())
            {
                tasks = new List<Challenge>()
                {
                    new Challenge()
                    {
                        Id = Guid.NewGuid(),
                        Name = "MakeSense",
                        Description = "MakeSense is a challenge on Hack The Box that requires you to find the flag hidden in a web application.",
                        Platform = platforms[0]
                    },
                    new Challenge()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Blue",
                        Description = "Blue is a challenge on THM that requires you to find the flag hidden in a web application.",
                        Platform = platforms[1]
                    },
                    new Challenge()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Crypto 101",
                        Description = "Crypto 101 is a challenge on Cryptohack that requires you to find the flag hidden in a web application.",
                        Platform = platforms[2]
                    }
                };
            }
            
            List<WriteUp> writeUps = new List<WriteUp>();
            if ( !_context.WriteUps.Any())
            {
                writeUps = new List<WriteUp>()
                {
                    new WriteUp()
                    {
                        Id = Guid.NewGuid(),
                        Challenge = tasks[0],
                        ChallengeId = tasks[0].Id,
                        Title = "MakeSense WriteUp",
                        Text = "This is a write-up for the MakeSense challenge on Hack The Box.",
                        PasswordHash = "password123",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsHidden = false,
                        IsHiddenByAdmin = false
                    },
                    new WriteUp()
                    {
                        Id = Guid.NewGuid(),
                        Challenge = tasks[1],
                        ChallengeId = tasks[1].Id,
                        Title = "Blue WriteUp",
                        Text = "This is a write-up for the Blue challenge on THM.",
                        PasswordHash = "password123",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsHidden = false,
                        IsHiddenByAdmin = false
                    },
                    new WriteUp()
                    {
                        Id = Guid.NewGuid(),
                        Challenge = tasks[2],
                        ChallengeId = tasks[2].Id,
                        Title = "Crypto 101 WriteUp",
                        Text = "This is a write-up for the Crypto 101 challenge on Cryptohack.",
                        PasswordHash = "password123",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsHidden = false,
                        IsHiddenByAdmin = false
                    }
                };

            }
            _context.Platforms.AddRange(platforms);
            _context.WriteUps.AddRange(writeUps);
            _context.Challenges.AddRange(tasks);
            _context.SaveChanges();
        }
        public async Task EnsureRolesAndAdmins()
        {
            if ( !await _context.Roles.AnyAsync())
            {
                await _context.Roles.AddAsync(new Microsoft.AspNetCore.Identity.IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });
                await _context.Roles.AddAsync(new Microsoft.AspNetCore.Identity.IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "Moder",
                    NormalizedName = "MODER"
                });
            }



            var id = await _context.Roles.Where(x => x.Name == "Admin").Select(x => x.Id).FirstOrDefaultAsync();
            var res = await _context.UserRoles.Where(x=> x.RoleId == id).Select(x => x.UserId).AnyAsync();

            if (res) return; // if some users are already assigned to the Admin role, we don't need to create a new admin user.

            var adminUser = new AppUser
            {
                Id = Guid.NewGuid(),
                Bio = "Admin User for testing.",
                UserName = "admin",
            };
            var createdUser = await _userManager.CreateAsync(adminUser, "Admin123!");
            if ( !createdUser.Succeeded) throw new Exception("Failed to create admin user.");

            var admin = await _userManager.AddToRoleAsync(adminUser, "Admin");
            var moder = await _userManager.AddToRoleAsync(adminUser, "Moder");

            if ( !admin.Succeeded || !moder.Succeeded ) throw new Exception ("Roles initialization failed.");
            
        }    
    }
}
