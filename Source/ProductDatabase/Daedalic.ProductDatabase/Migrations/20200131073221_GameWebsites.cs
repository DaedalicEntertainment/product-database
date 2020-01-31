using Microsoft.EntityFrameworkCore.Migrations;

namespace Daedalic.ProductDatabase.Migrations
{
    public partial class GameWebsites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacebookPageName",
                table: "Game",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TwitterHandle",
                table: "Game",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Game",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FacebookPageName",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "TwitterHandle",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Game");
        }
    }
}
