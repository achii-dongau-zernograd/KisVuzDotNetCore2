using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class UserWorkReviewFileModelIsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkReviews_Files_FileModelId",
                table: "UserWorkReviews");

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "UserWorkReviews",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkReviews_Files_FileModelId",
                table: "UserWorkReviews",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkReviews_Files_FileModelId",
                table: "UserWorkReviews");

            migrationBuilder.AlterColumn<int>(
                name: "FileModelId",
                table: "UserWorkReviews",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkReviews_Files_FileModelId",
                table: "UserWorkReviews",
                column: "FileModelId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
