using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class FileModelListPereutverjdeniya : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileModelListPereutverjdeniyaId",
                table: "RabPrograms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FileModelListPereutverjdeniyaId",
                table: "FondOcenochnihSredstvs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RabPrograms_FileModelListPereutverjdeniyaId",
                table: "RabPrograms",
                column: "FileModelListPereutverjdeniyaId");

            migrationBuilder.CreateIndex(
                name: "IX_FondOcenochnihSredstvs_FileModelListPereutverjdeniyaId",
                table: "FondOcenochnihSredstvs",
                column: "FileModelListPereutverjdeniyaId");

            migrationBuilder.AddForeignKey(
                name: "FK_FondOcenochnihSredstvs_Files_FileModelListPereutverjdeniyaId",
                table: "FondOcenochnihSredstvs",
                column: "FileModelListPereutverjdeniyaId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RabPrograms_Files_FileModelListPereutverjdeniyaId",
                table: "RabPrograms",
                column: "FileModelListPereutverjdeniyaId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FondOcenochnihSredstvs_Files_FileModelListPereutverjdeniyaId",
                table: "FondOcenochnihSredstvs");

            migrationBuilder.DropForeignKey(
                name: "FK_RabPrograms_Files_FileModelListPereutverjdeniyaId",
                table: "RabPrograms");

            migrationBuilder.DropIndex(
                name: "IX_RabPrograms_FileModelListPereutverjdeniyaId",
                table: "RabPrograms");

            migrationBuilder.DropIndex(
                name: "IX_FondOcenochnihSredstvs_FileModelListPereutverjdeniyaId",
                table: "FondOcenochnihSredstvs");

            migrationBuilder.DropColumn(
                name: "FileModelListPereutverjdeniyaId",
                table: "RabPrograms");

            migrationBuilder.DropColumn(
                name: "FileModelListPereutverjdeniyaId",
                table: "FondOcenochnihSredstvs");
        }
    }
}
