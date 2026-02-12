using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EuroLeaguePlayerBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TitlesWon = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    City = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoachId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    PointsPerGame = table.Column<double>(type: "float", nullable: false),
                    ReboundsPerGame = table.Column<double>(type: "float", nullable: false),
                    AssistsPerGame = table.Column<double>(type: "float", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "FirstName", "LastName", "TitlesWon" },
                values: new object[,]
                {
                    { 1, "Dimitris", "Itoudis", 5 },
                    { 2, "Georgios", "Bartzokas", 3 },
                    { 3, "Pablo", "Laso", 4 },
                    { 4, "Sarunas", "Jasikevicius", 2 },
                    { 5, "Dejan", "Radonjic", 1 },
                    { 6, "Zvezdan", "Mitrovic", 1 }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "City", "CoachId", "Country", "LogoPath", "Name" },
                values: new object[,]
                {
                    { 1, "Athens", 1, "Greece", "/images/panathinaikos.svg.png", "Panathinaikos" },
                    { 2, "Piraeus", 2, "Greece", "/images/olympiacos.svg.png", "Olympiacos" },
                    { 3, "Madrid", 3, "Spain", "/images/real_madrid.svg.png", "Real Madrid" },
                    { 4, "Barcelona", 4, "Spain", "/images/barcelona.svg.png", "Barcelona" },
                    { 5, "Belgrade", 5, "Serbia", "/images/crvena_zvezda.svg.png", "Crvena Zvezda" },
                    { 6, "Belgrade", 6, "Serbia", "/images/partizan.svg.png", "Partizan" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "AssistsPerGame", "FirstName", "LastName", "PointsPerGame", "Position", "ReboundsPerGame", "TeamId" },
                values: new object[,]
                {
                    { 1, 2.1000000000000001, "Ioannis", "Papapetrou", 12.5, 2, 4.2999999999999998, 1 },
                    { 2, 7.4000000000000004, "Nick", "Calathes", 11.199999999999999, 0, 3.5, 1 },
                    { 3, 1.3, "Dimitris", "Mitoglou", 10.1, 3, 5.2000000000000002, 1 },
                    { 4, 1.2, "DeShaun", "Thomas", 9.3000000000000007, 2, 4.0, 1 },
                    { 5, 0.90000000000000002, "Zach", "LeDay", 8.6999999999999993, 4, 5.5, 1 },
                    { 6, 0.5, "Moustapha", "Fall", 5.2000000000000002, 4, 4.7000000000000002, 1 },
                    { 7, 2.1000000000000001, "Sasha", "Vezenkov", 15.300000000000001, 3, 5.7999999999999998, 2 },
                    { 8, 1.5, "Kostas", "Papanikolaou", 10.199999999999999, 2, 4.0999999999999996, 2 },
                    { 9, 2.2999999999999998, "Giannoulis", "Larentzakis", 11.0, 1, 3.0, 2 },
                    { 10, 1.0, "Georgios", "Printezis", 9.5, 1, 4.7999999999999998, 2 },
                    { 11, 1.1000000000000001, "Shaquielle", "McKissic", 8.1999999999999993, 2, 3.2000000000000002, 2 },
                    { 12, 0.80000000000000004, "Othello", "Hunter", 7.5, 4, 5.0, 2 },
                    { 13, 5.5, "Sergio", "Llull", 12.300000000000001, 0, 2.3999999999999999, 3 },
                    { 14, 0.69999999999999996, "Walter", "Tavares", 9.0, 4, 6.7999999999999998, 3 },
                    { 15, 1.2, "Anthony", "Rudy", 10.5, 2, 4.2999999999999998, 3 },
                    { 16, 2.0, "Edgar", "Sosa", 8.6999999999999993, 1, 2.1000000000000001, 3 },
                    { 17, 1.0, "Guerschon", "Yabusele", 9.1999999999999993, 2, 3.7999999999999998, 3 },
                    { 18, 2.1000000000000001, "Fabien", "Causeur", 11.0, 1, 2.5, 3 },
                    { 19, 1.6000000000000001, "Nikola", "Mirotic", 13.1, 3, 5.4000000000000004, 4 },
                    { 20, 6.7999999999999998, "Nick", "Calathes", 11.0, 0, 3.2000000000000002, 4 },
                    { 21, 1.1000000000000001, "Brandon", "Davies", 10.199999999999999, 4, 5.7000000000000002, 4 },
                    { 22, 2.2999999999999998, "Leandro", "Bolmaro", 9.5, 1, 2.7999999999999998, 4 },
                    { 23, 1.5, "Jaka", "Blazic", 8.9000000000000004, 0, 2.0, 4 },
                    { 24, 0.69999999999999996, "Pierre", "Oriola", 7.7999999999999998, 3, 4.0, 4 },
                    { 25, 1.8999999999999999, "Ognjen", "Dobric", 10.0, 1, 2.5, 5 },
                    { 26, 0.80000000000000004, "Filip", "Perrin", 8.3000000000000007, 4, 4.5999999999999996, 5 },
                    { 27, 1.2, "Branko", "Lazić", 7.9000000000000004, 1, 2.0, 5 },
                    { 28, 1.3, "Jordan", "Lyles", 9.0999999999999996, 2, 3.1000000000000001, 5 },
                    { 29, 4.0, "Corey", "Walden", 11.199999999999999, 0, 2.5, 5 },
                    { 30, 1.0, "Marko", "Simonovic", 10.4, 3, 5.2999999999999998, 5 },
                    { 31, 2.5, "Kevin", "Punter", 12.199999999999999, 1, 3.1000000000000001, 6 },
                    { 32, 0.90000000000000002, "Mathias", "Lessort", 9.5, 4, 5.2999999999999998, 6 },
                    { 33, 1.2, "Nikola", "Jovic", 10.0, 2, 3.7000000000000002, 6 },
                    { 34, 0.80000000000000004, "Zlatko", "Racic", 8.5, 3, 4.2000000000000002, 6 },
                    { 35, 1.5, "Ognjen", "Jaramaz", 9.0, 1, 2.7999999999999998, 6 },
                    { 36, 3.8999999999999999, "Shawn", "Hines", 11.1, 0, 2.5, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CoachId",
                table: "Teams",
                column: "CoachId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Coaches");
        }
    }
}
