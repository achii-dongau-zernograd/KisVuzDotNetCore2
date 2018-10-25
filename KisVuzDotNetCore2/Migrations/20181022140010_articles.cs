using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class articles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitationBases",
                columns: table => new
                {
                    CitationBaseId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CitationBaseName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitationBases", x => x.CitationBaseId);
                });

            migrationBuilder.CreateTable(
                name: "ScienceJournals",
                columns: table => new
                {
                    ScienceJournalId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ELibraryLink = table.Column<string>(nullable: true),
                    IsVak = table.Column<bool>(nullable: false),
                    IsZarubejn = table.Column<bool>(nullable: false),
                    ScienceJournalName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScienceJournals", x => x.ScienceJournalId);
                });

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    YearId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    YearName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.YearId);
                });

            migrationBuilder.CreateTable(
                name: "ScienceJournalCitationBases",
                columns: table => new
                {
                    ScienceJournalCitationBaseId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CitationBaseId = table.Column<int>(nullable: false),
                    ScienceJournalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScienceJournalCitationBases", x => x.ScienceJournalCitationBaseId);
                    table.ForeignKey(
                        name: "FK_ScienceJournalCitationBases_CitationBases_CitationBaseId",
                        column: x => x.CitationBaseId,
                        principalTable: "CitationBases",
                        principalColumn: "CitationBaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScienceJournalCitationBases_ScienceJournals_ScienceJournalId",
                        column: x => x.ScienceJournalId,
                        principalTable: "ScienceJournals",
                        principalColumn: "ScienceJournalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    ArticleId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleName = table.Column<string>(nullable: true),
                    FileModelId = table.Column<int>(nullable: false),
                    Pages = table.Column<string>(nullable: true),
                    ScienceJournalId = table.Column<int>(nullable: false),
                    VipuskNumber = table.Column<string>(nullable: true),
                    YearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Articles_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_ScienceJournals_ScienceJournalId",
                        column: x => x.ScienceJournalId,
                        principalTable: "ScienceJournals",
                        principalColumn: "ScienceJournalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Years_YearId",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "YearId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArticleAuthors",
                columns: table => new
                {
                    ArticleAuthorId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false),
                    AuthorPart = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleAuthors", x => x.ArticleAuthorId);
                    table.ForeignKey(
                        name: "FK_ArticleAuthors_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleAuthors_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArticleNirTemas",
                columns: table => new
                {
                    ArticleNirTemaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ArticleId = table.Column<int>(nullable: false),
                    NirTemaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleNirTemas", x => x.ArticleNirTemaId);
                    table.ForeignKey(
                        name: "FK_ArticleNirTemas_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleNirTemas_NirTema_NirTemaId",
                        column: x => x.NirTemaId,
                        principalTable: "NirTema",
                        principalColumn: "NirTemaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAuthors_ArticleId",
                table: "ArticleAuthors",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAuthors_AuthorId",
                table: "ArticleAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleNirTemas_ArticleId",
                table: "ArticleNirTemas",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleNirTemas_NirTemaId",
                table: "ArticleNirTemas",
                column: "NirTemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_FileModelId",
                table: "Articles",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ScienceJournalId",
                table: "Articles",
                column: "ScienceJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_YearId",
                table: "Articles",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_ScienceJournalCitationBases_CitationBaseId",
                table: "ScienceJournalCitationBases",
                column: "CitationBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ScienceJournalCitationBases_ScienceJournalId",
                table: "ScienceJournalCitationBases",
                column: "ScienceJournalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleAuthors");

            migrationBuilder.DropTable(
                name: "ArticleNirTemas");

            migrationBuilder.DropTable(
                name: "ScienceJournalCitationBases");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "CitationBases");

            migrationBuilder.DropTable(
                name: "ScienceJournals");

            migrationBuilder.DropTable(
                name: "Years");
        }
    }
}
