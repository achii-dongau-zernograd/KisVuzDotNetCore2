using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ApplicationForAdmissionStatusRemark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "ApplicationForAdmissions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowStatusId",
                table: "ApplicationForAdmissions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationForAdmissions_RowStatusId",
                table: "ApplicationForAdmissions",
                column: "RowStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationForAdmissions_RowStatuses_RowStatusId",
                table: "ApplicationForAdmissions",
                column: "RowStatusId",
                principalTable: "RowStatuses",
                principalColumn: "RowStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationForAdmissions_RowStatuses_RowStatusId",
                table: "ApplicationForAdmissions");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationForAdmissions_RowStatusId",
                table: "ApplicationForAdmissions");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "ApplicationForAdmissions");

            migrationBuilder.DropColumn(
                name: "RowStatusId",
                table: "ApplicationForAdmissions");
        }
    }
}
