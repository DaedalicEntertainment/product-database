using Microsoft.EntityFrameworkCore.Migrations;

namespace Daedalic.ProductDatabase.Migrations
{
    public partial class GameLanguages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImplementedLanguage_LanguageType_LanguageTypeId",
                table: "ImplementedLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_ImplementedLanguage_Release_ReleaseId",
                table: "ImplementedLanguage");

            migrationBuilder.DropIndex(
                name: "IX_ImplementedLanguage_LanguageTypeId",
                table: "ImplementedLanguage");

            migrationBuilder.DropIndex(
                name: "IX_ImplementedLanguage_ReleaseId",
                table: "ImplementedLanguage");

            migrationBuilder.DropColumn(
                name: "LanguageTypeId",
                table: "ImplementedLanguage");

            migrationBuilder.DropColumn(
                name: "ReleaseId",
                table: "ImplementedLanguage");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "ImplementedLanguage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LanguageStatusId",
                table: "ImplementedLanguage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LanguageStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReleasedLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(nullable: false),
                    ReleaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleasedLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleasedLanguage_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReleasedLanguage_Release_ReleaseId",
                        column: x => x.ReleaseId,
                        principalTable: "Release",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportedLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(nullable: false),
                    LanguageTypeId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportedLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportedLanguage_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportedLanguage_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupportedLanguage_LanguageType_LanguageTypeId",
                        column: x => x.LanguageTypeId,
                        principalTable: "LanguageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImplementedLanguage_GameId",
                table: "ImplementedLanguage",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ImplementedLanguage_LanguageStatusId",
                table: "ImplementedLanguage",
                column: "LanguageStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleasedLanguage_LanguageId",
                table: "ReleasedLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleasedLanguage_ReleaseId",
                table: "ReleasedLanguage",
                column: "ReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedLanguage_GameId",
                table: "SupportedLanguage",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedLanguage_LanguageId",
                table: "SupportedLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportedLanguage_LanguageTypeId",
                table: "SupportedLanguage",
                column: "LanguageTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImplementedLanguage_Game_GameId",
                table: "ImplementedLanguage",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImplementedLanguage_LanguageStatus_LanguageStatusId",
                table: "ImplementedLanguage",
                column: "LanguageStatusId",
                principalTable: "LanguageStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImplementedLanguage_Game_GameId",
                table: "ImplementedLanguage");

            migrationBuilder.DropForeignKey(
                name: "FK_ImplementedLanguage_LanguageStatus_LanguageStatusId",
                table: "ImplementedLanguage");

            migrationBuilder.DropTable(
                name: "LanguageStatus");

            migrationBuilder.DropTable(
                name: "ReleasedLanguage");

            migrationBuilder.DropTable(
                name: "SupportedLanguage");

            migrationBuilder.DropIndex(
                name: "IX_ImplementedLanguage_GameId",
                table: "ImplementedLanguage");

            migrationBuilder.DropIndex(
                name: "IX_ImplementedLanguage_LanguageStatusId",
                table: "ImplementedLanguage");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "ImplementedLanguage");

            migrationBuilder.DropColumn(
                name: "LanguageStatusId",
                table: "ImplementedLanguage");

            migrationBuilder.AddColumn<int>(
                name: "LanguageTypeId",
                table: "ImplementedLanguage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReleaseId",
                table: "ImplementedLanguage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ImplementedLanguage_LanguageTypeId",
                table: "ImplementedLanguage",
                column: "LanguageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImplementedLanguage_ReleaseId",
                table: "ImplementedLanguage",
                column: "ReleaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImplementedLanguage_LanguageType_LanguageTypeId",
                table: "ImplementedLanguage",
                column: "LanguageTypeId",
                principalTable: "LanguageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImplementedLanguage_Release_ReleaseId",
                table: "ImplementedLanguage",
                column: "ReleaseId",
                principalTable: "Release",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
