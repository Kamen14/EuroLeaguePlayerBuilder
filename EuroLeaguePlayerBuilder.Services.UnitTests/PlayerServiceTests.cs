using EuroLeaguePlayerBuilder.Data.Models;
using EuroLeaguePlayerBuilder.Data.Repositories.Interfaces;
using EuroLeaguePlayerBuilder.GCommon.Enums;
using EuroLeaguePlayerBuilder.Services.Core;
using EuroLeaguePlayerBuilder.Services.Models.Players;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using Moq;
using static EuroLeaguePlayerBuilder.GCommon.PlayerPositionHelper;
using static EuroLeaguePlayerBuilder.GCommon.ErrorMessages;

namespace EuroLeaguePlayerBuilder.Services.UnitTests
{
    public class PlayerServiceTests
    {
        private Mock<IPlayerRepository> _playerRepositoryMock;
        private PlayerService _playerService;
        private List<Player> _players;
        private List<Team> _teams;
        private List<ApplicationUser> _users;

        [SetUp]
        public void SetUp()
        {
            _playerRepositoryMock = new Mock<IPlayerRepository>();
            _playerService = new PlayerService(_playerRepositoryMock.Object);
            _players = new List<Player>() //Arange
            {
                new Player
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Position = Position.SmallForward,
                    PointsPerGame = 27.2,
                    ReboundsPerGame = 7.5,
                    AssistsPerGame = 8.3,
                    TeamId = 1,
                    Team = new Team { Id = 1, Name = "LA Lakers" },
                    UserId = "user-1",
                    User = new ApplicationUser { Id = "user-1", UserName = "lebron23" }
                },
                new Player
                {
                    Id = 2,
                    FirstName = "LeBron",
                    LastName = "Anthony",
                    Position = Position.PointGuard,
                    PointsPerGame = 29.4,
                    ReboundsPerGame = 5.1,
                    AssistsPerGame = 6.3,
                    TeamId = 2,
                    Team = new Team { Id = 2, Name = "Golden State Warriors" },
                    UserId = "user-2",
                    User = new ApplicationUser { Id = "user-2", UserName = "curry30" }
                },
                new Player
                {
                    Id = 3,
                    FirstName = "Kevin",
                    LastName = "Durant",
                    Position = Position.PowerForward,
                    PointsPerGame = 28.1,
                    ReboundsPerGame = 6.8,
                    AssistsPerGame = 5.2,
                    TeamId = 3,
                    Team = new Team { Id = 3, Name = "Phoenix Suns" },
                    UserId = "user-3",
                    User = new ApplicationUser { Id = "user-3", UserName = "kd35" }
                },
                new Player
                {
                    Id = 4,
                    FirstName = "Giannis",
                    LastName = "Antetokounmpo",
                    Position = Position.PowerForward,
                    PointsPerGame = 31.1,
                    ReboundsPerGame = 11.8,
                    AssistsPerGame = 5.7,
                    TeamId = 4,
                    Team = new Team { Id = 4, Name = "Milwaukee Bucks" },
                    UserId = "user-4",
                    User = new ApplicationUser { Id = "user-4", UserName = "giannis34" }
                },
                new Player
                {
                    Id = 5,
                    FirstName = "Nikola",
                    LastName = "Jokic",
                    Position = Position.Center,
                    PointsPerGame = 26.4,
                    ReboundsPerGame = 12.4,
                    AssistsPerGame = 9.0,
                    TeamId = 5,
                    Team = new Team { Id = 5, Name = "Denver Nuggets" },
                    UserId = "user-5",
                    User = new ApplicationUser { Id = "user-5", UserName = "jokic15" }
                },
                new Player
                {
                    Id = 6,
                    FirstName = "LeBron",
                    LastName = "James",
                    Position = Position.SmallForward,
                    PointsPerGame = 26.9,
                    ReboundsPerGame = 8.1,
                    AssistsPerGame = 4.6,
                    TeamId = 6,
                    Team = new Team { Id = 6, Name = "Boston Celtics" },
                    UserId = "user-6",
                    User = new ApplicationUser { Id = "user-6", UserName = "tatum0" }
                }
            };

            _teams = new List<Team>()
            {
                new Team { Id = 1, Name = "LA Lakers" },
                new Team { Id = 2, Name = "Golden State Warriors" },
                new Team { Id = 3, Name = "Phoenix Suns" },
                new Team { Id = 4, Name = "Milwaukee Bucks" },
                new Team { Id = 5, Name = "Denver Nuggets" },
                new Team { Id = 6, Name = "Boston Celtics" }
            };

