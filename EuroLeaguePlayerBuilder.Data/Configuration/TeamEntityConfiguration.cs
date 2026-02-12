using EuroLeaguePlayerBuilder.Data.Models;
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
                    LogoPath = "/images/panathinaikos.svg.png",
                    CoachId = 1 // Dimitris Itoudis
                },
                new Team
                {
                    Id = 2,
                    Name = "Olympiacos",
                    Country = "Greece",
                    City = "Piraeus",
                    LogoPath = "/images/olympiacos.svg.png",
                    CoachId = 2 // Georgios Bartzokas
                },
                new Team
                {
                    Id = 3,
                    Name = "Real Madrid",
                    Country = "Spain",
                    City = "Madrid",
                    LogoPath = "/images/real_madrid.svg.png",
                    CoachId = 3 // Pablo Laso
                },
                new Team
                {
                    Id = 4,
                    Name = "Barcelona",
                    Country = "Spain",
                    City = "Barcelona",
                    LogoPath = "/images/barcelona.svg.png",
                    CoachId = 4 // Sarunas Jasikevicius
                },
                new Team
                {
                    Id = 5,
                    Name = "Crvena Zvezda",
                    Country = "Serbia",
                    City = "Belgrade",
                    LogoPath = "/images/crvena_zvezda.svg.png",
                    CoachId = 5 // Dejan Radonjic
                },
                new Team
                {
                    Id = 6,
                    Name = "Partizan",
                    Country = "Serbia",
                    City = "Belgrade",
                    LogoPath = "/images/partizan.svg.png",
                    CoachId = 6 // Zvezdan Mitrovic
                }
        };
        
        public void Configure(EntityTypeBuilder<Team> entity)
        {
            entity.HasData(Teams);
        }
    }
}
