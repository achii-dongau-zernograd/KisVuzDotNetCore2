using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class NirSpecialEduProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NirSpecialEduProfiles",
                columns: table => new
                {
                    NirSpecialEduProfileId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduProfileId = table.Column<int>(nullable: false),
                    NirSpecialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NirSpecialEduProfiles", x => x.NirSpecialEduProfileId);
                    table.ForeignKey(
                        name: "FK_NirSpecialEduProfiles_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NirSpecialEduProfiles_NirSpecials_NirSpecialId",
                        column: x => x.NirSpecialId,
                        principalTable: "NirSpecials",
                        principalColumn: "NirSpecialId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NirSpecialEduProfiles_EduProfileId",
                table: "NirSpecialEduProfiles",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_NirSpecialEduProfiles_NirSpecialId",
                table: "NirSpecialEduProfiles",
                column: "NirSpecialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NirSpecialEduProfiles");
        }
    }
}
