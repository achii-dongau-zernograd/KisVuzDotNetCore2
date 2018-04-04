using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class eduChislen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EduChislens",
                columns: table => new
                {
                    EduChislenId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduProfileId = table.Column<int>(nullable: false),
                    NumberBFpriem = table.Column<int>(nullable: false),
                    NumberBMpriem = table.Column<int>(nullable: false),
                    NumberBRpriem = table.Column<int>(nullable: false),
                    NumberPpriem = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduChislens", x => x.EduChislenId);
                    table.ForeignKey(
                        name: "FK_EduChislens_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduChislens_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduChislens_EduFormId",
                table: "EduChislens",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_EduChislens_EduProfileId",
                table: "EduChislens",
                column: "EduProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduChislens");
        }
    }
}
