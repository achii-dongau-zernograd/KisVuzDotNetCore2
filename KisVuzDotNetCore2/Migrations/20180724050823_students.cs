using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class students : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    StudentGroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduKursId = table.Column<int>(nullable: false),
                    EduProfileId = table.Column<int>(nullable: false),
                    KuratorId = table.Column<int>(nullable: false),
                    StructFacultetId = table.Column<int>(nullable: false),
                    StudentGroupNamePrefix = table.Column<string>(nullable: true),
                    StudentGroupNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroups", x => x.StudentGroupId);
                    table.ForeignKey(
                        name: "FK_StudentGroups_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGroups_EduKurses_EduKursId",
                        column: x => x.EduKursId,
                        principalTable: "EduKurses",
                        principalColumn: "EduKursId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGroups_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGroups_Teachers_KuratorId",
                        column: x => x.KuratorId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGroups_StructFacultets_StructFacultetId",
                        column: x => x.StructFacultetId,
                        principalTable: "StructFacultets",
                        principalColumn: "StructFacultetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    StudentFio = table.Column<string>(nullable: true),
                    StudentGroupId = table.Column<int>(nullable: false),
                    ZachetnayaKnijkaNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "StudentGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_EduFormId",
                table: "StudentGroups",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_EduKursId",
                table: "StudentGroups",
                column: "EduKursId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_EduProfileId",
                table: "StudentGroups",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_KuratorId",
                table: "StudentGroups",
                column: "KuratorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_StructFacultetId",
                table: "StudentGroups",
                column: "StructFacultetId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AppUserId",
                table: "Students",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentGroupId",
                table: "Students",
                column: "StudentGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "StudentGroups");
        }
    }
}
