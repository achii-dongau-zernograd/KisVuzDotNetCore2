using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class LmsTaskAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfPoints",
                table: "LmsTasks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LmsTaskAnswers",
                columns: table => new
                {
                    LmsTaskAnswerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileModelId = table.Column<int>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false),
                    LmsTaskAnswerText = table.Column<string>(nullable: true),
                    LmsTaskId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsTaskAnswers", x => x.LmsTaskAnswerId);
                    table.ForeignKey(
                        name: "FK_LmsTaskAnswers_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LmsTaskAnswers_LmsTasks_LmsTaskId",
                        column: x => x.LmsTaskId,
                        principalTable: "LmsTasks",
                        principalColumn: "LmsTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LmsTaskAnswers_FileModelId",
                table: "LmsTaskAnswers",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsTaskAnswers_LmsTaskId",
                table: "LmsTaskAnswers",
                column: "LmsTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LmsTaskAnswers");

            migrationBuilder.DropColumn(
                name: "NumberOfPoints",
                table: "LmsTasks");
        }
    }
}
