using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Services.Core;
using EuroLeaguePlayerBuilder.Services.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EuroLeaguePlayerBuilder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ICoachService, CoachService>();
            builder.Services.AddScoped<ITeamService, TeamService>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                ConfigureIdentity(options);
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentity(IdentityOptions identityOptions)
        {
            identityOptions.SignIn.RequireConfirmedAccount = false;
            identityOptions.SignIn.RequireConfirmedEmail = false;
            identityOptions.SignIn.RequireConfirmedPhoneNumber = false;

            identityOptions.User.RequireUniqueEmail = true;

            identityOptions.Lockout.MaxFailedAccessAttempts = 200;
            identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

            identityOptions.Password.RequireDigit = true;
            identityOptions.Password.RequireLowercase = false;
            identityOptions.Password.RequireNonAlphanumeric = false;
            identityOptions.Password.RequireUppercase = false;
            identityOptions.Password.RequiredLength = 6;
            identityOptions.Password.RequiredUniqueChars = 0;
        }
    }
}
