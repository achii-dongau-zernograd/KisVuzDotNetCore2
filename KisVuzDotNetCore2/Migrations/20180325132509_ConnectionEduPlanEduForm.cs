using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ConnectionEduPlanEduForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EduFormId",
                table: "EduPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduFormId",
                table: "EduPlans",
                column: "EduFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_EduPlans_EduForms_EduFormId",
                table: "EduPlans",
                column: "EduFormId",
                principalTable: "EduForms",
                principalColumn: "EduFormId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EduPlans_EduForms_EduFormId",
                table: "EduPlans");

            migrationBuilder.DropIndex(
                name: "IX_EduPlans_EduFormId",
                table: "EduPlans");

            migrationBuilder.DropColumn(
                name: "EduFormId",
                table: "EduPlans");
        }
    }
}
