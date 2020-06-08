using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class LmsEventLmsTasksetAppUserAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LmsEventLmsTasksetsAppUserAnswers",
                columns: table => new
                {
                    LmsEventLmsTasksetAppUserAnswerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AnswerAsFileId = table.Column<int>(nullable: true),
                    AnswerAsText = table.Column<string>(nullable: true),
                    AnswerDateTime = table.Column<DateTime>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    LmsEventLmsTaskSetId = table.Column<int>(nullable: false),
                    LmsTaskId = table.Column<int>(nullable: false),
                    NumberOfPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsEventLmsTasksetsAppUserAnswers", x => x.LmsEventLmsTasksetAppUserAnswerId);
                    table.ForeignKey(
                        name: "FK_LmsEventLmsTasksetsAppUserAnswers_Files_AnswerAsFileId",
                        column: x => x.AnswerAsFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LmsEventLmsTasksetsAppUserAnswers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LmsEventLmsTasksetsAppUserAnswers_LmsEventLmsTaskSets_LmsEventLmsTaskSetId",
                        column: x => x.LmsEventLmsTaskSetId,
                        principalTable: "LmsEventLmsTaskSets",
                        principalColumn: "LmsEventLmsTaskSetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsEventLmsTasksetsAppUserAnswers_LmsTasks_LmsTaskId",
                        column: x => x.LmsTaskId,
                        principalTable: "LmsTasks",
                        principalColumn: "LmsTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsEventLmsTasksetAppUserAnswerTaskAnswers",
                columns: table => new
                {
                    LmsEventLmsTasksetAppUserAnswerTaskAnswerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LmsEventLmsTasksetAppUserAnswerId = table.Column<int>(nullable: false),
                    LmsTaskAnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsEventLmsTasksetAppUserAnswerTaskAnswers", x => x.LmsEventLmsTasksetAppUserAnswerTaskAnswerId);
                    table.ForeignKey(
                        name: "FK_LmsEventLmsTasksetAppUserAnswerTaskAnswers_LmsEventLmsTasksetsAppUserAnswers_LmsEventLmsTasksetAppUserAnswerId",
                        column: x => x.LmsEventLmsTasksetAppUserAnswerId,
                        principalTable: "LmsEventLmsTasksetsAppUserAnswers",
                        principalColumn: "LmsEventLmsTasksetAppUserAnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsEventLmsTasksetAppUserAnswerTaskAnswers_LmsTaskAnswers_LmsTaskAnswerId",
                        column: x => x.LmsTaskAnswerId,
                        principalTable: "LmsTaskAnswers",
                        principalColumn: "LmsTaskAnswerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventLmsTasksetAppUserAnswerTaskAnswers_LmsEventLmsTasksetAppUserAnswerId",
                table: "LmsEventLmsTasksetAppUserAnswerTaskAnswers",
                column: "LmsEventLmsTasksetAppUserAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventLmsTasksetAppUserAnswerTaskAnswers_LmsTaskAnswerId",
                table: "LmsEventLmsTasksetAppUserAnswerTaskAnswers",
                column: "LmsTaskAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventLmsTasksetsAppUserAnswers_AnswerAsFileId",
                table: "LmsEventLmsTasksetsAppUserAnswers",
                column: "AnswerAsFileId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventLmsTasksetsAppUserAnswers_AppUserId",
                table: "LmsEventLmsTasksetsAppUserAnswers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventLmsTasksetsAppUserAnswers_LmsEventLmsTaskSetId",
                table: "LmsEventLmsTasksetsAppUserAnswers",
                column: "LmsEventLmsTaskSetId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventLmsTasksetsAppUserAnswers_LmsTaskId",
                table: "LmsEventLmsTasksetsAppUserAnswers",
                column: "LmsTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsEventLmsTasksetAppUserAnswerTaskAnswers");

            migrationBuilder.DropTable(
                name: "LmsEventLmsTasksetsAppUserAnswers");
        }
    }
}
