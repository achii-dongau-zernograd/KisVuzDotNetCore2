using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class OborudovaniePomesheni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korpus",
                columns: table => new
                {
                    KorpusId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    KorpusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpus", x => x.KorpusId);
                    table.ForeignKey(
                        name: "FK_Korpus_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PomeshenieType",
                columns: table => new
                {
                    PomeshenieTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PomeshenieTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PomeshenieType", x => x.PomeshenieTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Pomeshenie",
                columns: table => new
                {
                    PomeshenieId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KorpusId = table.Column<int>(nullable: false),
                    PomeshenieName = table.Column<string>(nullable: true),
                    PomeshenieOvz = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pomeshenie", x => x.PomeshenieId);
                    table.ForeignKey(
                        name: "FK_Pomeshenie_Korpus_KorpusId",
                        column: x => x.KorpusId,
                        principalTable: "Korpus",
                        principalColumn: "KorpusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oborudovanie",
                columns: table => new
                {
                    OborudovanieId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OborudovanieCount = table.Column<int>(nullable: false),
                    OborudovanieName = table.Column<string>(nullable: true),
                    PomeshenieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oborudovanie", x => x.OborudovanieId);
                    table.ForeignKey(
                        name: "FK_Oborudovanie_Pomeshenie_PomeshenieId",
                        column: x => x.PomeshenieId,
                        principalTable: "Pomeshenie",
                        principalColumn: "PomeshenieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PomeshenieTypepomesheniya",
                columns: table => new
                {
                    PomeshenieTypepomesheniyaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PomeshenieId = table.Column<int>(nullable: false),
                    PomeshenieTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PomeshenieTypepomesheniya", x => x.PomeshenieTypepomesheniyaId);
                    table.ForeignKey(
                        name: "FK_PomeshenieTypepomesheniya_Pomeshenie_PomeshenieId",
                        column: x => x.PomeshenieId,
                        principalTable: "Pomeshenie",
                        principalColumn: "PomeshenieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PomeshenieTypepomesheniya_PomeshenieType_PomeshenieTypeId",
                        column: x => x.PomeshenieTypeId,
                        principalTable: "PomeshenieType",
                        principalColumn: "PomeshenieTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Korpus_AddressId",
                table: "Korpus",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Oborudovanie_PomeshenieId",
                table: "Oborudovanie",
                column: "PomeshenieId");

            migrationBuilder.CreateIndex(
                name: "IX_Pomeshenie_KorpusId",
                table: "Pomeshenie",
                column: "KorpusId");

            migrationBuilder.CreateIndex(
                name: "IX_PomeshenieTypepomesheniya_PomeshenieId",
                table: "PomeshenieTypepomesheniya",
                column: "PomeshenieId");

            migrationBuilder.CreateIndex(
                name: "IX_PomeshenieTypepomesheniya_PomeshenieTypeId",
                table: "PomeshenieTypepomesheniya",
                column: "PomeshenieTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oborudovanie");

            migrationBuilder.DropTable(
                name: "PomeshenieTypepomesheniya");

            migrationBuilder.DropTable(
                name: "Pomeshenie");

            migrationBuilder.DropTable(
                name: "PomeshenieType");

            migrationBuilder.DropTable(
                name: "Korpus");
        }
    }
}
