using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RaxRotCinema.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "ba8eee3f-54b1-40f4-b62c-527fee2e4678";
            var superAdminRoleId = "74fa71ba-a061-47d3-897b-211c8b4b297b";
            var userRoleId = "dcadc65c-ae59-44f6-bcdc-974e46fdc0eb";

            //SeedRoles user,admin,superadmin

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName="Admin",
                    Id=adminRoleId,
                    ConcurrencyStamp=adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName="SuperAdmin",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId
                },
                 new IdentityRole
                {
                    Name = "User",
                    NormalizedName="User",
                    Id=userRoleId,
                    ConcurrencyStamp=userRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //SeedSuperAdminUser
            var superAdminId = "377fca39-be14-44e6-b6a5-f57a0853cfef";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@raxrot.com",
                Email = "superadmin@raxrot.com",
                NormalizedEmail = "superadmin@raxrot.com".ToUpper(),
                NormalizedUserName = "superadmin@raxrot.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "123456!Aa");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add all roles to superadmin

            var superAdminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    RoleId=adminRoleId,
                    UserId=superAdminId
                },
                new IdentityUserRole<string>()
                {
                    RoleId=userRoleId,
                    UserId=superAdminId
                },
                new IdentityUserRole<string>()
                {
                    RoleId=superAdminRoleId,
                    UserId=superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);


        }
    }
}
