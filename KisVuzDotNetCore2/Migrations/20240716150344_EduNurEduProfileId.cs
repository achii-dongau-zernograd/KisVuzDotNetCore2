using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class EduNurEduProfileId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EduProfileId",
                table: "EduNir",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EduNir_EduProfileId",
                table: "EduNir",
                column: "EduProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_EduNir_EduProfiles_EduProfileId",
                table: "EduNir",
                column: "EduProfileId",
                principalTable: "EduProfiles",
                principalColumn: "EduProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EduNir_EduProfiles_EduProfileId",
                table: "EduNir");

            migrationBuilder.DropIndex(
                name: "IX_EduNir_EduProfileId",
                table: "EduNir");

            migrationBuilder.DropColumn(
                name: "EduProfileId",
                table: "EduNir");
        }
    }
}
