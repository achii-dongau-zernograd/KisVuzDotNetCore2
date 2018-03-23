using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AddingEduNapravls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EduNapravls",
                columns: table => new
                {
                    EduNapravlId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravCode = table.Column<string>(nullable: true),
                    EduNapravName = table.Column<string>(nullable: true),
                    EduUgsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduNapravls", x => x.EduNapravlId);
                    table.ForeignKey(
                        name: "FK_EduNapravls_EduUgses_EduUgsId",
                        column: x => x.EduUgsId,
                        principalTable: "EduUgses",
                        principalColumn: "EduUgsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduNapravls_EduUgsId",
                table: "EduNapravls",
                column: "EduUgsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduNapravls");
        }
    }
}
