using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AppUserLmsEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LmsEvents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebLink",
                table: "LmsEvents",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppUserLmsEventUserRoles",
                columns: table => new
                {
                    AppUserLmsEventUserRoleId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserLmsEventUserRoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLmsEventUserRoles", x => x.AppUserLmsEventUserRoleId);
                });

            migrationBuilder.CreateTable(
                name: "LmsEventAdditionalMaterials",
                columns: table => new
                {
                    LmsEventAdditionalMaterialId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LmsEventAdditionalMaterialLink = table.Column<string>(nullable: true),
                    LmsEventAdditionalMaterialName = table.Column<string>(nullable: true),
                    LmsEventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsEventAdditionalMaterials", x => x.LmsEventAdditionalMaterialId);
                    table.ForeignKey(
                        name: "FK_LmsEventAdditionalMaterials_LmsEvents_LmsEventId",
                        column: x => x.LmsEventId,
                        principalTable: "LmsEvents",
                        principalColumn: "LmsEventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsEventChatMessages",
                columns: table => new
                {
                    LmsEventChatMessageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    LmsEventChatMessageLink = table.Column<string>(nullable: true),
                    LmsEventChatMessageText = table.Column<string>(nullable: true),
                    LmsEventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsEventChatMessages", x => x.LmsEventChatMessageId);
                    table.ForeignKey(
                        name: "FK_LmsEventChatMessages_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LmsEventChatMessages_LmsEvents_LmsEventId",
                        column: x => x.LmsEventId,
                        principalTable: "LmsEvents",
                        principalColumn: "LmsEventId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLmsEvents",
                columns: table => new
                {
                    AppUserLmsEventId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    AppUserLmsEventUserRoleId = table.Column<int>(nullable: false),
                    LmsEventId = table.Column<string>(nullable: true),
                    LmsEventId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLmsEvents", x => x.AppUserLmsEventId);
                    table.ForeignKey(
                        name: "FK_AppUserLmsEvents_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserLmsEvents_AppUserLmsEventUserRoles_AppUserLmsEventUserRoleId",
                        column: x => x.AppUserLmsEventUserRoleId,
                        principalTable: "AppUserLmsEventUserRoles",
                        principalColumn: "AppUserLmsEventUserRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserLmsEvents_LmsEvents_LmsEventId1",
                        column: x => x.LmsEventId1,
                        principalTable: "LmsEvents",
                        principalColumn: "LmsEventId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLmsEvents_AppUserId",
                table: "AppUserLmsEvents",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLmsEvents_AppUserLmsEventUserRoleId",
                table: "AppUserLmsEvents",
                column: "AppUserLmsEventUserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLmsEvents_LmsEventId1",
                table: "AppUserLmsEvents",
                column: "LmsEventId1");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventAdditionalMaterials_LmsEventId",
                table: "LmsEventAdditionalMaterials",
                column: "LmsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventChatMessages_AppUserId",
                table: "LmsEventChatMessages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventChatMessages_LmsEventId",
                table: "LmsEventChatMessages",
                column: "LmsEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserLmsEvents");

            migrationBuilder.DropTable(
                name: "LmsEventAdditionalMaterials");

            migrationBuilder.DropTable(
                name: "LmsEventChatMessages");

            migrationBuilder.DropTable(
                name: "AppUserLmsEventUserRoles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "LmsEvents");

            migrationBuilder.DropColumn(
                name: "WebLink",
                table: "LmsEvents");
        }
    }
}
