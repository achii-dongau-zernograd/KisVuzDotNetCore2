using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class metodkomissiya : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetodKomissii",
                columns: table => new
                {
                    MetodKomissiyaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MetodKomissiyaName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodKomissii", x => x.MetodKomissiyaId);
                });

            migrationBuilder.CreateTable(
                name: "MetodKomissiyaEduProfiles",
                columns: table => new
                {
                    MetodKomissiyaEduProfileId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduProfileId = table.Column<int>(nullable: false),
                    MetodKomissiyaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodKomissiyaEduProfiles", x => x.MetodKomissiyaEduProfileId);
                    table.ForeignKey(
                        name: "FK_MetodKomissiyaEduProfiles_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetodKomissiyaEduProfiles_MetodKomissii_MetodKomissiyaId",
                        column: x => x.MetodKomissiyaId,
                        principalTable: "MetodKomissii",
                        principalColumn: "MetodKomissiyaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherMetodKomissiya",
                columns: table => new
                {
                    TeacherMetodKomissiyaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MetodKomissiyaId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherMetodKomissiya", x => x.TeacherMetodKomissiyaId);
                    table.ForeignKey(
                        name: "FK_TeacherMetodKomissiya_MetodKomissii_MetodKomissiyaId",
                        column: x => x.MetodKomissiyaId,
                        principalTable: "MetodKomissii",
                        principalColumn: "MetodKomissiyaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherMetodKomissiya_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MetodKomissiyaEduProfiles_EduProfileId",
                table: "MetodKomissiyaEduProfiles",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_MetodKomissiyaEduProfiles_MetodKomissiyaId",
                table: "MetodKomissiyaEduProfiles",
                column: "MetodKomissiyaId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherMetodKomissiya_MetodKomissiyaId",
                table: "TeacherMetodKomissiya",
                column: "MetodKomissiyaId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherMetodKomissiya_TeacherId",
                table: "TeacherMetodKomissiya",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetodKomissiyaEduProfiles");

            migrationBuilder.DropTable(
                name: "TeacherMetodKomissiya");

            migrationBuilder.DropTable(
                name: "MetodKomissii");
        }
    }
}
