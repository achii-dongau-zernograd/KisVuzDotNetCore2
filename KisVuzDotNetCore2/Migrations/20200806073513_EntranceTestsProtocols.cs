using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class EntranceTestsProtocols : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntranceTestsProtocols",
                columns: table => new
                {
                    EntranceTestsProtocolId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientId = table.Column<int>(nullable: false),
                    DataVidachi = table.Column<DateTime>(nullable: false),
                    FileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntranceTestsProtocols", x => x.EntranceTestsProtocolId);
                    table.ForeignKey(
                        name: "FK_EntranceTestsProtocols_Abiturients_AbiturientId",
                        column: x => x.AbiturientId,
                        principalTable: "Abiturients",
                        principalColumn: "AbiturientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntranceTestsProtocols_AbiturientId",
                table: "EntranceTestsProtocols",
                column: "AbiturientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntranceTestsProtocols");
        }
    }
}
