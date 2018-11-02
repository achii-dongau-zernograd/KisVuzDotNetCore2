using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class NirSpecial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NirSpecials",
                columns: table => new
                {
                    NirSpecialId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NirSpecialCode = table.Column<string>(nullable: true),
                    NirSpecialName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NirSpecials", x => x.NirSpecialId);
                });

            migrationBuilder.CreateTable(
                name: "ArticleNirSpecials",
                columns: table => new
                {
                    ArticleNirSpecialId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false),
                    NirSpecialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleNirSpecials", x => x.ArticleNirSpecialId);
                    table.ForeignKey(
                        name: "FK_ArticleNirSpecials_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleNirSpecials_NirSpecials_NirSpecialId",
                        column: x => x.NirSpecialId,
                        principalTable: "NirSpecials",
                        principalColumn: "NirSpecialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleNirSpecials_ArticleId",
                table: "ArticleNirSpecials",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleNirSpecials_NirSpecialId",
                table: "ArticleNirSpecials",
                column: "NirSpecialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleNirSpecials");

            migrationBuilder.DropTable(
                name: "NirSpecials");
        }
    }
}
