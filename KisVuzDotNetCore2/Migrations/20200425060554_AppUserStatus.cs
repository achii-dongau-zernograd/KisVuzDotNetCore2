using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AppUserStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserStatusId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUserStatuses",
                columns: table => new
                {
                    AppUserStatusId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserStatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserStatuses", x => x.AppUserStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppUserStatusId",
                table: "AspNetUsers",
                column: "AppUserStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AppUserStatuses_AppUserStatusId",
                table: "AspNetUsers",
                column: "AppUserStatusId",
                principalTable: "AppUserStatuses",
                principalColumn: "AppUserStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AppUserStatuses_AppUserStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AppUserStatuses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppUserStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserStatusId",
                table: "AspNetUsers");
        }
    }
}
