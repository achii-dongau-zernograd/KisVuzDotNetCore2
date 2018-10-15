using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class structSubvisionPomeshenie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StructSubvisionId",
                table: "Pomeshenie",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pomeshenie_StructSubvisionId",
                table: "Pomeshenie",
                column: "StructSubvisionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pomeshenie_StructSubvisions_StructSubvisionId",
                table: "Pomeshenie",
                column: "StructSubvisionId",
                principalTable: "StructSubvisions",
                principalColumn: "StructSubvisionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pomeshenie_StructSubvisions_StructSubvisionId",
                table: "Pomeshenie");

            migrationBuilder.DropIndex(
                name: "IX_Pomeshenie_StructSubvisionId",
                table: "Pomeshenie");

            migrationBuilder.DropColumn(
                name: "StructSubvisionId",
                table: "Pomeshenie");
        }
    }
}
