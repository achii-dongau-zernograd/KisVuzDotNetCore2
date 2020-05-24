using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AbiturientLmsEventsDrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbiturientLmsEvents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbiturientLmsEvents",
                columns: table => new
                {
                    AbiturientLmsEventId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientId = table.Column<int>(nullable: false),
                    IsAccepted = table.Column<bool>(nullable: false),
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
        }
    }
}
