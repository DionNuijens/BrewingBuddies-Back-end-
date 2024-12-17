using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrewingBuddies_DataService.Migrations
{
    /// <inheritdoc />
    public partial class RiotId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RiotId",
                table: "LeagueUsers",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RiotId",
                table: "LeagueUsers");
        }
    }
}
