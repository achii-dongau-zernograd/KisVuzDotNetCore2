using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class TeacherWorkExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkExperienceGeneral",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkExperienceSpecialty",
                table: "Teachers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkExperienceGeneral",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "WorkExperienceSpecialty",
                table: "Teachers");
        }
    }
}
