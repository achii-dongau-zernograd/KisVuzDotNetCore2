using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class fos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FondOcenochnihSredstvs",
                columns: table => new
                {
                    FondOcenochnihSredstvId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisciplineId = table.Column<int>(nullable: false),
                    FileModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FondOcenochnihSredstvs", x => x.FondOcenochnihSredstvId);
                    table.ForeignKey(
                        name: "FK_FondOcenochnihSredstvs_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FondOcenochnihSredstvs_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FondOcenochnihSredstvs_DisciplineId",
                table: "FondOcenochnihSredstvs",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_FondOcenochnihSredstvs_FileModelId",
                table: "FondOcenochnihSredstvs",
                column: "FileModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FondOcenochnihSredstvs");
        }
    }
}
