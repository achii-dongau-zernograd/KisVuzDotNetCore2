using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class EduPOAccreds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EduPOAccreds",
                columns: table => new
                {
                    EduPOAccredId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccredDateEnd = table.Column<DateTime>(nullable: false),
                    AccredOrgName = table.Column<string>(nullable: true),
                    EduNapravlId = table.Column<int>(nullable: false),
                    EduPOAccredPdfId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduPOAccreds", x => x.EduPOAccredId);
                    table.ForeignKey(
                        name: "FK_EduPOAccreds_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPOAccreds_Files_EduPOAccredPdfId",
                        column: x => x.EduPOAccredPdfId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduPOAccreds_EduNapravlId",
                table: "EduPOAccreds",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPOAccreds_EduPOAccredPdfId",
                table: "EduPOAccreds",
                column: "EduPOAccredPdfId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduPOAccreds");
        }
    }
}
