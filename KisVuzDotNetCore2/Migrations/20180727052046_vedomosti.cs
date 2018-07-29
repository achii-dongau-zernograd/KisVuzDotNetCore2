using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class vedomosti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vedomosti",
                columns: table => new
                {
                    VedomostId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisciplineName = table.Column<string>(nullable: true),
                    EduYearId = table.Column<int>(nullable: false),
                    SemestrNameId = table.Column<int>(nullable: false),
                    StudentGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vedomosti", x => x.VedomostId);
                    table.ForeignKey(
                        name: "FK_Vedomosti_EduYears_EduYearId",
                        column: x => x.EduYearId,
                        principalTable: "EduYears",
                        principalColumn: "EduYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vedomosti_SemestrNames_SemestrNameId",
                        column: x => x.SemestrNameId,
                        principalTable: "SemestrNames",
                        principalColumn: "SemestrNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vedomosti_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "StudentGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VedomostStudentMarkNames",
                columns: table => new
                {
                    VedomostStudentMarkNameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    VedomostStudentMarkNameName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VedomostStudentMarkNames", x => x.VedomostStudentMarkNameId);
                });

            migrationBuilder.CreateTable(
                name: "VedomostStudentMarks",
                columns: table => new
                {
                    VedomostStudentMarkId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    VedomostId = table.Column<int>(nullable: false),
                    VedomostStudentMarkNameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VedomostStudentMarks", x => x.VedomostStudentMarkId);
                    table.ForeignKey(
                        name: "FK_VedomostStudentMarks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VedomostStudentMarks_Vedomosti_VedomostId",
                        column: x => x.VedomostId,
                        principalTable: "Vedomosti",
                        principalColumn: "VedomostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VedomostStudentMarks_VedomostStudentMarkNames_VedomostStudentMarkNameId",
                        column: x => x.VedomostStudentMarkNameId,
                        principalTable: "VedomostStudentMarkNames",
                        principalColumn: "VedomostStudentMarkNameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vedomosti_EduYearId",
                table: "Vedomosti",
                column: "EduYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Vedomosti_SemestrNameId",
                table: "Vedomosti",
                column: "SemestrNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Vedomosti_StudentGroupId",
                table: "Vedomosti",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VedomostStudentMarks_StudentId",
                table: "VedomostStudentMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_VedomostStudentMarks_VedomostId",
                table: "VedomostStudentMarks",
                column: "VedomostId");

            migrationBuilder.CreateIndex(
                name: "IX_VedomostStudentMarks_VedomostStudentMarkNameId",
                table: "VedomostStudentMarks",
                column: "VedomostStudentMarkNameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VedomostStudentMarks");

            migrationBuilder.DropTable(
                name: "Vedomosti");

            migrationBuilder.DropTable(
                name: "VedomostStudentMarkNames");
        }
    }
}
