using EuroLeaguePlayerBuilder.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace EuroLeaguePlayerBuilder.Data.Configuration
{
    public class RoleConfiguration
    {
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                var roleExists = roleManager.RoleExistsAsync(role)
                    .GetAwaiter().GetResult();

                if (!roleExists)
                {
                    var result = roleManager.CreateAsync(new IdentityRole(role))
                        .GetAwaiter().GetResult();

                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role: {role}");
                    }
                }
            }
        }

        public static void SeedDefaultAdmin(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = serviceProvider
                .GetRequiredService<IConfiguration>();

            var adminEmail = configuration["AdminSettings:Email"];
            var adminPassword = configuration["AdminSettings:Password"];

            var adminUser = userManager.FindByEmailAsync(adminEmail)
                .GetAwaiter().GetResult();

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                var result = userManager.CreateAsync(adminUser, adminPassword)
                    .GetAwaiter().GetResult();

                if (!result.Succeeded)
                    throw new Exception("Failed to create default admin user.");

                userManager.AddToRoleAsync(adminUser, "Admin")
                    .GetAwaiter().GetResult();
            }
        }
    }
}

