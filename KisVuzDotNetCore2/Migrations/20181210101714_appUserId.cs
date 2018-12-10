using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class appUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "ScienceJournalAddingClaims",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScienceJournalAddingClaims_AppUserId",
                table: "ScienceJournalAddingClaims",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScienceJournalAddingClaims_AspNetUsers_AppUserId",
                table: "ScienceJournalAddingClaims",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScienceJournalAddingClaims_AspNetUsers_AppUserId",
                table: "ScienceJournalAddingClaims");

            migrationBuilder.DropIndex(
                name: "IX_ScienceJournalAddingClaims_AppUserId",
                table: "ScienceJournalAddingClaims");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ScienceJournalAddingClaims");
        }
    }
}
