using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class FileDataTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileDataTypes",
                columns: table => new
                {
                    FileDataTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileDataTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDataTypes", x => x.FileDataTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FileToFileTypes",
                columns: table => new
                {
                    FileToFileTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileDataTypeId = table.Column<int>(nullable: false),
                    FileModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileToFileTypes", x => x.FileToFileTypeId);
                    table.ForeignKey(
                        name: "FK_FileToFileTypes_FileDataTypes_FileDataTypeId",
                        column: x => x.FileDataTypeId,
                        principalTable: "FileDataTypes",
                        principalColumn: "FileDataTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileToFileTypes_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileToFileTypes_FileDataTypeId",
                table: "FileToFileTypes",
                column: "FileDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FileToFileTypes_FileModelId",
                table: "FileToFileTypes",
                column: "FileModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileToFileTypes");

            migrationBuilder.DropTable(
                name: "FileDataTypes");
        }
    }
}
