using EuroLeaguePlayerBuilder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EuroLeagueFantasy.Data.Configuration
{
    public class TeamEntityConfiguration : IEntityTypeConfiguration<Team>
    {
        private readonly IEnumerable<Team> Teams = new List<Team>()
        {
            new Team
                {
                    Id = 1,
                    Name = "Panathinaikos",
                    Country = "Greece",
                    City = "Athens",
                    CoachId = 1 // Dimitris Itoudis
                },
                new Team
                {
                    Id = 2,
                    Name = "Olympiacos",
                    Country = "Greece",
                    City = "Piraeus",
                    CoachId = 2 // Georgios Bartzokas
                },
                new Team
                {
                    Id = 3,
                    Name = "Real Madrid",
                    Country = "Spain",
                    City = "Madrid",
                    CoachId = 3 // Pablo Laso
                },
                new Team
                {
                    Id = 4,
                    Name = "Barcelona",
                    Country = "Spain",
                    City = "Barcelona",
                    CoachId = 4 // Sarunas Jasikevicius
                },
                new Team
                {
                    Id = 5,
                    Name = "Crvena Zvezda",
                    Country = "Serbia",
                    City = "Belgrade",
                    CoachId = 5 // Dejan Radonjic
                },
                new Team
                {
                    Id = 6,
                    Name = "Partizan",
                    Country = "Serbia",
                    City = "Belgrade",
                    CoachId = 6 // Zvezdan Mitrovic
                }
        };
        
        public void Configure(EntityTypeBuilder<Team> entity)
        {
            entity.HasData(Teams);
        }
    }
}
