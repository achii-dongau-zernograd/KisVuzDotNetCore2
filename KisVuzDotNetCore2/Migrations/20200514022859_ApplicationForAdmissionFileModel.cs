using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ApplicationForAdmissionFileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileModelId",
                table: "ApplicationForAdmissions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationForAdmissions_FileModelId",
                table: "ApplicationForAdmissions",
                column: "FileModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationForAdmissions_Files_FileModelId",
                table: "ApplicationForAdmissions",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationForAdmissions_Files_FileModelId",
                table: "ApplicationForAdmissions");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationForAdmissions_FileModelId",
                table: "ApplicationForAdmissions");

            migrationBuilder.DropColumn(
                name: "FileModelId",
                table: "ApplicationForAdmissions");
        }
    }
}
