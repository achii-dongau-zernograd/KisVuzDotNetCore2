using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class structSubvisionAccountChief : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StructSubvisionAccountChiefId",
                table: "StructSubvisions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructSubvisionAccountChiefId",
                table: "StructSubvisions",
                column: "StructSubvisionAccountChiefId");

            migrationBuilder.AddForeignKey(
                name: "FK_StructSubvisions_AspNetUsers_StructSubvisionAccountChiefId",
                table: "StructSubvisions",
                column: "StructSubvisionAccountChiefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StructSubvisions_AspNetUsers_StructSubvisionAccountChiefId",
                table: "StructSubvisions");

            migrationBuilder.DropIndex(
                name: "IX_StructSubvisions_StructSubvisionAccountChiefId",
                table: "StructSubvisions");

            migrationBuilder.DropColumn(
                name: "StructSubvisionAccountChiefId",
                table: "StructSubvisions");
        }
    }
}
