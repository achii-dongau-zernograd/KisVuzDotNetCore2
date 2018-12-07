using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ScienceJournalAddingClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScienceJournalAddingClaims",
                columns: table => new
                {
                    ScienceJournalAddingClaimId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CitationBasesList = table.Column<string>(nullable: true),
                    ELibraryLink = table.Column<string>(nullable: true),
                    IsVak = table.Column<bool>(nullable: false),
                    IsZarubejn = table.Column<bool>(nullable: false),
                    RowStatusId = table.Column<int>(nullable: true),
                    ScienceJournalName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScienceJournalAddingClaims", x => x.ScienceJournalAddingClaimId);
                    table.ForeignKey(
                        name: "FK_ScienceJournalAddingClaims_RowStatuses_RowStatusId",
                        column: x => x.RowStatusId,
                        principalTable: "RowStatuses",
                        principalColumn: "RowStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScienceJournalAddingClaims_RowStatusId",
                table: "ScienceJournalAddingClaims",
                column: "RowStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScienceJournalAddingClaims");
        }
    }
}
