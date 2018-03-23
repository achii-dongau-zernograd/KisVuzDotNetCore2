using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AddingEduLevelsAndEduUgses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EduLevels",
                columns: table => new
                {
                    EduLevelId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduLevelName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduLevels", x => x.EduLevelId);
                });

            migrationBuilder.CreateTable(
                name: "EduUgses",
                columns: table => new
                {
                    EduUgsId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduLevelId = table.Column<int>(nullable: false),
                    EduUgsCode = table.Column<string>(nullable: true),
                    EduUgsName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduUgses", x => x.EduUgsId);
                    table.ForeignKey(
                        name: "FK_EduUgses_EduLevels_EduLevelId",
                        column: x => x.EduLevelId,
                        principalTable: "EduLevels",
                        principalColumn: "EduLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduUgses_EduLevelId",
                table: "EduUgses",
                column: "EduLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduUgses");

            migrationBuilder.DropTable(
                name: "EduLevels");
        }
    }
}
