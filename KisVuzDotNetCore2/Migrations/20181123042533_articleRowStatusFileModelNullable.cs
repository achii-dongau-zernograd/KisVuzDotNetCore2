using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class articleRowStatusFileModelNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Files_FileModelId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RowStatusId",
                table: "Articles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_RowStatusId",
                table: "Articles",
                column: "RowStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Files_FileModelId",
                table: "Articles",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_RowStatuses_RowStatusId",
                table: "Articles",
                column: "RowStatusId",
                principalTable: "RowStatuses",
                principalColumn: "RowStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Files_FileModelId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_RowStatuses_RowStatusId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_RowStatusId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "RowStatusId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Files_FileModelId",
                table: "Articles",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
