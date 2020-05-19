using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class LmsEventTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LmsEventTypeGroups",
                columns: table => new
                {
                    LmsEventTypeGroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LmsEventTypeGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsEventTypeGroups", x => x.LmsEventTypeGroupId);
                });

            migrationBuilder.CreateTable(
                name: "LmsEventTypes",
                columns: table => new
                {
                    LmsEventTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LmsEventTypeGroupId = table.Column<int>(nullable: false),
                    LmsEventTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsEventTypes", x => x.LmsEventTypeId);
                    table.ForeignKey(
                        name: "FK_LmsEventTypes_LmsEventTypeGroups_LmsEventTypeGroupId",
                        column: x => x.LmsEventTypeGroupId,
                        principalTable: "LmsEventTypeGroups",
                        principalColumn: "LmsEventTypeGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsEvents",
                columns: table => new
                {
                    LmsEventId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateTimeStart = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: false),
                    LmsEventTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsEvents", x => x.LmsEventId);
                    table.ForeignKey(
                        name: "FK_LmsEvents_LmsEventTypes_LmsEventTypeId",
                        column: x => x.LmsEventTypeId,
                        principalTable: "LmsEventTypes",
                        principalColumn: "LmsEventTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AbiturientLmsEvents",
                columns: table => new
                {
                    AbiturientLmsEventId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientId = table.Column<int>(nullable: false),
                    LmsEventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbiturientLmsEvents", x => x.AbiturientLmsEventId);
                    table.ForeignKey(
                        name: "FK_AbiturientLmsEvents_Abiturients_AbiturientId",
                        column: x => x.AbiturientId,
                        principalTable: "Abiturients",
                        principalColumn: "AbiturientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbiturientLmsEvents_LmsEvents_LmsEventId",
                        column: x => x.LmsEventId,
                        principalTable: "LmsEvents",
                        principalColumn: "LmsEventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbiturientLmsEvents_AbiturientId",
                table: "AbiturientLmsEvents",
                column: "AbiturientId");

            migrationBuilder.CreateIndex(
                name: "IX_AbiturientLmsEvents_LmsEventId",
                table: "AbiturientLmsEvents",
                column: "LmsEventId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEvents_LmsEventTypeId",
                table: "LmsEvents",
                column: "LmsEventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsEventTypes_LmsEventTypeGroupId",
                table: "LmsEventTypes",
                column: "LmsEventTypeGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbiturientLmsEvents");

            migrationBuilder.DropTable(
                name: "LmsEvents");

            migrationBuilder.DropTable(
                name: "LmsEventTypes");

            migrationBuilder.DropTable(
                name: "LmsEventTypeGroups");
        }
    }
}
