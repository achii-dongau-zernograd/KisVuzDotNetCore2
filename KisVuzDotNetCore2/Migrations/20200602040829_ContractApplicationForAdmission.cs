using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ContractApplicationForAdmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationForAdmissionId",
                table: "Contracts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ApplicationForAdmissionId",
                table: "Contracts",
                column: "ApplicationForAdmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_ApplicationForAdmissions_ApplicationForAdmissionId",
                table: "Contracts",
                column: "ApplicationForAdmissionId",
                principalTable: "ApplicationForAdmissions",
                principalColumn: "ApplicationForAdmissionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_ApplicationForAdmissions_ApplicationForAdmissionId",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_ApplicationForAdmissionId",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ApplicationForAdmissionId",
                table: "Contracts");
        }
    }
}
