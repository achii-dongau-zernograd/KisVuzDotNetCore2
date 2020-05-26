using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class DocumentSamples : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocumentSamples",
                columns: table => new
                {
                    DocumentSampleId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocumentSampleName = table.Column<string>(nullable: true),
                    EduProfileId = table.Column<int>(nullable: true),
                    FileDataTypeId = table.Column<int>(nullable: false),
                    FileModelId = table.Column<int>(nullable: true),
                    IsBlank = table.Column<bool>(nullable: false),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentSamples", x => x.DocumentSampleId);
                    table.ForeignKey(
                        name: "FK_DocumentSamples_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentSamples_FileDataTypes_FileDataTypeId",
                        column: x => x.FileDataTypeId,
                        principalTable: "FileDataTypes",
                        principalColumn: "FileDataTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentSamples_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSamples_EduProfileId",
                table: "DocumentSamples",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSamples_FileDataTypeId",
                table: "DocumentSamples",
                column: "FileDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentSamples_FileModelId",
                table: "DocumentSamples",
                column: "FileModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentSamples");
        }
    }
}