            _users = new List<ApplicationUser>()
            {
                new ApplicationUser { Id = "user-1", UserName = "lebron23" },
                new ApplicationUser { Id = "user-2", UserName = "curry30" },
                new ApplicationUser { Id = "user-3", UserName = "kd35" },
                new ApplicationUser { Id = "user-4", UserName = "giannis34" },
                new ApplicationUser { Id = "user-5", UserName = "jokic15" },
                new ApplicationUser { Id = "user-6", UserName = "tatum0" },
                new ApplicationUser { Id = "user-7", UserName = "cp3" },
                new ApplicationUser { Id = "user-8", UserName = "westbrook0" }
            };
        }

        //For GetAllPlayersOrderedByNameAsync
        [Test]
        public async Task GetAllPlayersOrderedByNameAsync_NoSearchQuery_ReturnsAllPlayersSortedCorrectly()
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking()).Returns(_players.BuildMock());

            // Act
            List<PlayerDto> result = (await _playerService.GetAllPlayersOrderedByNameAsync()).ToList();

            // Assert

            //First (Giannis Antetokounmpo)
            Assert.That(result[0].FirstName, Is.EqualTo("Giannis"));
            Assert.That(result[0].LastName, Is.EqualTo("Antetokounmpo"));
            Assert.That(result[0].Id, Is.EqualTo(4));

            Assert.That(result[1].FirstName, Is.EqualTo("Kevin"));
            Assert.That(result[1].LastName, Is.EqualTo("Durant"));
            Assert.That(result[1].Id, Is.EqualTo(3));

            Assert.That(result[2].FirstName, Is.EqualTo("LeBron"));
            Assert.That(result[2].LastName, Is.EqualTo("Anthony"));
            Assert.That(result[2].Id, Is.EqualTo(2));

            Assert.That(result[3].FirstName, Is.EqualTo("LeBron"));
            Assert.That(result[3].LastName, Is.EqualTo("James"));
            Assert.That(result[3].Id, Is.EqualTo(1));

            Assert.That(result[4].FirstName, Is.EqualTo("LeBron"));
            Assert.That(result[4].LastName, Is.EqualTo("James"));
            Assert.That(result[4].Id, Is.EqualTo(6));

            Assert.That(result[5].FirstName, Is.EqualTo("Nikola"));
            Assert.That(result[5].LastName, Is.EqualTo("Jokic"));
            Assert.That(result[5].Id, Is.EqualTo(5));
        }


        //[Test]
        [TestCase("le", 3, new string[] { "LeBron", "LeBron", "LeBron" })]
        [TestCase("ja", 2, new string[] { "LeBron", "LeBron" })] // matches LeBron James (last name)
        [TestCase("jok", 1, new string[] { "Nikola" })]
        [TestCase("xyz", 0, null)] //matches no players

        public async Task GetAllPlayersOrderedByNameAsync_WithSearchQuery_ReturnsAllPlayersSortedCorrectly(string searchQuery,
            int resultCount, string[]? expectedFirstNames)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking()).Returns(_players.BuildMock());

            // Act
            List<PlayerDto> result = (await _playerService.GetAllPlayersOrderedByNameAsync(searchQuery)).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(resultCount));
            if (expectedFirstNames != null)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    Assert.That(result[i].FirstName, Is.EqualTo(expectedFirstNames[i]));
                }
            }
        }

        [Test]
        public async Task GetAllPlayersOrderedByNameAsync_WithPageNumber_ReturnsAllPlayersSortedCorrectly()
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking()).Returns(_players.BuildMock());

            // Act
            List<PlayerDto> result = (await _playerService
                .GetAllPlayersOrderedByNameAsync(pageNumber: 3, playersPerPage: 2)).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].FirstName, Is.EqualTo("LeBron"));
            Assert.That(result[0].LastName, Is.EqualTo("James"));
            Assert.That(result[0].Id, Is.EqualTo(6));

            Assert.That(result[1].FirstName, Is.EqualTo("Nikola"));
            Assert.That(result[1].LastName, Is.EqualTo("Jokic"));
            Assert.That(result[1].Id, Is.EqualTo(5));
        }

        [Test]
        public async Task GetAllPlayersOrderedByNameAsync_WithPageNumberAndSearchQuery_ReturnsAllPlayersSortedCorrectly()
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking()).Returns(_players.BuildMock());

            // Act
            string searchQuery = "le";

            List<PlayerDto> result = (await _playerService
                .GetAllPlayersOrderedByNameAsync(searchQuery: searchQuery, pageNumber: 1, playersPerPage: 2))
                .ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));

            Assert.That(result[0].FirstName, Is.EqualTo("LeBron"));
            Assert.That(result[0].LastName, Is.EqualTo("Anthony"));
            Assert.That(result[0].Id, Is.EqualTo(2));

            Assert.That(result[1].FirstName, Is.EqualTo("LeBron"));
            Assert.That(result[1].LastName, Is.EqualTo("James"));
            Assert.That(result[1].Id, Is.EqualTo(1));
        }

        //For GetPlayersCountAsync
        [TestCase(null, 6)]
        [TestCase("le", 3)]
        [TestCase("ja", 2)] // matches LeBron James (last name)
        [TestCase("jok", 1)]
        [TestCase("xyz", 0)] //matches no players

        public async Task GetPlayersCountAsync_ReturnsCorrectCount(string? searchQuery, int expectedCount)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking()).Returns(_players.BuildMock());

            // Act
            int playersCount = await _playerService
                .GetPlayersCountAsync(searchQuery);

            // Assert
            Assert.That(playersCount, Is.EqualTo(expectedCount));
        }

        //For GetPlayerDetailsByIdAsync

        [TestCase(1, "LeBron", "James", 1, "LA Lakers", 27.2, 7.5)]
        [TestCase(2, "LeBron", "Anthony", 2, "Golden State Warriors", 29.4, 5.1)]
        [TestCase(3, "Kevin", "Durant", 3, "Phoenix Suns", 28.1, 6.8)]
        [TestCase(4, "Giannis", "Antetokounmpo", 4, "Milwaukee Bucks", 31.1, 11.8)]
        

        public async Task GetPlayerDetailsByIdAsync_ValidId_ReturnsCorrectResult
            (int id, string? expectedFirstName, string? expectedLastName,
            int expectedTeamId, string? expectedTeamName, double expectedPpg, double expectedRpg)
        {

            _playerRepositoryMock.Setup(r => r.GetPlayerWithTeamByIdNoTrackingAsync(id))
                .ReturnsAsync(_players.SingleOrDefault(p => p.Id == id));

            // Act
            PlayerDetailsDto? result = await _playerService.GetPlayerDetailsByIdAsync(id);

            // Assert
            Assert.That(result.FirstName, Is.EqualTo(expectedFirstName));
            Assert.That(result.LastName, Is.EqualTo(expectedLastName));
            Assert.That(result.TeamId, Is.EqualTo(expectedTeamId));
            Assert.That(result.TeamName, Is.EqualTo(expectedTeamName));
            Assert.That(result.PointsPerGame, Is.EqualTo(expectedPpg));
            Assert.That(result.ReboundsPerGame, Is.EqualTo(expectedRpg));
        }

        [TestCase(99)]//matches no player (invalid id cases)
        [TestCase(-1)]
        public async Task GetPlayerDetailsByIdAsync_InvalidId_ReturnsNull (int id)
        {
            _playerRepositoryMock.Setup(r => r.GetPlayerWithTeamByIdNoTrackingAsync(id))
                .ReturnsAsync(_players.SingleOrDefault(p => p.Id == id));

            // Act
            PlayerDetailsDto? result = await _playerService.GetPlayerDetailsByIdAsync(id);

            // Assert
           Assert.That(result, Is.Null);
        }

        //For LoadTeamsDropdownAsync
        [Test]

        public async Task LoadTeamsDropdownAsync_ReturnsAllTeams_OrderedByName()
        {
            _playerRepositoryMock.Setup(r => r.GetAllTeamsNoTracking())
                .Returns(_teams.BuildMock());

            // Act
            List<CreatePlayerTeamDto> result = (await _playerService.LoadTeamsDropdownAsync()).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(_teams.Count));

            Assert.That(result[0].Name, Is.EqualTo("Boston Celtics"));
            Assert.That(result[1].Name, Is.EqualTo("Denver Nuggets"));
            Assert.That(result[2].Name, Is.EqualTo("Golden State Warriors"));
            Assert.That(result[3].Name, Is.EqualTo("LA Lakers"));
            Assert.That(result[4].Name, Is.EqualTo("Milwaukee Bucks"));
            Assert.That(result[5].Name, Is.EqualTo("Phoenix Suns"));
        }

        //For GetPlayerInputModelWithLoadedTeamsAsync
        [Test]
        public async Task GetPlayerInputModelWithLoadedTeamsAsync_ReturnsPlayerInputDtoWithTeams()
        {
            _playerRepositoryMock.Setup(r => r.GetAllTeamsNoTracking())
                .Returns(_teams.BuildMock());

            // Act
            PlayerInputDto result = await _playerService.GetPlayerInputModelWithLoadedTeamsAsync();

            // Assert
            Assert.That(result, Is.Not.Null);

            Assert.That(result.Teams.Count(), Is.EqualTo(_teams.Count));

            Assert.That(result.Teams.ElementAt(0).Name, Is.EqualTo("Boston Celtics"));
            Assert.That(result.Teams.ElementAt(1).Name, Is.EqualTo("Denver Nuggets"));
            Assert.That(result.Teams.ElementAt(2).Name, Is.EqualTo("Golden State Warriors"));
            Assert.That(result.Teams.ElementAt(3).Name, Is.EqualTo("LA Lakers"));
            Assert.That(result.Teams.ElementAt(4).Name, Is.EqualTo("Milwaukee Bucks"));
            Assert.That(result.Teams.ElementAt(5).Name, Is.EqualTo("Phoenix Suns"));

        }

        //For CreatePlayerAsync
        [Test]
        public async Task CreatePlayerAsync_ValidInput_CallsRepositoryAddPlayerAsync_SavesPlayerToRepository()
        {
            //Arrange
            _playerRepositoryMock.Setup(r => r.AddPlayerAsync(It.IsAny<Player>())) //matches any Player object passed to this method, regardless of its property values.
                .ReturnsAsync(true);

            PlayerInputDto playerInputDto = new PlayerInputDto
            {
                FirstName = "LeBron",
                LastName = "James",
                Position = Position.SmallForward,
                PointsPerGame = 27.5,
                ReboundsPerGame = 7.3,
                AssistsPerGame = 8.1,
                TeamId = 1
            };

            //Act & Assert
            await _playerService.CreatePlayerAsync(playerInputDto, "userId");

            _playerRepositoryMock.Verify(
                r => r.AddPlayerAsync(It.Is<Player>(p => p.FirstName == playerInputDto.FirstName &&
                p.LastName == playerInputDto.LastName &&
                p.UserId == "userId")), Times.Once
                );
        }

        [Test]
        public async Task CreatePlayerAsync_InValidInput_CallsRepositoryAddPlayerAsync_Throws()
        {
            //Arrange
            _playerRepositoryMock.Setup(r => r.AddPlayerAsync(It.IsAny<Player>())) //matches any Player object passed to this method, regardless of its property values.
                .ReturnsAsync(false);

            PlayerInputDto playerInputDto = new PlayerInputDto
            {
                FirstName = "LeBron",
                LastName = "James",
                Position = Position.SmallForward,
                PointsPerGame = 27.5,
                ReboundsPerGame = 7.3,
                AssistsPerGame = 8.1,
                TeamId = 1
            };

            //Act & Assert
            DbUpdateException ex = Assert.ThrowsAsync<DbUpdateException>(() => _playerService
            .CreatePlayerAsync(playerInputDto, "userId"));

            Assert.That(ex.Message, Is.EqualTo(PlayerAddToDatabaseServiceError));

            _playerRepositoryMock.Verify(r => r.AddPlayerAsync(It.IsAny<Player>()), Times.Once);
        }

        //For GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync

        [TestCase(1, "LeBron", "James", Position.SmallForward, 27.2, 7.5, 8.3)]
        [TestCase(3, "Kevin", "Durant", Position.PowerForward, 28.1, 6.8, 5.2)]
        public async Task GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync_ValidId_ReturnsPlayerInputDtoWithTeamsAndPlayerData
            (int playerId, string expectedFirstName, string expectedLastName, Position expectedPosition
            , double expectedPointsPerGame, double expectedReboundsPerGame, double expectedAssistsPerGame)
        {
            //Arrange
            _playerRepositoryMock.Setup(r => r.GetPlayerWithTeamByIdNoTrackingAsync(playerId))
                .ReturnsAsync(_players.SingleOrDefault(p => p.Id == playerId));

            _playerRepositoryMock.Setup(r => r.GetAllTeamsNoTracking())
                .Returns(_teams.BuildMock());

            // Act
            PlayerInputDto result = await _playerService.GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync(playerId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.FirstName, Is.EqualTo(expectedFirstName));
            Assert.That(result.LastName, Is.EqualTo(expectedLastName));
            Assert.That(result.Position, Is.EqualTo(expectedPosition));
            Assert.That(result.PointsPerGame, Is.EqualTo(expectedPointsPerGame));
            Assert.That(result.ReboundsPerGame, Is.EqualTo(expectedReboundsPerGame));
            Assert.That(result.AssistsPerGame, Is.EqualTo(expectedAssistsPerGame));
            Assert.That(result.Teams.Count(), Is.EqualTo(_teams.Count));
        }

        [TestCase(99)]//matches no player (invalid id cases)
        [TestCase(-1000)]
        public async Task GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync_InvalidId_ReturnsNull(int playerId)
        {
            //Arrange
            _playerRepositoryMock.Setup(r => r.GetPlayerWithTeamByIdNoTrackingAsync(playerId))
                .ReturnsAsync(_players.SingleOrDefault(p => p.Id == playerId));

            _playerRepositoryMock.Setup(r => r.GetAllTeamsNoTracking())
                .Returns(_teams.BuildMock());
            
            // Act
            PlayerInputDto result = await _playerService.GetPlayerInputModelWithLoadedTeamsAndPlayerDataAsync(playerId);

            // Assert
            Assert.That(result, Is.Null);
        }


        //For PlayerExistsAsync

        [TestCase(4, true)]
        [TestCase(1, true)]
        [TestCase(-1, false)]
        [TestCase(91, false)]
        public async Task PlayerExistsAsync_ReturnsCorrectResult(int playerId, bool expectedResult)
        {
            //Arrange
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(_players.BuildMock());

            // Act
            bool result = await _playerService.PlayerExistsAsync(playerId);


            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        //For EditPlayerAsync

        [TestCase(-1)]
        [TestCase(91)]
        public async Task EditPlayerAsync_WhenNoPlayerIsFound_ThrowsException(int playerId)
        {
            //Arrange
            _playerRepositoryMock.Setup(r => r.GetAllPlayers())
                .Returns(_players.BuildMock());

            PlayerInputDto inputDto = new PlayerInputDto()
            {
                FirstName = "LeBron",
                LastName = "James",
                Position = Position.SmallForward,
                PointsPerGame = 27.5,
                ReboundsPerGame = 7.3,
                AssistsPerGame = 8.1,
                TeamId = 1
            };

            //Act & Assert

            ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(
                () => _playerService.EditPlayerAsync(playerId, new PlayerInputDto())
            );

            Assert.That(ex.Message, Is.EqualTo(PlayerWithProvidedIdDoesNotExistServiceError));
        }

        [Test]
        public async Task EditPlayerAsync_WhenPlayerExists_UpdatesSuccessfully()
        {
            //Arrange
            Player existingPlayer = new Player
            {
                Id = 1,
                FirstName = "LeBron",
                LastName = "James",
                Position = Position.SmallForward,
                PointsPerGame = 27.2,
                ReboundsPerGame = 7.5,
                AssistsPerGame = 8.3,
                TeamId = 1,
                Team = new Team { Id = 1, Name = "LA Lakers" },
                UserId = "user-1",
                User = new ApplicationUser { Id = "user-1", UserName = "lebron23" }
            };

            List<Player> players = new List<Player>() { existingPlayer };

            _playerRepositoryMock.Setup(r => r.GetAllPlayers())
                .Returns(players.BuildMock());

            _playerRepositoryMock.Setup(r => r.UpdatePlayerAsync(It.IsAny<Player>()))
                .Returns(Task.CompletedTask);

            PlayerInputDto inputDto = new PlayerInputDto()
            {
                FirstName = "LeBroon",
                LastName = "Jamess",
                Position = Position.PointGuard,
                PointsPerGame = 27.5,
                ReboundsPerGame = 7.3,
                AssistsPerGame = 8.1,
                TeamId = 4
            };

            //Act
            await _playerService.EditPlayerAsync(existingPlayer.Id ,inputDto);

            //Assert
            Assert.That(existingPlayer.FirstName, Is.EqualTo("LeBroon"));
            Assert.That(existingPlayer.LastName, Is.EqualTo("Jamess"));
            Assert.That(existingPlayer.Position, Is.EqualTo(Position.PointGuard));
            Assert.That(existingPlayer.PointsPerGame, Is.EqualTo(27.5));
            Assert.That(existingPlayer.ReboundsPerGame, Is.EqualTo(7.3));
            Assert.That(existingPlayer.AssistsPerGame, Is.EqualTo(8.1));
            Assert.That(existingPlayer.TeamId, Is.EqualTo(4));

            _playerRepositoryMock.Verify(
                r => r.UpdatePlayerAsync(It.Is<Player>(p => p.Id == 1)), Times.Once
                );

            /*
              verifies that UpdatePlayerAsync is called with a player whose
              id mathes the id we want has been called exactly one time
              Guards against the case where fields are updated in memory
              but never actually saved to the database.
            */
        }

        //For GetPlayerForDeleteByIdAsync
        [TestCase(0)]
        [TestCase(-12)]
        [TestCase(-100)]
        public async Task GetPlayerForDeleteByIdAsync_WhenIdIsInvalid_ReturnsNull(int playerId)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(_players.BuildMock());

            DeletePlayerDto result = await _playerService.GetPlayerForDeleteByIdAsync(playerId);

            Assert.That(result, Is.Null);
        }

        [TestCase(1, "LeBron", "James")]
        [TestCase(3, "Kevin", "Durant")]
        [TestCase(5, "Nikola", "Jokic")]
        public async Task GetPlayerForDeleteByIdAsync_WhenIdIsValid_ReturnsDeletePlayerDto
            (int playerId, string expectedFirstName, string expectedLastName)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(_players.BuildMock());

            DeletePlayerDto result = await _playerService.GetPlayerForDeleteByIdAsync(playerId);

            Assert.That(result.FirstName, Is.EqualTo(expectedFirstName));
            Assert.That(result.LastName, Is.EqualTo(expectedLastName));
        }

        //For DeletePlayerAsync

        [TestCase(0)]
        [TestCase(-14)]
        [TestCase(-1000)]
        public async Task DeletePlayerAsync_WhenPlayerDoesNotExist_Throws(int playerId)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayers())
               .Returns(_players.BuildMock());

            ArgumentException ex = Assert.ThrowsAsync<ArgumentException>(
                () => _playerService.DeletePlayerAsync(playerId)
            );

            Assert.That(ex.Message, Is.EqualTo(PlayerWithProvidedIdDoesNotExistServiceError));
        }

        [TestCase(1)]
        [TestCase(4)]
        public async Task DeletePlayerAsync_WhenPlayerExists_DeletesPlayerFromDb
            (int playerId)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayers())
              .Returns(_players.BuildMock());

            await _playerService.DeletePlayerAsync(playerId);

            Player playerToDelete = _players.SingleOrDefault(p => p.Id == playerId);

            _playerRepositoryMock.Verify(r => r.DeletePlayerFromDbAsync(playerToDelete), Times.Once);
        }

        //For IsPlayerOwnedByUserAsync
        [TestCase(1, "addadad")]
        [TestCase(2, "azis")]
        [TestCase(-1, "marcus_brown")]
        [TestCase(-14, "marcusBrown")]
        public async Task IsPlayerOwnedByUserAsync_WhenPlayerDoesntExists_ReturnsFalse(int playerId, string userId)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(_players.BuildMock());

            bool result = await _playerService.IsPlayerOwnedByUserAsync(playerId, userId);

            Assert.That(result, Is.False);
        }

        [TestCase(1, "user-1")]
        [TestCase(2, "user-2")]
        [TestCase(5, "user-5")]
        [TestCase(4, "user-4")]
        public async Task IsPlayerOwnedByUserAsync_WhenPlayerExists_ReturnsTrue(int playerId, string userId)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(_players.BuildMock());

            bool result = await _playerService.IsPlayerOwnedByUserAsync(playerId, userId);

            Assert.That(result, Is.True);
        }

        //For GetUserPlayers

        [TestCase("user-7")]
        [TestCase("user-8")]
        public async Task GetUserPlayers_WhenUserHasNoPlayers_ReturnsEmptyIEnumerable(string userId)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(_players.BuildMock());

            IEnumerable<PlayerDto> result = await _playerService.GetUserPlayers(userId);

            Assert.That(result, Is.Empty);
        }


        [TestCase("user-1", 1, "LeBron", "James", Position.SmallForward)]
        [TestCase("user-4", 4, "Giannis", "Antetokounmpo",Position.PowerForward)]
        [TestCase("user-6", 6, "LeBron", "James", Position.SmallForward)]
        public async Task GetUserPlayers_WhenUserPlayers_ReturnsTheRightPlayers
            (string userId, int expectedPlayerId, string expectedFirstName, string expectedLastName, Position expectedPosition)
        {
            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(_players.BuildMock());

            IEnumerable<PlayerDto> result = await _playerService.GetUserPlayers(userId);

            PlayerDto userPlayer = result.First(); // since we have only one player per user for the tests

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(userPlayer.Id, Is.EqualTo(expectedPlayerId));
            Assert.That(userPlayer.FirstName, Is.EqualTo(expectedFirstName));
            Assert.That(userPlayer.LastName, Is.EqualTo(expectedLastName));
            Assert.That(userPlayer.Position, Is.EqualTo(PositionToString[expectedPosition]));
        }

        //For GetAllPlayersForAdminAsync
        [Test]
        public async Task GetAllPlayersForAdminAsync_ReturnsInTheCorrectOrder() 
        {
            List<Player> players = new List<Player>() //Arange
            {
                new Player
                {
                    Id = 1,
                    FirstName = "LeBron",
                    LastName = "James",
                    Position = Position.SmallForward,
                    PointsPerGame = 27.2,
                    ReboundsPerGame = 7.5,
                    AssistsPerGame = 8.3,
                    TeamId = 1,
                    Team = new Team { Id = 1, Name = "LA Lakers" },
                    UserId = "user-1",
                    User = new ApplicationUser { Id = "user-1", Email = "lebron23" }
                },
                new Player
                {
                    Id = 2,
                    FirstName = "LeBron",
                    LastName = "Anthony",
                    Position = Position.PointGuard,
                    PointsPerGame = 29.4,
                    ReboundsPerGame = 5.1,
                    AssistsPerGame = 6.3,
                    TeamId = 2,
                    Team = new Team { Id = 2, Name = "Golden State Warriors" },
                    UserId = "user-2",
                    User = new ApplicationUser { Id = "user-2", Email = "steph12", Nickname = "curry30" }
                },
                new Player
                {
                    Id = 3,
                    FirstName = "Kevin",
                    LastName = "Durant",
                    Position = Position.PowerForward,
                    PointsPerGame = 28.1,
                    ReboundsPerGame = 6.8,
                    AssistsPerGame = 5.2,
                    TeamId = 3,
                    Team = new Team { Id = 3, Name = "Phoenix Suns" },
                    UserId = "user-3",
                    User = new ApplicationUser { Id = "user-3",Email = "trey35", Nickname = "kd35" }
                },
                new Player
                {
                    Id = 4,
                    FirstName = "Giannis",
                    LastName = "Antetokounmpo",
                    Position = Position.PowerForward,
                    PointsPerGame = 31.1,
                    ReboundsPerGame = 11.8,
                    AssistsPerGame = 5.7,
                    TeamId = 4,
                    Team = new Team { Id = 4, Name = "Milwaukee Bucks" },
                    UserId = "user-4",
                    User = new ApplicationUser { Id = "user-4", Email = "zico",Nickname = "giannis34" }
                },
                new Player
                {
                    Id = 5,
                    FirstName = "Nikola",
                    LastName = "Jokic",
                    Position = Position.Center,
                    PointsPerGame = 26.4,
                    ReboundsPerGame = 12.4,
                    AssistsPerGame = 9.0,
                    TeamId = 5,
                    Team = new Team { Id = 5, Name = "Denver Nuggets" },
                    UserId = "user-5",
                    User = new ApplicationUser { Id = "user-5", Email = "oooo", Nickname = "jokic15" }
                },
                new Player
                {
                    Id = 6,
                    FirstName = "LeBron",
                    LastName = "James",
                    Position = Position.SmallForward,
                    PointsPerGame = 26.9,
                    ReboundsPerGame = 8.1,
                    AssistsPerGame = 4.6,
                    TeamId = 6,
                    Team = new Team { Id = 6, Name = "Boston Celtics" },
                    UserId = "user-6",
                    User = new ApplicationUser { Id = "user-6", Email = "jt000", Nickname = "tatum0" }
                },
                new Player
                {
                    Id = 7,
                    FirstName = "Alexander",
                    LastName = "Shopov",
                    Position = Position.SmallForward,
                    PointsPerGame = 99,
                    ReboundsPerGame = 10.1,
                    AssistsPerGame = 1.6,
                    TeamId = 6,
                    Team = new Team { Id = 6, Name = "Boston Celtics" },
                }
            };

            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(players.BuildMock());

            List<AdminPlayerDto> adminPlayers = (await _playerService.GetAllPlayersForAdminAsync()).ToList();

            //For the first
            Assert.That(adminPlayers.First().FirstName, Is.EqualTo("LeBron"));
            Assert.That(adminPlayers.First().LastName, Is.EqualTo("Anthony"));
            Assert.That(adminPlayers.First().CreatedByNickname, Is.EqualTo("curry30"));
            Assert.That(adminPlayers.First().CreatedByEmail, Is.EqualTo("steph12"));

            //For the last
            Assert.That(adminPlayers.Last().FirstName, Is.EqualTo("Alexander"));
            Assert.That(adminPlayers.Last().LastName, Is.EqualTo("Shopov"));
            Assert.That(adminPlayers.Last().CreatedByNickname, Is.Null);
            Assert.That(adminPlayers.Last().CreatedByNickname, Is.Null);
        }

        // For IsPlayerUserCreatedAsync
        [TestCase(3)]
        [TestCase(4)]
        public async Task IsPlayerUserCreatedAsync_WhenPlayerIsNotCreatedByUser_ReturnsFalse(int playerId)
        {
            List<Player> players = new List<Player>()
            {
                new Player
                {
                    Id = 1,
                    FirstName = "Nikola",
                    LastName = "Jokic",
                    Position = Position.Center,
                    PointsPerGame = 26.4,
                    ReboundsPerGame = 12.4,
                    AssistsPerGame = 9.0,
                    TeamId = 5,
                    Team = new Team { Id = 5, Name = "Denver Nuggets" },
                    UserId = "user-5",
                    User = new ApplicationUser { Id = "user-5", Email = "oooo", Nickname = "jokic15" }
                },
                new Player
                {
                    Id = 2,
                    FirstName = "LeBron",
                    LastName = "James",
                    Position = Position.SmallForward,
                    PointsPerGame = 26.9,
                    ReboundsPerGame = 8.1,
                    AssistsPerGame = 4.6,
                    TeamId = 6,
                    Team = new Team { Id = 6, Name = "Boston Celtics" },
                    UserId = "user-6",
                    User = new ApplicationUser { Id = "user-6", Email = "jt000", Nickname = "tatum0" }
                },
                new Player
                {
                    Id = 3,
                    FirstName = "Alexander",
                    LastName = "Shopov",
                    Position = Position.SmallForward,
                    PointsPerGame = 99,
                    ReboundsPerGame = 10.1,
                    AssistsPerGame = 1.6,
                    TeamId = 6,
                    Team = new Team { Id = 4, Name = "Botev" },
                },
                 new Player
                {
                    Id = 4,
                    FirstName = "Georgi",
                    LastName = "Vasilev",
                    Position = Position.Center,
                    PointsPerGame = 9,
                    ReboundsPerGame = 10.1,
                    AssistsPerGame = 1.9,
                    TeamId = 6,
                    Team = new Team { Id = 3, Name = "Paisii" },
                }
            };

            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(players.BuildMock());

            bool result = await _playerService.IsPlayerUserCreatedAsync(playerId);

            Assert.That(result, Is.False);
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task IsPlayerUserCreatedAsync_WhenPlayerISCreatedByUser_ReturnsTrue(int playerId)
        {
            List<Player> players = new List<Player>()
            {
                new Player
                {
                    Id = 1,
                    FirstName = "Nikola",
                    LastName = "Jokic",
                    Position = Position.Center,
                    PointsPerGame = 26.4,
                    ReboundsPerGame = 12.4,
                    AssistsPerGame = 9.0,
                    TeamId = 5,
                    Team = new Team { Id = 5, Name = "Denver Nuggets" },
                    UserId = "user-5",
                    User = new ApplicationUser { Id = "user-5", Email = "oooo", Nickname = "jokic15" }
                },
                new Player
                {
                    Id = 2,
                    FirstName = "LeBron",
                    LastName = "James",
                    Position = Position.SmallForward,
                    PointsPerGame = 26.9,
                    ReboundsPerGame = 8.1,
                    AssistsPerGame = 4.6,
                    TeamId = 6,
                    Team = new Team { Id = 6, Name = "Boston Celtics" },
                    UserId = "user-6",
                    User = new ApplicationUser { Id = "user-6", Email = "jt000", Nickname = "tatum0" }
                },
                new Player
                {
                    Id = 3,
                    FirstName = "Alexander",
                    LastName = "Shopov",
                    Position = Position.SmallForward,
                    PointsPerGame = 99,
                    ReboundsPerGame = 10.1,
                    AssistsPerGame = 1.6,
                    TeamId = 6,
                    Team = new Team { Id = 4, Name = "Botev" },
                },
                 new Player
                {
                    Id = 4,
                    FirstName = "Georgi",
                    LastName = "Vasilev",
                    Position = Position.Center,
                    PointsPerGame = 9,
                    ReboundsPerGame = 10.1,
                    AssistsPerGame = 1.9,
                    TeamId = 6,
                    Team = new Team { Id = 3, Name = "Paisii" },
                }
            };

            _playerRepositoryMock.Setup(r => r.GetAllPlayersNoTracking())
                .Returns(players.BuildMock());

            bool result = await _playerService.IsPlayerUserCreatedAsync(playerId);

            Assert.That(result, Is.True);
        }
    }
}
