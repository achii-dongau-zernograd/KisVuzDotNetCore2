using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class annotationFileModelIdIsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EduAnnotations_Files_FileModelId",
                table: "EduAnnotations");

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "EduAnnotations",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_EduAnnotations_Files_FileModelId",
                table: "EduAnnotations",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EduAnnotations_Files_FileModelId",
                table: "EduAnnotations");

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "EduAnnotations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EduAnnotations_Files_FileModelId",
                table: "EduAnnotations",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
