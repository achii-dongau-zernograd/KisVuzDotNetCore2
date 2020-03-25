using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class MessageFromAppUserToStudentGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMessageTypes",
                columns: table => new
                {
                    UserMessageTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserMessageTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessageTypes", x => x.UserMessageTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MessagesFromAppUsersToStudentGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false),
                    DisciplineNameId = table.Column<int>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    MessageText = table.Column<string>(nullable: true),
                    StudentGroupId = table.Column<int>(nullable: false),
                    UserMessageTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesFromAppUsersToStudentGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessagesFromAppUsersToStudentGroups_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessagesFromAppUsersToStudentGroups_DisciplineNames_DisciplineNameId",
                        column: x => x.DisciplineNameId,
                        principalTable: "DisciplineNames",
                        principalColumn: "DisciplineNameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessagesFromAppUsersToStudentGroups_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "StudentGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessagesFromAppUsersToStudentGroups_UserMessageTypes_UserMessageTypeId",
                        column: x => x.UserMessageTypeId,
                        principalTable: "UserMessageTypes",
                        principalColumn: "UserMessageTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessagesFromAppUsersToStudentGroups_AppUserId",
                table: "MessagesFromAppUsersToStudentGroups",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagesFromAppUsersToStudentGroups_DisciplineNameId",
                table: "MessagesFromAppUsersToStudentGroups",
                column: "DisciplineNameId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagesFromAppUsersToStudentGroups_StudentGroupId",
                table: "MessagesFromAppUsersToStudentGroups",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MessagesFromAppUsersToStudentGroups_UserMessageTypeId",
                table: "MessagesFromAppUsersToStudentGroups",
                column: "UserMessageTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessagesFromAppUsersToStudentGroups");

            migrationBuilder.DropTable(
                name: "UserMessageTypes");
        }
    }
}
