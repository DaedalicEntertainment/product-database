using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Daedalic.ProductDatabase.Migrations
{
    public partial class ReadyForReleaseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReadyForReleaseDate",
                table: "Release",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadyForReleaseDate",
                table: "Release");
        }
    }
}
