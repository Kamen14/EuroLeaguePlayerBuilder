using EuroLeaguePlayerBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static EuroLeaguePlayerBuilder.GCommon.ImageConstants.TeamImages;

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
                    LogoPath = PanathinaikosLogo, //"/images/panathinaikos.svg.png",
                    CoachId = 1, // Dimitris Itoudis
                    ArenaId = 1 // OAKA
                },
                new Team
                {
                    Id = 2,
                    Name = "Olympiacos",
                    Country = "Greece",
                    City = "Piraeus",
                    LogoPath = OlympiacosLogo, // "/images/olympiacos.svg.png",
                    CoachId = 2, // Georgios Bartzokas
                    ArenaId = 2 // Peace and Friendship Stadium
                },
                new Team
                {
                    Id = 3,
                    Name = "Real Madrid",
                    Country = "Spain",
                    City = "Madrid",
                    LogoPath = RealMadridLogo, // "/images/real_madrid.svg.png",
                    CoachId = 3, // Pablo Laso
                    ArenaId = 3 // WiZink Center
                },
                new Team
                {
                    Id = 4,
                    Name = "Barcelona",
                    Country = "Spain",
                    City = "Barcelona",
                    LogoPath = BarcelonaLogo, // "/images/barcelona.svg.png",
                    CoachId = 4, // Sarunas Jasikevicius
                    ArenaId = 4 // Palau Blaugrana
                },
                new Team
                {
                    Id = 5,
                    Name = "Crvena Zvezda",
                    Country = "Serbia",
                    City = "Belgrade",
                    LogoPath = CrvenaZvezdaLogo, // "/images/crvena_zvezda.svg.png",
                    CoachId = 5, // Dejan Radonjic
                    ArenaId = 5 // Belgrade Arena
                },
                new Team
                {
                    Id = 6,
                    Name = "Partizan",
                    Country = "Serbia",
                    City = "Belgrade",
                    LogoPath = PartizanLogo, // "/images/partizan.svg.png",
                    CoachId = 6, // Zvezdan Mitrovic
                    ArenaId = 5 // Belgrade Arena
                }
        };
        
        public void Configure(EntityTypeBuilder<Team> entity)
        {
            entity.HasData(Teams);
        }
    }
}
