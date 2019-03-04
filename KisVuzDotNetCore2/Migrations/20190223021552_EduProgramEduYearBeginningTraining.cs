using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class EduProgramEduYearBeginningTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EduProgramEduYearBeginningTraining",
                columns: table => new
                {
                    EduProgramEduYearBeginningTrainingId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduProgramId = table.Column<int>(nullable: false),
                    EduYearBeginningTrainingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduProgramEduYearBeginningTraining", x => x.EduProgramEduYearBeginningTrainingId);
                    table.ForeignKey(
                        name: "FK_EduProgramEduYearBeginningTraining_EduPrograms_EduProgramId",
                        column: x => x.EduProgramId,
                        principalTable: "EduPrograms",
                        principalColumn: "EduProgramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduProgramEduYearBeginningTraining_EduYearBeginningTrainings_EduYearBeginningTrainingId",
                        column: x => x.EduYearBeginningTrainingId,
                        principalTable: "EduYearBeginningTrainings",
                        principalColumn: "EduYearBeginningTrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduProgramEduYearBeginningTraining_EduProgramId",
                table: "EduProgramEduYearBeginningTraining",
                column: "EduProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_EduProgramEduYearBeginningTraining_EduYearBeginningTrainingId",
                table: "EduProgramEduYearBeginningTraining",
                column: "EduYearBeginningTrainingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduProgramEduYearBeginningTraining");
        }
    }
}
