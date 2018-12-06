using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class PatentFileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileModelId",
                table: "Patents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowStatusId",
                table: "Patents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patents_FileModelId",
                table: "Patents",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Patents_RowStatusId",
                table: "Patents",
                column: "RowStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patents_Files_FileModelId",
                table: "Patents",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patents_RowStatuses_RowStatusId",
                table: "Patents",
                column: "RowStatusId",
                principalTable: "RowStatuses",
                principalColumn: "RowStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patents_Files_FileModelId",
                table: "Patents");

            migrationBuilder.DropForeignKey(
                name: "FK_Patents_RowStatuses_RowStatusId",
                table: "Patents");

            migrationBuilder.DropIndex(
                name: "IX_Patents_FileModelId",
                table: "Patents");

            migrationBuilder.DropIndex(
                name: "IX_Patents_RowStatusId",
                table: "Patents");

            migrationBuilder.DropColumn(
                name: "FileModelId",
                table: "Patents");

            migrationBuilder.DropColumn(
                name: "RowStatusId",
                table: "Patents");
        }
    }
}
