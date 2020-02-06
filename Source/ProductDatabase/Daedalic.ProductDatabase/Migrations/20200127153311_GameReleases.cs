using Microsoft.EntityFrameworkCore.Migrations;

namespace Daedalic.ProductDatabase.Migrations
{
    public partial class GameReleases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Release",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Release",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Release_GameId",
                table: "Release",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Release_Game_GameId",
                table: "Release",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Release_Game_GameId",
                table: "Release");

            migrationBuilder.DropIndex(
                name: "IX_Release_GameId",
                table: "Release");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Release");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Release",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
