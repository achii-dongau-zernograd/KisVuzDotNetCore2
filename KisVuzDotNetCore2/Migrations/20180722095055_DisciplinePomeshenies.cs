using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class DisciplinePomeshenies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DisciplinePomeshenies",
                columns: table => new
                {
                    DisciplinePomeshenieId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisciplineId = table.Column<int>(nullable: false),
                    EduPlanEduYearId = table.Column<int>(nullable: false),
                    PomeshenieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinePomeshenies", x => x.DisciplinePomeshenieId);
                    table.ForeignKey(
                        name: "FK_DisciplinePomeshenies_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinePomeshenies_EduPlanEduYears_EduPlanEduYearId",
                        column: x => x.EduPlanEduYearId,
                        principalTable: "EduPlanEduYears",
                        principalColumn: "EduPlanEduYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DisciplinePomeshenies_Pomeshenie_PomeshenieId",
                        column: x => x.PomeshenieId,
                        principalTable: "Pomeshenie",
                        principalColumn: "PomeshenieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinePomeshenies_DisciplineId",
                table: "DisciplinePomeshenies",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinePomeshenies_EduPlanEduYearId",
                table: "DisciplinePomeshenies",
                column: "EduPlanEduYearId");

            migrationBuilder.CreateIndex(
                name: "IX_DisciplinePomeshenies_PomeshenieId",
                table: "DisciplinePomeshenies",
                column: "PomeshenieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DisciplinePomeshenies");
        }
    }
}
