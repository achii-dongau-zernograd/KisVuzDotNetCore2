using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class rabProgramIdfosIdAreNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FondOcenochnihSredstvs_Files_FileModelId",
                table: "FondOcenochnihSredstvs");

            migrationBuilder.DropForeignKey(
                name: "FK_RabPrograms_Files_FileModelId",
                table: "RabPrograms");

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "RabPrograms",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "FondOcenochnihSredstvs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FondOcenochnihSredstvs_Files_FileModelId",
                table: "FondOcenochnihSredstvs",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RabPrograms_Files_FileModelId",
                table: "RabPrograms",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FondOcenochnihSredstvs_Files_FileModelId",
                table: "FondOcenochnihSredstvs");

            migrationBuilder.DropForeignKey(
                name: "FK_RabPrograms_Files_FileModelId",
                table: "RabPrograms");

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "RabPrograms",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "FondOcenochnihSredstvs",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FondOcenochnihSredstvs_Files_FileModelId",
                table: "FondOcenochnihSredstvs",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RabPrograms_Files_FileModelId",
                table: "RabPrograms",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
