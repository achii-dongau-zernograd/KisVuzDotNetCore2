using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class RezultOsvoenObrazovatProgrId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RezultOsvoenObrazovatProgrId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_RezultOsvoenObrazovatProgrId",
                table: "Students",
                column: "RezultOsvoenObrazovatProgrId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Files_RezultOsvoenObrazovatProgrId",
                table: "Students",
                column: "RezultOsvoenObrazovatProgrId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Files_RezultOsvoenObrazovatProgrId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_RezultOsvoenObrazovatProgrId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RezultOsvoenObrazovatProgrId",
                table: "Students");
        }
    }
}
