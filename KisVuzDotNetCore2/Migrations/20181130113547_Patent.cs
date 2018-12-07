using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class Patent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatentVidGroups",
                columns: table => new
                {
                    PatentVidGroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatentVidGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentVidGroups", x => x.PatentVidGroupId);
                });

            migrationBuilder.CreateTable(
                name: "PatentVids",
                columns: table => new
                {
                    PatentVidId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatentVidGroupId = table.Column<int>(nullable: false),
                    PatentVidName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentVids", x => x.PatentVidId);
                    table.ForeignKey(
                        name: "FK_PatentVids_PatentVidGroups_PatentVidGroupId",
                        column: x => x.PatentVidGroupId,
                        principalTable: "PatentVidGroups",
                        principalColumn: "PatentVidGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patents",
                columns: table => new
                {
                    PatentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsACHII = table.Column<bool>(nullable: false),
                    IsZarubejn = table.Column<bool>(nullable: false),
                    PatentName = table.Column<string>(nullable: true),
                    PatentNumber = table.Column<string>(nullable: true),
                    PatentOwner = table.Column<string>(nullable: true),
                    PatentVidId = table.Column<int>(nullable: true),
                    YearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patents", x => x.PatentId);
                    table.ForeignKey(
                        name: "FK_Patents_PatentVids_PatentVidId",
                        column: x => x.PatentVidId,
                        principalTable: "PatentVids",
                        principalColumn: "PatentVidId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patents_Years_YearId",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "YearId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatentAuthors",
                columns: table => new
                {
                    PatentAuthorId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    AuthorPart = table.Column<decimal>(nullable: false),
                    PatentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentAuthors", x => x.PatentAuthorId);
                    table.ForeignKey(
                        name: "FK_PatentAuthors_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentAuthors_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "PatentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentNirSpecials",
                columns: table => new
                {
                    PatentNirSpecialId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NirSpecialId = table.Column<int>(nullable: false),
                    PatentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentNirSpecials", x => x.PatentNirSpecialId);
                    table.ForeignKey(
                        name: "FK_PatentNirSpecials_NirSpecials_NirSpecialId",
                        column: x => x.NirSpecialId,
                        principalTable: "NirSpecials",
                        principalColumn: "NirSpecialId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentNirSpecials_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "PatentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatentNirTemas",
                columns: table => new
                {
                    PatentNirTemaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NirTemaId = table.Column<int>(nullable: false),
                    PatentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatentNirTemas", x => x.PatentNirTemaId);
                    table.ForeignKey(
                        name: "FK_PatentNirTemas_NirTema_NirTemaId",
                        column: x => x.NirTemaId,
                        principalTable: "NirTema",
                        principalColumn: "NirTemaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatentNirTemas_Patents_PatentId",
                        column: x => x.PatentId,
                        principalTable: "Patents",
                        principalColumn: "PatentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatentAuthors_AuthorId",
                table: "PatentAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentAuthors_PatentId",
                table: "PatentAuthors",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentNirSpecials_NirSpecialId",
                table: "PatentNirSpecials",
                column: "NirSpecialId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentNirSpecials_PatentId",
                table: "PatentNirSpecials",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentNirTemas_NirTemaId",
                table: "PatentNirTemas",
                column: "NirTemaId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentNirTemas_PatentId",
                table: "PatentNirTemas",
                column: "PatentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_PatentVidId",
                table: "Patents",
                column: "PatentVidId");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_YearId",
                table: "Patents",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_PatentVids_PatentVidGroupId",
                table: "PatentVids",
                column: "PatentVidGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatentAuthors");

            migrationBuilder.DropTable(
                name: "PatentNirSpecials");

            migrationBuilder.DropTable(
                name: "PatentNirTemas");

            migrationBuilder.DropTable(
                name: "Patents");

            migrationBuilder.DropTable(
                name: "PatentVids");

            migrationBuilder.DropTable(
                name: "PatentVidGroups");
        }
    }
}
