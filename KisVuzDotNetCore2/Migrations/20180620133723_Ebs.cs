using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class Ebs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectronBiblSystem",
                columns: table => new
                {
                    ElectronBiblSystemId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CopyDogovorId = table.Column<int>(nullable: true),
                    DateEnd = table.Column<DateTime>(nullable: true),
                    DateStart = table.Column<DateTime>(nullable: false),
                    LinkEbs = table.Column<string>(nullable: true),
                    NameEbs = table.Column<string>(nullable: true),
                    NumberDogovor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronBiblSystem", x => x.ElectronBiblSystemId);
                    table.ForeignKey(
                        name: "FK_ElectronBiblSystem_Files_CopyDogovorId",
                        column: x => x.CopyDogovorId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectronBiblSystem_CopyDogovorId",
                table: "ElectronBiblSystem",
                column: "CopyDogovorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectronBiblSystem");
        }
    }
}
