using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class rucovodstvo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "SvedenRucovodstvo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SvedenRucovodstvo_AppUserId",
                table: "SvedenRucovodstvo",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SvedenRucovodstvo_AspNetUsers_AppUserId",
                table: "SvedenRucovodstvo",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SvedenRucovodstvo_AspNetUsers_AppUserId",
                table: "SvedenRucovodstvo");

            migrationBuilder.DropIndex(
                name: "IX_SvedenRucovodstvo_AppUserId",
                table: "SvedenRucovodstvo");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "SvedenRucovodstvo");
        }
    }
}
