using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class BlankNum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlankNums",
                columns: table => new
                {
                    BlankNumId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlId = table.Column<int>(nullable: false),
                    Och = table.Column<int>(nullable: false),
                    OchZaoch = table.Column<int>(nullable: false),
                    Zaoch = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlankNums", x => x.BlankNumId);
                    table.ForeignKey(
                        name: "FK_BlankNums_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlankNums_EduNapravlId",
                table: "BlankNums",
                column: "EduNapravlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlankNums");
        }
    }
}
