using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class TeacherEduProfileDisciplineNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherEduProfileDisciplineNames_EduProfiles_EduProfileId",
                table: "TeacherEduProfileDisciplineNames");

            migrationBuilder.AlterColumn<int>(
                name: "EduProfileId",
                table: "TeacherEduProfileDisciplineNames",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherEduProfileDisciplineNames_EduProfiles_EduProfileId",
                table: "TeacherEduProfileDisciplineNames",
                column: "EduProfileId",
                principalTable: "EduProfiles",
                principalColumn: "EduProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherEduProfileDisciplineNames_EduProfiles_EduProfileId",
                table: "TeacherEduProfileDisciplineNames");

            migrationBuilder.AlterColumn<int>(
                name: "EduProfileId",
                table: "TeacherEduProfileDisciplineNames",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherEduProfileDisciplineNames_EduProfiles_EduProfileId",
                table: "TeacherEduProfileDisciplineNames",
                column: "EduProfileId",
                principalTable: "EduProfiles",
                principalColumn: "EduProfileId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
