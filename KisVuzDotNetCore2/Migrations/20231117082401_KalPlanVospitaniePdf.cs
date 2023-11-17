using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class KalPlanVospitaniePdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KalPlanVospitaniePdfId",
                table: "EduPlans",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_KalPlanVospitaniePdfId",
                table: "EduPlans",
                column: "KalPlanVospitaniePdfId");

            migrationBuilder.AddForeignKey(
                name: "FK_EduPlans_Files_KalPlanVospitaniePdfId",
                table: "EduPlans",
                column: "KalPlanVospitaniePdfId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EduPlans_Files_KalPlanVospitaniePdfId",
                table: "EduPlans");

            migrationBuilder.DropIndex(
                name: "IX_EduPlans_KalPlanVospitaniePdfId",
                table: "EduPlans");

            migrationBuilder.DropColumn(
                name: "KalPlanVospitaniePdfId",
                table: "EduPlans");
        }
    }
}
