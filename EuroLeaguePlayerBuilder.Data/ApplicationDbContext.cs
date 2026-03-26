using EuroLeaguePlayerBuilder.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EuroLeaguePlayerBuilder.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Team> Teams { get; set; } = null!;

        public virtual DbSet<Coach> Coaches { get; set; } = null!;

        public virtual DbSet<Player> Players { get; set; } = null!;

        public virtual DbSet<Arena> Arenas { get; set; } = null!;

        public virtual DbSet<Game> Games { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Team>()
                .HasOne(t => t.Arena)
                .WithMany()
                .HasForeignKey(p => p.ArenaId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.TeamOne)
                .WithMany()
                .HasForeignKey(g => g.TeamOneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.TeamTwo)
                .WithMany()
                .HasForeignKey(g => g.TeamTwoId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
