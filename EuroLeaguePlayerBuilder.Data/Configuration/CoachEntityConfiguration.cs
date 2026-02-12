using EuroLeaguePlayerBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EuroLeagueFantasy.Data.Configuration
{
    public class CoachEntityConfiguration : IEntityTypeConfiguration<Coach>
    {
        private readonly IEnumerable<Coach> Coaches = new List<Coach>()
        {
            new Coach
                {
                    Id = 1,
                    FirstName = "Dimitris",
                    LastName = "Itoudis",
                    TitlesWon = 5,
                },
                new Coach
                {
                    Id = 2,
                    FirstName = "Georgios",
                    LastName = "Bartzokas",
                    TitlesWon = 3,
                },
                new Coach
                {
                    Id = 3,
                    FirstName = "Pablo",
                    LastName = "Laso",
                    TitlesWon = 4,
                },
                new Coach
                {
                    Id = 4,
                    FirstName = "Sarunas",
                    LastName = "Jasikevicius",
                    TitlesWon = 2,
                },
                new Coach
                {
                    Id = 5,
                    FirstName = "Dejan",
                    LastName = "Radonjic",
                    TitlesWon = 1,
                },
                new Coach
                {
                    Id = 6,
                    FirstName = "Zvezdan",
                    LastName = "Mitrovic",
                    TitlesWon = 1,
                }
        };
        public void Configure(EntityTypeBuilder<Coach> entity)
        {
            entity.HasData(Coaches);
        }
    }
}
