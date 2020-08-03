using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class RevocationStatements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RevocationStatements",
                columns: table => new
                {
                    RevocationStatementId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicationForAdmissionId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    FileModelId = table.Column<int>(nullable: false),
                    GeneratedPdfDocumentPath = table.Column<string>(nullable: true),
                    RejectionReason = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    RowStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevocationStatements", x => x.RevocationStatementId);
                    table.ForeignKey(
                        name: "FK_RevocationStatements_ApplicationForAdmissions_ApplicationForAdmissionId",
                        column: x => x.ApplicationForAdmissionId,
                        principalTable: "ApplicationForAdmissions",
                        principalColumn: "ApplicationForAdmissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RevocationStatements_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RevocationStatements_RowStatuses_RowStatusId",
                        column: x => x.RowStatusId,
                        principalTable: "RowStatuses",
                        principalColumn: "RowStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RevocationStatements_ApplicationForAdmissionId",
                table: "RevocationStatements",
                column: "ApplicationForAdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RevocationStatements_FileModelId",
                table: "RevocationStatements",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RevocationStatements_RowStatusId",
                table: "RevocationStatements",
                column: "RowStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RevocationStatements");
        }
    }
}
