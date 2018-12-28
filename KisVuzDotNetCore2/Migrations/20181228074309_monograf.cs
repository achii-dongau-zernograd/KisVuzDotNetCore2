using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class monograf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Monografs",
                columns: table => new
                {
                    MonografId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileModelId = table.Column<int>(nullable: true),
                    IsACHII = table.Column<bool>(nullable: false),
                    MonografName = table.Column<string>(nullable: true),
                    RowStatusId = table.Column<int>(nullable: true),
                    YearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monografs", x => x.MonografId);
                    table.ForeignKey(
                        name: "FK_Monografs_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Monografs_RowStatuses_RowStatusId",
                        column: x => x.RowStatusId,
                        principalTable: "RowStatuses",
                        principalColumn: "RowStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Monografs_Years_YearId",
                        column: x => x.YearId,
                        principalTable: "Years",
                        principalColumn: "YearId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MonografAuthors",
                columns: table => new
                {
                    MonografAuthorId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    AuthorPart = table.Column<decimal>(nullable: false),
                    MonografId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonografAuthors", x => x.MonografAuthorId);
                    table.ForeignKey(
                        name: "FK_MonografAuthors_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonografAuthors_Monografs_MonografId",
                        column: x => x.MonografId,
                        principalTable: "Monografs",
                        principalColumn: "MonografId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonografNirSpecials",
                columns: table => new
                {
                    MonografNirSpecialId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MonografId = table.Column<int>(nullable: false),
                    NirSpecialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonografNirSpecials", x => x.MonografNirSpecialId);
                    table.ForeignKey(
                        name: "FK_MonografNirSpecials_Monografs_MonografId",
                        column: x => x.MonografId,
                        principalTable: "Monografs",
                        principalColumn: "MonografId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonografNirSpecials_NirSpecials_NirSpecialId",
                        column: x => x.NirSpecialId,
                        principalTable: "NirSpecials",
                        principalColumn: "NirSpecialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonografNirTemas",
                columns: table => new
                {
                    MonografNirTemaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MonografId = table.Column<int>(nullable: false),
                    NirTemaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonografNirTemas", x => x.MonografNirTemaId);
                    table.ForeignKey(
                        name: "FK_MonografNirTemas_Monografs_MonografId",
                        column: x => x.MonografId,
                        principalTable: "Monografs",
                        principalColumn: "MonografId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MonografNirTemas_NirTema_NirTemaId",
                        column: x => x.NirTemaId,
                        principalTable: "NirTema",
                        principalColumn: "NirTemaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonografAuthors_AuthorId",
                table: "MonografAuthors",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_MonografAuthors_MonografId",
                table: "MonografAuthors",
                column: "MonografId");

            migrationBuilder.CreateIndex(
                name: "IX_MonografNirSpecials_MonografId",
                table: "MonografNirSpecials",
                column: "MonografId");

            migrationBuilder.CreateIndex(
                name: "IX_MonografNirSpecials_NirSpecialId",
                table: "MonografNirSpecials",
                column: "NirSpecialId");

            migrationBuilder.CreateIndex(
                name: "IX_MonografNirTemas_MonografId",
                table: "MonografNirTemas",
                column: "MonografId");

            migrationBuilder.CreateIndex(
                name: "IX_MonografNirTemas_NirTemaId",
                table: "MonografNirTemas",
                column: "NirTemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Monografs_FileModelId",
                table: "Monografs",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Monografs_RowStatusId",
                table: "Monografs",
                column: "RowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Monografs_YearId",
                table: "Monografs",
                column: "YearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonografAuthors");

            migrationBuilder.DropTable(
                name: "MonografNirSpecials");

            migrationBuilder.DropTable(
                name: "MonografNirTemas");

            migrationBuilder.DropTable(
                name: "Monografs");
        }
    }
}
