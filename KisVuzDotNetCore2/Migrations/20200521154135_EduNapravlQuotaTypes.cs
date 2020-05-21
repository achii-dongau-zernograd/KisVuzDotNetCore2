using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class EduNapravlQuotaTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EduNapravlQuotaTypes",
                columns: table => new
                {
                    EduNapravlQuotaTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlId = table.Column<int>(nullable: false),
                    QuotaTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduNapravlQuotaTypes", x => x.EduNapravlQuotaTypeId);
                    table.ForeignKey(
                        name: "FK_EduNapravlQuotaTypes_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduNapravlQuotaTypes_QuotaTypes_QuotaTypeId",
                        column: x => x.QuotaTypeId,
                        principalTable: "QuotaTypes",
                        principalColumn: "QuotaTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduNapravlQuotaTypes_EduNapravlId",
                table: "EduNapravlQuotaTypes",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_EduNapravlQuotaTypes_QuotaTypeId",
                table: "EduNapravlQuotaTypes",
                column: "QuotaTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduNapravlQuotaTypes");
        }
    }
}
