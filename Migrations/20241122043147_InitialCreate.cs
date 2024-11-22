using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShortalkB2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GamePhase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Host = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Round = table.Column<int>(type: "int", nullable: false),
                    NumberOfRounds = table.Column<int>(type: "int", nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false),
                    TeamAScore = table.Column<int>(type: "int", nullable: false),
                    TeamBScore = table.Column<int>(type: "int", nullable: false),
                    Speaker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurnNumber = table.Column<int>(type: "int", nullable: false),
                    OnePointWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThreePointWord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerA1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerA2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerA3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerA4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerA5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerB1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerB2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerB3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerB4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayerB5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadyStatusA1 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusA2 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusA3 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusA4 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusA5 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB1 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB2 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB3 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB4 = table.Column<bool>(type: "bit", nullable: false),
                    ReadyStatusB5 = table.Column<bool>(type: "bit", nullable: false),
                    OnePointWordHasBeenSaid = table.Column<bool>(type: "bit", nullable: false),
                    ThreePointWordHasBeenSaid = table.Column<bool>(type: "bit", nullable: false),
                    BuzzWords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkippedWords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnePointWords = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThreePointWords = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameInfo", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameInfo");
        }
    }
}
