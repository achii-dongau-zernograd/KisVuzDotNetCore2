using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class TeacherStruktPostStavkaAddingEmploymentForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmploymentFormId",
                table: "TeacherStructKafPostStavka",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStructKafPostStavka_EmploymentFormId",
                table: "TeacherStructKafPostStavka",
                column: "EmploymentFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherStructKafPostStavka_EmploymentForms_EmploymentFormId",
                table: "TeacherStructKafPostStavka",
                column: "EmploymentFormId",
                principalTable: "EmploymentForms",
                principalColumn: "EmploymentFormId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherStructKafPostStavka_EmploymentForms_EmploymentFormId",
                table: "TeacherStructKafPostStavka");

            migrationBuilder.DropIndex(
                name: "IX_TeacherStructKafPostStavka_EmploymentFormId",
                table: "TeacherStructKafPostStavka");

            migrationBuilder.DropColumn(
                name: "EmploymentFormId",
                table: "TeacherStructKafPostStavka");
        }
    }
}
