using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AppUserDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Citizenship",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MilitaryServiceStatusId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "AbiturientLmsEvents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "FamilyMemberTypes",
                columns: table => new
                {
                    FamilyMemberTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FamilyMemberTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMemberTypes", x => x.FamilyMemberTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GenderName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "LmsTaskSets",
                columns: table => new
                {
                    LmsTaskSetId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LmsTaskSetDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsTaskSets", x => x.LmsTaskSetId);
                });

            migrationBuilder.CreateTable(
                name: "LmsTaskTypes",
                columns: table => new
                {
                    LmsTaskTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LmsTaskTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsTaskTypes", x => x.LmsTaskTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MilitaryServiceStatuses",
                columns: table => new
                {
                    MilitaryServiceStatusId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MilitaryServiceStatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryServiceStatuses", x => x.MilitaryServiceStatusId);
                });

            migrationBuilder.CreateTable(
                name: "FamilyMemberContacts",
                columns: table => new
                {
                    FamilyMemberContactId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    FamilyMemberTypeId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    WorkPlace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMemberContacts", x => x.FamilyMemberContactId);
                    table.ForeignKey(
                        name: "FK_FamilyMemberContacts_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamilyMemberContacts_FamilyMemberTypes_FamilyMemberTypeId",
                        column: x => x.FamilyMemberTypeId,
                        principalTable: "FamilyMemberTypes",
                        principalColumn: "FamilyMemberTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsEventLmsTaskSets",
                columns: table => new
                {
                    LmsEventLmsTaskSetId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LmsEventId = table.Column<int>(nullable: false),
                    LmsTaskSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsEventLmsTaskSets", x => x.LmsEventLmsTaskSetId);
                    table.ForeignKey(
                        name: "FK_LmsEventLmsTaskSets_LmsEvents_LmsEventId",
                        column: x => x.LmsEventId,
                        principalTable: "LmsEvents",
                        principalColumn: "LmsEventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsEventLmsTaskSets_LmsTaskSets_LmsTaskSetId",
                        column: x => x.LmsTaskSetId,
                        principalTable: "LmsTaskSets",
                        principalColumn: "LmsTaskSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsTasks",
                columns: table => new
                {
                    LmsTaskId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    DateTimeOfCreation = table.Column<DateTime>(nullable: false),
                    LmsTaskJpgId = table.Column<int>(nullable: true),
                    LmsTaskText = table.Column<string>(nullable: true),
                    LmsTaskTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsTasks", x => x.LmsTaskId);
                    table.ForeignKey(
                        name: "FK_LmsTasks_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LmsTasks_Files_LmsTaskJpgId",
                        column: x => x.LmsTaskJpgId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LmsTasks_LmsTaskTypes_LmsTaskTypeId",
                        column: x => x.LmsTaskTypeId,
                        principalTable: "LmsTaskTypes",
                        principalColumn: "LmsTaskTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsTaskDisciplineNames",
                columns: table => new
                {
                    LmsTaskDisciplineNameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisciplineNameId = table.Column<int>(nullable: false),
                    LmsTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsTaskDisciplineNames", x => x.LmsTaskDisciplineNameId);
                    table.ForeignKey(
                        name: "FK_LmsTaskDisciplineNames_DisciplineNames_DisciplineNameId",
                        column: x => x.DisciplineNameId,
                        principalTable: "DisciplineNames",
                        principalColumn: "DisciplineNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsTaskDisciplineNames_LmsTasks_LmsTaskId",
                        column: x => x.LmsTaskId,
                        principalTable: "LmsTasks",
                        principalColumn: "LmsTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsTaskSetLmsTasks",
                columns: table => new
                {
                    LmsTaskSetLmsTaskId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LmsTaskId = table.Column<int>(nullable: false),
                    LmsTaskSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsTaskSetLmsTasks", x => x.LmsTaskSetLmsTaskId);
                    table.ForeignKey(
                        name: "FK_LmsTaskSetLmsTasks_LmsTasks_LmsTaskId",
                        column: x => x.LmsTaskId,
                        principalTable: "LmsTasks",
                        principalColumn: "LmsTaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsTaskSetLmsTasks_LmsTaskSets_LmsTaskSetId",
                        column: x => x.LmsTaskSetId,
                        principalTable: "LmsTaskSets",
                        principalColumn: "LmsTaskSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MilitaryServiceStatusId",
                table: "AspNetUsers",
                column: "MilitaryServiceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMemberContacts_AppUserId",
                table: "FamilyMemberContacts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMemberContacts_FamilyMemberTypeId",
                table: "FamilyMemberContacts",
                column: "FamilyMemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventLmsTaskSets_LmsEventId",
                table: "LmsEventLmsTaskSets",
                column: "LmsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventLmsTaskSets_LmsTaskSetId",
                table: "LmsEventLmsTaskSets",
                column: "LmsTaskSetId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsTaskDisciplineNames_DisciplineNameId",
                table: "LmsTaskDisciplineNames",
                column: "DisciplineNameId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsTaskDisciplineNames_LmsTaskId",
                table: "LmsTaskDisciplineNames",
                column: "LmsTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsTasks_AppUserId",
                table: "LmsTasks",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsTasks_LmsTaskJpgId",
                table: "LmsTasks",
                column: "LmsTaskJpgId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsTasks_LmsTaskTypeId",
                table: "LmsTasks",
                column: "LmsTaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsTaskSetLmsTasks_LmsTaskId",
                table: "LmsTaskSetLmsTasks",
                column: "LmsTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsTaskSetLmsTasks_LmsTaskSetId",
                table: "LmsTaskSetLmsTasks",
                column: "LmsTaskSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Genders_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MilitaryServiceStatuses_MilitaryServiceStatusId",
                table: "AspNetUsers",
                column: "MilitaryServiceStatusId",
                principalTable: "MilitaryServiceStatuses",
                principalColumn: "MilitaryServiceStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Genders_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MilitaryServiceStatuses_MilitaryServiceStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FamilyMemberContacts");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "LmsEventLmsTaskSets");

            migrationBuilder.DropTable(
                name: "LmsTaskDisciplineNames");

            migrationBuilder.DropTable(
                name: "LmsTaskSetLmsTasks");

            migrationBuilder.DropTable(
                name: "MilitaryServiceStatuses");

            migrationBuilder.DropTable(
                name: "FamilyMemberTypes");

            migrationBuilder.DropTable(
                name: "LmsTasks");

            migrationBuilder.DropTable(
                name: "LmsTaskSets");

            migrationBuilder.DropTable(
                name: "LmsTaskTypes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MilitaryServiceStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Citizenship",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MilitaryServiceStatusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "AbiturientLmsEvents");
        }
    }
}
