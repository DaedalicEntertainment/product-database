using Microsoft.EntityFrameworkCore.Migrations;

namespace Daedalic.ProductDatabase.Migrations
{
    public partial class ReleaseGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReleaseGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReleaseInReleaseGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseId = table.Column<int>(nullable: false),
                    ReleaseGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseInReleaseGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleaseInReleaseGroup_ReleaseGroup_ReleaseGroupId",
                        column: x => x.ReleaseGroupId,
                        principalTable: "ReleaseGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReleaseInReleaseGroup_Release_ReleaseId",
                        column: x => x.ReleaseId,
                        principalTable: "Release",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseInReleaseGroup_ReleaseGroupId",
                table: "ReleaseInReleaseGroup",
                column: "ReleaseGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseInReleaseGroup_ReleaseId",
                table: "ReleaseInReleaseGroup",
                column: "ReleaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReleaseInReleaseGroup");

            migrationBuilder.DropTable(
                name: "ReleaseGroup");
        }
    }
}
