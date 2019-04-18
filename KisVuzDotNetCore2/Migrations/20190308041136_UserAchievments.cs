using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class UserAchievments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAchievmentTypes",
                columns: table => new
                {
                    UserAchievmentTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserAchievmentTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievmentTypes", x => x.UserAchievmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserAchievments",
                columns: table => new
                {
                    UserAchievmentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UserAchievmentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAchievments", x => x.UserAchievmentId);
                    table.ForeignKey(
                        name: "FK_UserAchievments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAchievments_UserAchievmentTypes_UserAchievmentTypeId",
                        column: x => x.UserAchievmentTypeId,
                        principalTable: "UserAchievmentTypes",
                        principalColumn: "UserAchievmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievments_AppUserId",
                table: "UserAchievments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAchievments_UserAchievmentTypeId",
                table: "UserAchievments",
                column: "UserAchievmentTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAchievments");

            migrationBuilder.DropTable(
                name: "UserAchievmentTypes");
        }
    }
}
