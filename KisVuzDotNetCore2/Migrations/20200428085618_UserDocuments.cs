using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class UserDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDocuments",
                columns: table => new
                {
                    UserDocumentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    FileDataTypeId = table.Column<int>(nullable: false),
                    FileModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocuments", x => x.UserDocumentId);
                    table.ForeignKey(
                        name: "FK_UserDocuments_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDocuments_FileDataTypes_FileDataTypeId",
                        column: x => x.FileDataTypeId,
                        principalTable: "FileDataTypes",
                        principalColumn: "FileDataTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDocuments_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_AppUserId",
                table: "UserDocuments",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_FileDataTypeId",
                table: "UserDocuments",
                column: "FileDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_FileModelId",
                table: "UserDocuments",
                column: "FileModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDocuments");
        }
    }
}
