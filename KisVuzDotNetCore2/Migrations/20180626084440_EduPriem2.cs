using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class EduPriem2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EduPriem",
                columns: table => new
                {
                    EduPriemId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduNapravlId = table.Column<int>(nullable: false),
                    FinBF = table.Column<string>(nullable: true),
                    FinBM = table.Column<string>(nullable: true),
                    FinBR = table.Column<string>(nullable: true),
                    FinPV = table.Column<string>(nullable: true),
                    SredSum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduPriem", x => x.EduPriemId);
                    table.ForeignKey(
                        name: "FK_EduPriem_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPriem_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduPriem_EduFormId",
                table: "EduPriem",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPriem_EduNapravlId",
                table: "EduPriem",
                column: "EduNapravlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduPriem");
        }
    }
}
