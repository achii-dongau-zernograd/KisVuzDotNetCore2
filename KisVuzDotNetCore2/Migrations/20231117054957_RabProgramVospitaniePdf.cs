using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class RabProgramVospitaniePdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RabProgramVospitaniePdfId",
                table: "EduPlans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_RabProgramVospitaniePdfId",
                table: "EduPlans",
                column: "RabProgramVospitaniePdfId");

            migrationBuilder.AddForeignKey(
                name: "FK_EduPlans_Files_RabProgramVospitaniePdfId",
                table: "EduPlans",
                column: "RabProgramVospitaniePdfId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EduPlans_Files_RabProgramVospitaniePdfId",
                table: "EduPlans");

            migrationBuilder.DropIndex(
                name: "IX_EduPlans_RabProgramVospitaniePdfId",
                table: "EduPlans");

            migrationBuilder.DropColumn(
                name: "RabProgramVospitaniePdfId",
                table: "EduPlans");
        }
    }
}
