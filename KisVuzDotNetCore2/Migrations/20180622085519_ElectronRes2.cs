using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ElectronRes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectronObrazovatInformRes",
                columns: table => new
                {
                    ElectronObrazovatInformResId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DescriptionRes = table.Column<string>(nullable: true),
                    IsSobstv = table.Column<bool>(nullable: false),
                    LinkRes = table.Column<string>(nullable: true),
                    NameRes = table.Column<string>(nullable: true),
                    ResId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronObrazovatInformRes", x => x.ElectronObrazovatInformResId);
                    table.ForeignKey(
                        name: "FK_ElectronObrazovatInformRes_Files_ResId",
                        column: x => x.ResId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ElectronObrazovatInformRes_ResId",
                table: "ElectronObrazovatInformRes",
                column: "ResId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectronObrazovatInformRes");
        }
    }
}
