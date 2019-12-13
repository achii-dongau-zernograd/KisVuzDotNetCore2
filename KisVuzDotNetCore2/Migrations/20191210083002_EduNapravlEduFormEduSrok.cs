using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class EduNapravlEduFormEduSrok : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EduNapravlStandartDocLinkFgos3pp",
                table: "EduNapravls",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EduNapravlEduFormEduSroks",
                columns: table => new
                {
                    EduNapravlEduFormEduSrokId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduNapravlId = table.Column<int>(nullable: false),
                    EduSrokId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduNapravlEduFormEduSroks", x => x.EduNapravlEduFormEduSrokId);
                    table.ForeignKey(
                        name: "FK_EduNapravlEduFormEduSroks_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduNapravlEduFormEduSroks_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduNapravlEduFormEduSroks_EduSrok_EduSrokId",
                        column: x => x.EduSrokId,
                        principalTable: "EduSrok",
                        principalColumn: "EduSrokId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduNapravlEduFormEduSroks_EduFormId",
                table: "EduNapravlEduFormEduSroks",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_EduNapravlEduFormEduSroks_EduNapravlId",
                table: "EduNapravlEduFormEduSroks",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_EduNapravlEduFormEduSroks_EduSrokId",
                table: "EduNapravlEduFormEduSroks",
                column: "EduSrokId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduNapravlEduFormEduSroks");

            migrationBuilder.DropColumn(
                name: "EduNapravlStandartDocLinkFgos3pp",
                table: "EduNapravls");
        }
    }
}
