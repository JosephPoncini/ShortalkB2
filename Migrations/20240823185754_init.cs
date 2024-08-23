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
                name: "LobbyInfo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LobbyName = table.Column<string>(type: "TEXT", nullable: false),
                    Host = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfRounds = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeLimit = table.Column<int>(type: "INTEGER", nullable: false),
                    TeamMemberA1 = table.Column<string>(type: "TEXT", nullable: false),
                    TeamMemberA2 = table.Column<string>(type: "TEXT", nullable: false),
                    TeamMemberA3 = table.Column<string>(type: "TEXT", nullable: false),
                    TeamMemberA4 = table.Column<string>(type: "TEXT", nullable: false),
                    TeamMemberA5 = table.Column<string>(type: "TEXT", nullable: false),
                    TeamMemberB1 = table.Column<string>(type: "TEXT", nullable: false),
                    TeamMemberB2 = table.Column<string>(type: "TEXT", nullable: false),
                    TeamMemberB3 = table.Column<string>(type: "TEXT", nullable: false),
                    TeamMemberB4 = table.Column<string>(type: "TEXT", nullable: false),
                    TeamMemberB5 = table.Column<string>(type: "TEXT", nullable: false),
                    ReadyStatusA1 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusA2 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusA3 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusA4 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusA5 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB1 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB2 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB3 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB4 = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadyStatusB5 = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LobbyInfo", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LobbyInfo");
        }
    }
}
