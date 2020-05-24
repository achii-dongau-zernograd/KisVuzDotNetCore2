using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AppUserLmsEventsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLmsEvents_LmsEvents_LmsEventId1",
                table: "AppUserLmsEvents");

            migrationBuilder.DropIndex(
                name: "IX_AppUserLmsEvents_LmsEventId1",
                table: "AppUserLmsEvents");

            migrationBuilder.DropColumn(
                name: "LmsEventId1",
                table: "AppUserLmsEvents");

            migrationBuilder.AlterColumn<int>(
                name: "LmsEventId",
                table: "AppUserLmsEvents",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLmsEvents_LmsEventId",
                table: "AppUserLmsEvents",
                column: "LmsEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLmsEvents_LmsEvents_LmsEventId",
                table: "AppUserLmsEvents",
                column: "LmsEventId",
                principalTable: "LmsEvents",
                principalColumn: "LmsEventId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserLmsEvents_LmsEvents_LmsEventId",
                table: "AppUserLmsEvents");

            migrationBuilder.DropIndex(
                name: "IX_AppUserLmsEvents_LmsEventId",
                table: "AppUserLmsEvents");

            migrationBuilder.AlterColumn<string>(
                name: "LmsEventId",
                table: "AppUserLmsEvents",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "LmsEventId1",
                table: "AppUserLmsEvents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLmsEvents_LmsEventId1",
                table: "AppUserLmsEvents",
                column: "LmsEventId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserLmsEvents_LmsEvents_LmsEventId1",
                table: "AppUserLmsEvents",
                column: "LmsEventId1",
                principalTable: "LmsEvents",
                principalColumn: "LmsEventId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
