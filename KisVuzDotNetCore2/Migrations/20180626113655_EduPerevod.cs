using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class EduPerevod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "eduPerevod",
                columns: table => new
                {
                    EduPerevodId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduNapravlId = table.Column<int>(nullable: false),
                    NumberExpPerevod = table.Column<string>(nullable: true),
                    NumberOutPerevod = table.Column<string>(nullable: true),
                    NumberResPerevod = table.Column<string>(nullable: true),
                    NumberToPerevod = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eduPerevod", x => x.EduPerevodId);
                    table.ForeignKey(
                        name: "FK_eduPerevod_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eduPerevod_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_eduPerevod_EduFormId",
                table: "eduPerevod",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_eduPerevod_EduNapravlId",
                table: "eduPerevod",
                column: "EduNapravlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "eduPerevod");
        }
    }
}
