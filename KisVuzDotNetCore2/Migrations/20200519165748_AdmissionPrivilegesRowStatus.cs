using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AdmissionPrivilegesRowStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "AdmissionPrivileges",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowStatusId",
                table: "AdmissionPrivileges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionPrivileges_RowStatusId",
                table: "AdmissionPrivileges",
                column: "RowStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdmissionPrivileges_RowStatuses_RowStatusId",
                table: "AdmissionPrivileges",
                column: "RowStatusId",
                principalTable: "RowStatuses",
                principalColumn: "RowStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdmissionPrivileges_RowStatuses_RowStatusId",
                table: "AdmissionPrivileges");

            migrationBuilder.DropIndex(
                name: "IX_AdmissionPrivileges_RowStatusId",
                table: "AdmissionPrivileges");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "AdmissionPrivileges");

            migrationBuilder.DropColumn(
                name: "RowStatusId",
                table: "AdmissionPrivileges");
        }
    }
}
