using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class DisciplineName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EduYears_EduPlans_EduPlanId",
                table: "EduYears");

            migrationBuilder.DropIndex(
                name: "IX_EduYears_EduPlanId",
                table: "EduYears");

            migrationBuilder.DropColumn(
                name: "EduPlanId",
                table: "EduYears");

            migrationBuilder.DropColumn(
                name: "DisciplineName",
                table: "Discipline");

            migrationBuilder.AddColumn<int>(
                name: "DisciplineNameId",
                table: "Discipline",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EduPlanEduYears",
                columns: table => new
                {
                    EduPlanEduYearId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduPlanId = table.Column<int>(nullable: false),
                    EduYearId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduPlanEduYears", x => x.EduPlanEduYearId);
                    table.ForeignKey(
                        name: "FK_EduPlanEduYears_EduPlans_EduPlanId",
                        column: x => x.EduPlanId,
                        principalTable: "EduPlans",
                        principalColumn: "EduPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPlanEduYears_EduYears_EduYearId",
                        column: x => x.EduYearId,
                        principalTable: "EduYears",
                        principalColumn: "EduYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_DisciplineNameId",
                table: "Discipline",
                column: "DisciplineNameId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlanEduYears_EduPlanId",
                table: "EduPlanEduYears",
                column: "EduPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlanEduYears_EduYearId",
                table: "EduPlanEduYears",
                column: "EduYearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discipline_DisciplineNames_DisciplineNameId",
                table: "Discipline",
                column: "DisciplineNameId",
                principalTable: "DisciplineNames",
                principalColumn: "DisciplineNameId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discipline_DisciplineNames_DisciplineNameId",
                table: "Discipline");

            migrationBuilder.DropTable(
                name: "EduPlanEduYears");

            migrationBuilder.DropIndex(
                name: "IX_Discipline_DisciplineNameId",
                table: "Discipline");

            migrationBuilder.DropColumn(
                name: "DisciplineNameId",
                table: "Discipline");

            migrationBuilder.AddColumn<int>(
                name: "EduPlanId",
                table: "EduYears",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisciplineName",
                table: "Discipline",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EduYears_EduPlanId",
                table: "EduYears",
                column: "EduPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_EduYears_EduPlans_EduPlanId",
                table: "EduYears",
                column: "EduPlanId",
                principalTable: "EduPlans",
                principalColumn: "EduPlanId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
