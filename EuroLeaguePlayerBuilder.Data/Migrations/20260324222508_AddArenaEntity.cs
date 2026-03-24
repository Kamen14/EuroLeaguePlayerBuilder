using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EuroLeaguePlayerBuilder.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddArenaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArenaId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Arenas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arenas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arenas_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Arenas",
                columns: new[] { "Id", "Capacity", "City", "Country", "ImagePath", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, 19443, "Athens", "Greece", "/images/arenas/oaka.jpg", "OAKA", null },
                    { 2, 12000, "Piraeus", "Greece", "/images/arenas/peace_and_friendship_stadium.jpg", "Peace and Friendship Stadium", null },
                    { 3, 15000, "Madrid", "Spain", "/images/arenas/wizink_center.jpg", "WiZink Center", null },
                    { 4, 7585, "Barcelona", "Spain", "/images/arenas/palau_blaugrana.jpg", "Palau Blaugrana", null },
                    { 5, 18386, "Belgrade", "Serbia", "/images/arenas/belgrade_arena.jpg", "Belgrade Arena", null }
                });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArenaId", "LogoPath" },
                values: new object[] { 1, "/images/teams/panathinaikos.svg.png" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArenaId", "LogoPath" },
                values: new object[] { 2, "/images/teams/olympiacos.svg.png" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ArenaId", "LogoPath" },
                values: new object[] { 3, "/images/teams/real_madrid.svg.png" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArenaId", "LogoPath" },
                values: new object[] { 4, "/images/teams/barcelona.svg.png" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ArenaId", "LogoPath" },
                values: new object[] { 5, "/images/teams/crvena_zvezda.svg.png" });

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ArenaId", "LogoPath" },
                values: new object[] { 5, "/images/teams/partizan.svg.png" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ArenaId",
                table: "Teams",
                column: "ArenaId");

            migrationBuilder.CreateIndex(
                name: "IX_Arenas_UserId",
                table: "Arenas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Arenas_ArenaId",
                table: "Teams",
                column: "ArenaId",
                principalTable: "Arenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Arenas_ArenaId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Arenas");

            migrationBuilder.DropIndex(
                name: "IX_Teams_ArenaId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ArenaId",
                table: "Teams");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1,
                column: "LogoPath",
                value: "/images/panathinaikos.svg.png");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2,
                column: "LogoPath",
                value: "/images/olympiacos.svg.png");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3,
                column: "LogoPath",
                value: "/images/real_madrid.svg.png");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4,
                column: "LogoPath",
                value: "/images/barcelona.svg.png");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5,
                column: "LogoPath",
                value: "/images/crvena_zvezda.svg.png");

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6,
                column: "LogoPath",
                value: "/images/partizan.svg.png");
        }
    }
}
