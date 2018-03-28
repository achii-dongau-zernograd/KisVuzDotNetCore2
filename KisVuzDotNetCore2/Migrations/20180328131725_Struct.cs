using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class Struct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StructSubvisionTypes",
                columns: table => new
                {
                    StructSubvisionTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructSubvisionTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructSubvisionTypes", x => x.StructSubvisionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StructSubvisions",
                columns: table => new
                {
                    StructSubvisionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructStandingOrderId = table.Column<int>(nullable: true),
                    StructSubvisionAdressId = table.Column<int>(nullable: false),
                    StructSubvisionEmailEmailId = table.Column<int>(nullable: true),
                    StructSubvisionFioChief = table.Column<string>(nullable: true),
                    StructSubvisionName = table.Column<string>(nullable: true),
                    StructSubvisionPostChief = table.Column<string>(nullable: true),
                    StructSubvisionSite = table.Column<string>(nullable: true),
                    StructSubvisionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructSubvisions", x => x.StructSubvisionId);
                    table.ForeignKey(
                        name: "FK_StructSubvisions_Files_StructStandingOrderId",
                        column: x => x.StructStandingOrderId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StructSubvisions_Addresses_StructSubvisionAdressId",
                        column: x => x.StructSubvisionAdressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StructSubvisions_Emails_StructSubvisionEmailEmailId",
                        column: x => x.StructSubvisionEmailEmailId,
                        principalTable: "Emails",
                        principalColumn: "EmailId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StructSubvisions_StructSubvisionTypes_StructSubvisionTypeId",
                        column: x => x.StructSubvisionTypeId,
                        principalTable: "StructSubvisionTypes",
                        principalColumn: "StructSubvisionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructStandingOrderId",
                table: "StructSubvisions",
                column: "StructStandingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructSubvisionAdressId",
                table: "StructSubvisions",
                column: "StructSubvisionAdressId");

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructSubvisionEmailEmailId",
                table: "StructSubvisions",
                column: "StructSubvisionEmailEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructSubvisionTypeId",
                table: "StructSubvisions",
                column: "StructSubvisionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StructSubvisions");

            migrationBuilder.DropTable(
                name: "StructSubvisionTypes");
        }
    }
}
