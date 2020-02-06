using Microsoft.EntityFrameworkCore.Migrations;

namespace Daedalic.ProductDatabase.Migrations
{
    public partial class LanguageReleaseStatusOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Release_Platform_PlatformId",
                table: "Release");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "ReleaseStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PlatformId",
                table: "Release",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "LanguageStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Release_Platform_PlatformId",
                table: "Release",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Release_Platform_PlatformId",
                table: "Release");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "ReleaseStatus");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "LanguageStatus");

            migrationBuilder.AlterColumn<int>(
                name: "PlatformId",
                table: "Release",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Release_Platform_PlatformId",
                table: "Release",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
