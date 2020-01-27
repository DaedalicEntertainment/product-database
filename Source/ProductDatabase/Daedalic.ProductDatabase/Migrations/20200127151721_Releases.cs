using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Daedalic.ProductDatabase.Migrations
{
    public partial class Releases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReleaseStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Release",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Summary = table.Column<string>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    ReleaseStatusId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: true),
                    PlatformId = table.Column<int>(nullable: true),
                    StoreId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Release", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Release_Platform_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Release_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publisher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Release_ReleaseStatus_ReleaseStatusId",
                        column: x => x.ReleaseStatusId,
                        principalTable: "ReleaseStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Release_Store_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImplementedLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<int>(nullable: false),
                    LanguageTypeId = table.Column<int>(nullable: false),
                    ReleaseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImplementedLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImplementedLanguage_Language_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImplementedLanguage_LanguageType_LanguageTypeId",
                        column: x => x.LanguageTypeId,
                        principalTable: "LanguageType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImplementedLanguage_Release_ReleaseId",
                        column: x => x.ReleaseId,
                        principalTable: "Release",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImplementedLanguage_LanguageId",
                table: "ImplementedLanguage",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ImplementedLanguage_LanguageTypeId",
                table: "ImplementedLanguage",
                column: "LanguageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ImplementedLanguage_ReleaseId",
                table: "ImplementedLanguage",
                column: "ReleaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Release_PlatformId",
                table: "Release",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_Release_PublisherId",
                table: "Release",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Release_ReleaseStatusId",
                table: "Release",
                column: "ReleaseStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Release_StoreId",
                table: "Release",
                column: "StoreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImplementedLanguage");

            migrationBuilder.DropTable(
                name: "Release");

            migrationBuilder.DropTable(
                name: "ReleaseStatus");
        }
    }
}
