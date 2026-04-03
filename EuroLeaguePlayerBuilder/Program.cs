using EuroLeaguePlayerBuilder.Data;
using EuroLeaguePlayerBuilder.Data.Repositories;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
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

            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddScoped<ITeamRepository, TeamRepository>();
            builder.Services.AddScoped<IArenaRepository, ArenaRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();

            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<ICoachService, CoachService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<IArenaService, ArenaService>();
            builder.Services.AddScoped<IGameService, GameService>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                ConfigureIdentity(builder.Configuration, options);
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

            app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");// app.UseStatusCodePagesWithReDirects("/Home/Error/{0}");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentity(ConfigurationManager configurationManager
            , IdentityOptions identityOptions)
        {
            identityOptions.SignIn.RequireConfirmedAccount = configurationManager
                .GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            identityOptions.SignIn.RequireConfirmedEmail = configurationManager
                .GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            identityOptions.SignIn.RequireConfirmedPhoneNumber = configurationManager
                .GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            identityOptions.User.RequireUniqueEmail = configurationManager
                .GetValue<bool>("Identity:User:RequireUniqueEmail"); ;

            identityOptions.Lockout.MaxFailedAccessAttempts = configurationManager
                .GetValue<int>("Identity:Lockout:MaxFailedAccessAttempts");
            identityOptions.Lockout.DefaultLockoutTimeSpan = configurationManager
                .GetValue<TimeSpan>("Identity:Lockout:DefaultLockoutTimeSpan");

            identityOptions.Password.RequireDigit = configurationManager
                .GetValue<bool>("Identity:Password:RequireDigit");
            identityOptions.Password.RequireLowercase = configurationManager
                .GetValue<bool>("Identity:Password:RequireLowercase");
            identityOptions.Password.RequireNonAlphanumeric = configurationManager
                .GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
            identityOptions.Password.RequireUppercase = configurationManager
                .GetValue<bool>("Identity:Password:RequireUppercase");
            identityOptions.Password.RequiredLength = configurationManager
                .GetValue<int>("Identity:Password:RequiredLength");
            identityOptions.Password.RequiredUniqueChars = configurationManager
                .GetValue<int>("Identity:Password:RequiredUniqueChars");
        }
    }
}
