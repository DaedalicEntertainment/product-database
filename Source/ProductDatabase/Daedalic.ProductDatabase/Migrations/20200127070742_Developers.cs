using Microsoft.EntityFrameworkCore.Migrations;

namespace Daedalic.ProductDatabase.Migrations
{
    public partial class Developers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Game",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "Game",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_DeveloperId",
                table: "Game",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Developer_DeveloperId",
                table: "Game",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Developer_DeveloperId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "Developer");

            migrationBuilder.DropIndex(
                name: "IX_Game_DeveloperId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Game");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Game",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
