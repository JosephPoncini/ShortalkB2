using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShortalkB2.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomName = table.Column<string>(type: "TEXT", nullable: true),
                    GamePhase = table.Column<string>(type: "TEXT", nullable: true),
                    Host = table.Column<string>(type: "TEXT", nullable: true),
                    Round = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfRounds = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    Time = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamAScore = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamBScore = table.Column<int>(type: "INTEGER", nullable: false),
                    Speaker = table.Column<string>(type: "TEXT", nullable: true),
                    TurnNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    OnePointWord = table.Column<string>(type: "TEXT", nullable: true),
                    ThreePointWord = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerA1 = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerA2 = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerA3 = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerA4 = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerA5 = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerB1 = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerB2 = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerB3 = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerB4 = table.Column<string>(type: "TEXT", nullable: true),
                    PlayerB5 = table.Column<string>(type: "TEXT", nullable: true),
                    ReadyStatusA1 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusA2 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusA3 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusA4 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusA5 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB1 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB2 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB3 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB4 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB5 = table.Column<bool>(type: "INTEGER", nullable: false),
                    OnePointWordHasBeenSaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    ThreePointWordHasBeenSaid = table.Column<bool>(type: "INTEGER", nullable: false),
                    BuzzWords = table.Column<string>(type: "TEXT", nullable: true),
                    SkippedWords = table.Column<string>(type: "TEXT", nullable: true),
                    OnePointWords = table.Column<string>(type: "TEXT", nullable: true),
                    ThreePointWords = table.Column<string>(type: "TEXT", nullable: true)
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
