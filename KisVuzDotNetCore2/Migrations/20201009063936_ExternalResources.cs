using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ExternalResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalResourceTypes",
                columns: table => new
                {
                    ExternalResourceTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExternalResourceTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalResourceTypes", x => x.ExternalResourceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ExternalResources",
                columns: table => new
                {
                    ExternalResourceId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ExternalResourceName = table.Column<string>(nullable: true),
                    ExternalResourceTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalResources", x => x.ExternalResourceId);
                    table.ForeignKey(
                        name: "FK_ExternalResources_ExternalResourceTypes_ExternalResourceTypeId",
                        column: x => x.ExternalResourceTypeId,
                        principalTable: "ExternalResourceTypes",
                        principalColumn: "ExternalResourceTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userAccountExternals",
                columns: table => new
                {
                    UserAccountExternalId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    ExternalResourceId = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userAccountExternals", x => x.UserAccountExternalId);
                    table.ForeignKey(
                        name: "FK_userAccountExternals_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userAccountExternals_ExternalResources_ExternalResourceId",
                        column: x => x.ExternalResourceId,
                        principalTable: "ExternalResources",
                        principalColumn: "ExternalResourceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalResources_ExternalResourceTypeId",
                table: "ExternalResources",
                column: "ExternalResourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_userAccountExternals_AppUserId",
                table: "userAccountExternals",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_userAccountExternals_ExternalResourceId",
                table: "userAccountExternals",
                column: "ExternalResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userAccountExternals");

            migrationBuilder.DropTable(
                name: "ExternalResources");

            migrationBuilder.DropTable(
                name: "ExternalResourceTypes");
        }
    }
}
