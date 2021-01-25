using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ElGradebooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElGradebookLessonAttendanceTypes",
                columns: table => new
                {
                    ElGradebookLessonAttendanceTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ElGradebookLessonAttendanceTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGradebookLessonAttendanceTypes", x => x.ElGradebookLessonAttendanceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ElGradebookLessonTypes",
                columns: table => new
                {
                    ElGradebookLessonTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ElGradebookLessonTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGradebookLessonTypes", x => x.ElGradebookLessonTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ElGradebooks",
                columns: table => new
                {
                    ElGradebookId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Course = table.Column<int>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    DisciplineName = table.Column<string>(nullable: true),
                    EduYear = table.Column<string>(nullable: true),
                    Faculty = table.Column<string>(nullable: true),
                    GroupId = table.Column<int>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    SemesterNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGradebooks", x => x.ElGradebookId);
                });

            migrationBuilder.CreateTable(
                name: "ElGradebookGroupStudents",
                columns: table => new
                {
                    ElGradebookGroupStudentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    ElGradebookGroupStudentFio = table.Column<string>(nullable: true),
                    ElGradebookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGradebookGroupStudents", x => x.ElGradebookGroupStudentId);
                    table.ForeignKey(
                        name: "FK_ElGradebookGroupStudents_ElGradebooks_ElGradebookId",
                        column: x => x.ElGradebookId,
                        principalTable: "ElGradebooks",
                        principalColumn: "ElGradebookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElGradebookLessons",
                columns: table => new
                {
                    ElGradebookLessonId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ElGradebookId = table.Column<int>(nullable: false),
                    ElGradebookLessonTypeId = table.Column<int>(nullable: false),
                    HoursNumber = table.Column<double>(nullable: false),
                    LessonTheme = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGradebookLessons", x => x.ElGradebookLessonId);
                    table.ForeignKey(
                        name: "FK_ElGradebookLessons_ElGradebooks_ElGradebookId",
                        column: x => x.ElGradebookId,
                        principalTable: "ElGradebooks",
                        principalColumn: "ElGradebookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElGradebookLessons_ElGradebookLessonTypes_ElGradebookLessonTypeId",
                        column: x => x.ElGradebookLessonTypeId,
                        principalTable: "ElGradebookLessonTypes",
                        principalColumn: "ElGradebookLessonTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElGradebookTeachers",
                columns: table => new
                {
                    ElGradebookTeacherId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ElGradebookId = table.Column<int>(nullable: false),
                    TeacherFio = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGradebookTeachers", x => x.ElGradebookTeacherId);
                    table.ForeignKey(
                        name: "FK_ElGradebookTeachers_ElGradebooks_ElGradebookId",
                        column: x => x.ElGradebookId,
                        principalTable: "ElGradebooks",
                        principalColumn: "ElGradebookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElGradebookLessonMarks",
                columns: table => new
                {
                    ElGradebookLessonMarkId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ElGradebookGroupStudentId = table.Column<int>(nullable: false),
                    ElGradebookLessonAttendanceTypeId = table.Column<int>(nullable: false),
                    ElGradebookLessonId = table.Column<int>(nullable: false),
                    PointsNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElGradebookLessonMarks", x => x.ElGradebookLessonMarkId);
                    table.ForeignKey(
                        name: "FK_ElGradebookLessonMarks_ElGradebookGroupStudents_ElGradebookGroupStudentId",
                        column: x => x.ElGradebookGroupStudentId,
                        principalTable: "ElGradebookGroupStudents",
                        principalColumn: "ElGradebookGroupStudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElGradebookLessonMarks_ElGradebookLessonAttendanceTypes_ElGradebookLessonAttendanceTypeId",
                        column: x => x.ElGradebookLessonAttendanceTypeId,
                        principalTable: "ElGradebookLessonAttendanceTypes",
                        principalColumn: "ElGradebookLessonAttendanceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElGradebookLessonMarks_ElGradebookLessons_ElGradebookLessonId",
                        column: x => x.ElGradebookLessonId,
                        principalTable: "ElGradebookLessons",
                        principalColumn: "ElGradebookLessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElGradebookGroupStudents_ElGradebookId",
                table: "ElGradebookGroupStudents",
                column: "ElGradebookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElGradebookLessonMarks_ElGradebookGroupStudentId",
                table: "ElGradebookLessonMarks",
                column: "ElGradebookGroupStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ElGradebookLessonMarks_ElGradebookLessonAttendanceTypeId",
                table: "ElGradebookLessonMarks",
                column: "ElGradebookLessonAttendanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElGradebookLessonMarks_ElGradebookLessonId",
                table: "ElGradebookLessonMarks",
                column: "ElGradebookLessonId");

            migrationBuilder.CreateIndex(
                name: "IX_ElGradebookLessons_ElGradebookId",
                table: "ElGradebookLessons",
                column: "ElGradebookId");

            migrationBuilder.CreateIndex(
                name: "IX_ElGradebookLessons_ElGradebookLessonTypeId",
                table: "ElGradebookLessons",
                column: "ElGradebookLessonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ElGradebookTeachers_ElGradebookId",
                table: "ElGradebookTeachers",
                column: "ElGradebookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElGradebookLessonMarks");

            migrationBuilder.DropTable(
                name: "ElGradebookTeachers");

            migrationBuilder.DropTable(
                name: "ElGradebookGroupStudents");

            migrationBuilder.DropTable(
                name: "ElGradebookLessonAttendanceTypes");

            migrationBuilder.DropTable(
                name: "ElGradebookLessons");

            migrationBuilder.DropTable(
                name: "ElGradebooks");

            migrationBuilder.DropTable(
                name: "ElGradebookLessonTypes");
        }
    }
}
