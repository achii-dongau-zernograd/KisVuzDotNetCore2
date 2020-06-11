using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class PaymentRowStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Payments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowStatusId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RowStatusId",
                table: "Payments",
                column: "RowStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_RowStatuses_RowStatusId",
                table: "Payments",
                column: "RowStatusId",
                principalTable: "RowStatuses",
                principalColumn: "RowStatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_RowStatuses_RowStatusId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_RowStatusId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "RowStatusId",
                table: "Payments");
        }
    }
}
