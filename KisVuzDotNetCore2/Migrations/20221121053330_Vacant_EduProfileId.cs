using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class Vacant_EduProfileId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EduProfileId",
                table: "Vacants",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vacants_EduProfileId",
                table: "Vacants",
                column: "EduProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacants_EduProfiles_EduProfileId",
                table: "Vacants",
                column: "EduProfileId",
                principalTable: "EduProfiles",
                principalColumn: "EduProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacants_EduProfiles_EduProfileId",
                table: "Vacants");

            migrationBuilder.DropIndex(
                name: "IX_Vacants_EduProfileId",
                table: "Vacants");

            migrationBuilder.DropColumn(
                name: "EduProfileId",
                table: "Vacants");
        }
    }
}
