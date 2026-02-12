using EuroLeaguePlayerBuilder.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EuroLeaguePlayerBuilder.GCommon.Enums;

namespace EuroLeagueFantasy.Data.Configuration
{
    public class PlayerEntityConfiguration : IEntityTypeConfiguration<Player>
    {
        private readonly IEnumerable<Player> Players = new List<Player>()
        {
            // Panathinaikos (Greece)
                new Player { Id = 1, FirstName = "Ioannis", LastName = "Papapetrou", Position = Position.SmallForward, PointsPerGame = 12.5, ReboundsPerGame = 4.3, AssistsPerGame = 2.1, TeamId = 1 },
                new Player { Id = 2, FirstName = "Nick", LastName = "Calathes", Position = Position.PointGuard, PointsPerGame = 11.2, ReboundsPerGame = 3.5, AssistsPerGame = 7.4, TeamId = 1 },
                new Player { Id = 3, FirstName = "Dimitris", LastName = "Mitoglou", Position = Position.PowerForward, PointsPerGame = 10.1, ReboundsPerGame = 5.2, AssistsPerGame = 1.3, TeamId = 1 },
                new Player { Id = 4, FirstName = "DeShaun", LastName = "Thomas", Position = Position.SmallForward, PointsPerGame = 9.3, ReboundsPerGame = 4.0, AssistsPerGame = 1.2, TeamId = 1 },
                new Player { Id = 5, FirstName = "Zach", LastName = "LeDay", Position = Position.Center, PointsPerGame = 8.7, ReboundsPerGame = 5.5, AssistsPerGame = 0.9, TeamId = 1 },
                new Player { Id = 6, FirstName = "Moustapha", LastName = "Fall", Position = Position.Center, PointsPerGame = 5.2, ReboundsPerGame = 4.7, AssistsPerGame = 0.5, TeamId = 1 },

                // Olympiacos (Greece)
                new Player { Id = 7, FirstName = "Sasha", LastName = "Vezenkov", Position = Position.PowerForward, PointsPerGame = 15.3, ReboundsPerGame = 5.8, AssistsPerGame = 2.1, TeamId = 2 },
                new Player { Id = 8, FirstName = "Kostas", LastName = "Papanikolaou", Position = Position.SmallForward, PointsPerGame = 10.2, ReboundsPerGame = 4.1, AssistsPerGame = 1.5, TeamId = 2 },
                new Player { Id = 9, FirstName = "Giannoulis", LastName = "Larentzakis", Position = Position.ShootingGuard, PointsPerGame = 11.0, ReboundsPerGame = 3.0, AssistsPerGame = 2.3, TeamId = 2 },
                new Player { Id = 10, FirstName = "Georgios", LastName = "Printezis", Position = Position.ShootingGuard, PointsPerGame = 9.5, ReboundsPerGame = 4.8, AssistsPerGame = 1.0, TeamId = 2 },
                new Player { Id = 11, FirstName = "Shaquielle", LastName = "McKissic", Position = Position.SmallForward, PointsPerGame = 8.2, ReboundsPerGame = 3.2, AssistsPerGame = 1.1, TeamId = 2 },
                new Player { Id = 12, FirstName = "Othello", LastName = "Hunter", Position = Position.Center, PointsPerGame = 7.5, ReboundsPerGame = 5.0, AssistsPerGame = 0.8, TeamId = 2 },

                // Real Madrid (Spain)
                new Player { Id = 13, FirstName = "Sergio", LastName = "Llull", Position = Position.PointGuard, PointsPerGame = 12.3, ReboundsPerGame = 2.4, AssistsPerGame = 5.5, TeamId = 3 },
                new Player { Id = 14, FirstName = "Walter", LastName = "Tavares", Position = Position.Center, PointsPerGame = 9.0, ReboundsPerGame = 6.8, AssistsPerGame = 0.7, TeamId = 3 },
                new Player { Id = 15, FirstName = "Anthony", LastName = "Rudy", Position = Position.SmallForward, PointsPerGame = 10.5, ReboundsPerGame = 4.3, AssistsPerGame = 1.2, TeamId = 3 },
                new Player { Id = 16, FirstName = "Edgar", LastName = "Sosa", Position = Position.ShootingGuard, PointsPerGame = 8.7, ReboundsPerGame = 2.1, AssistsPerGame = 2.0, TeamId = 3 },
                new Player { Id = 17, FirstName = "Guerschon", LastName = "Yabusele", Position = Position.SmallForward, PointsPerGame = 9.2, ReboundsPerGame = 3.8, AssistsPerGame = 1.0, TeamId = 3 },
                new Player { Id = 18, FirstName = "Fabien", LastName = "Causeur", Position = Position.ShootingGuard, PointsPerGame = 11.0, ReboundsPerGame = 2.5, AssistsPerGame = 2.1, TeamId = 3 },

                // Barcelona (Spain)
                new Player { Id = 19, FirstName = "Nikola", LastName = "Mirotic", Position = Position.PowerForward, PointsPerGame = 13.1, ReboundsPerGame = 5.4, AssistsPerGame = 1.6, TeamId = 4 },
                new Player { Id = 20, FirstName = "Nick", LastName = "Calathes", Position = Position.PointGuard, PointsPerGame = 11.0, ReboundsPerGame = 3.2, AssistsPerGame = 6.8, TeamId = 4 },
                new Player { Id = 21, FirstName = "Brandon", LastName = "Davies", Position = Position.Center, PointsPerGame = 10.2, ReboundsPerGame = 5.7, AssistsPerGame = 1.1, TeamId = 4 },
                new Player { Id = 22, FirstName = "Leandro", LastName = "Bolmaro", Position = Position.ShootingGuard, PointsPerGame = 9.5, ReboundsPerGame = 2.8, AssistsPerGame = 2.3, TeamId = 4 },
                new Player { Id = 23, FirstName = "Jaka", LastName = "Blazic", Position = Position.PointGuard, PointsPerGame = 8.9, ReboundsPerGame = 2.0, AssistsPerGame = 1.5, TeamId = 4 },
                new Player { Id = 24, FirstName = "Pierre", LastName = "Oriola", Position = Position.PowerForward, PointsPerGame = 7.8, ReboundsPerGame = 4.0, AssistsPerGame = 0.7, TeamId = 4 },

                // Crvena Zvezda (Serbia)
                new Player { Id = 25, FirstName = "Ognjen", LastName = "Dobric", Position = Position.ShootingGuard, PointsPerGame = 10.0, ReboundsPerGame = 2.5, AssistsPerGame = 1.9, TeamId = 5 },
                new Player { Id = 26, FirstName = "Filip", LastName = "Perrin", Position = Position.Center, PointsPerGame = 8.3, ReboundsPerGame = 4.6, AssistsPerGame = 0.8, TeamId = 5 },
                new Player { Id = 27, FirstName = "Branko", LastName = "Lazić", Position = Position.ShootingGuard, PointsPerGame = 7.9, ReboundsPerGame = 2.0, AssistsPerGame = 1.2, TeamId = 5 },
                new Player { Id = 28, FirstName = "Jordan", LastName = "Lyles", Position = Position.SmallForward, PointsPerGame = 9.1, ReboundsPerGame = 3.1, AssistsPerGame = 1.3, TeamId = 5 },
                new Player { Id = 29, FirstName = "Corey", LastName = "Walden", Position = Position.PointGuard, PointsPerGame = 11.2, ReboundsPerGame = 2.5, AssistsPerGame = 4.0, TeamId = 5 },
                new Player { Id = 30, FirstName = "Marko", LastName = "Simonovic", Position = Position.PowerForward, PointsPerGame = 10.4, ReboundsPerGame = 5.3, AssistsPerGame = 1.0, TeamId = 5 },

                // Partizan (Serbia)
                new Player { Id = 31, FirstName = "Kevin", LastName = "Punter", Position = Position.ShootingGuard, PointsPerGame = 12.2, ReboundsPerGame = 3.1, AssistsPerGame = 2.5, TeamId = 6 },
                new Player { Id = 32, FirstName = "Mathias", LastName = "Lessort", Position = Position.Center, PointsPerGame = 9.5, ReboundsPerGame = 5.3, AssistsPerGame = 0.9, TeamId = 6 },
                new Player { Id = 33, FirstName = "Nikola", LastName = "Jovic", Position = Position.SmallForward, PointsPerGame = 10.0, ReboundsPerGame = 3.7, AssistsPerGame = 1.2, TeamId = 6 },
                new Player { Id = 34, FirstName = "Zlatko", LastName = "Racic", Position = Position.PowerForward, PointsPerGame = 8.5, ReboundsPerGame = 4.2, AssistsPerGame = 0.8, TeamId = 6 },
                new Player { Id = 35, FirstName = "Ognjen", LastName = "Jaramaz", Position = Position.ShootingGuard, PointsPerGame = 9.0, ReboundsPerGame = 2.8, AssistsPerGame = 1.5, TeamId = 6 },
                new Player { Id = 36, FirstName = "Shawn", LastName = "Hines", Position = Position.PointGuard, PointsPerGame = 11.1, ReboundsPerGame = 2.5, AssistsPerGame = 3.9, TeamId = 6 },
        };

        public void Configure(EntityTypeBuilder<Player> entity)
        {
            entity.HasData(Players);
        }
    }
}
