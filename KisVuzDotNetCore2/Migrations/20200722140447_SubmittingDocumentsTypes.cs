using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class SubmittingDocumentsTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubmittingDocumentsTypeId",
                table: "Abiturients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubmittingDocumentsTypes",
                columns: table => new
                {
                    SubmittingDocumentsTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubmittingDocumentsTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmittingDocumentsTypes", x => x.SubmittingDocumentsTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abiturients_SubmittingDocumentsTypeId",
                table: "Abiturients",
                column: "SubmittingDocumentsTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abiturients_SubmittingDocumentsTypes_SubmittingDocumentsTypeId",
                table: "Abiturients",
                column: "SubmittingDocumentsTypeId",
                principalTable: "SubmittingDocumentsTypes",
                principalColumn: "SubmittingDocumentsTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abiturients_SubmittingDocumentsTypes_SubmittingDocumentsTypeId",
                table: "Abiturients");

            migrationBuilder.DropTable(
                name: "SubmittingDocumentsTypes");

            migrationBuilder.DropIndex(
                name: "IX_Abiturients_SubmittingDocumentsTypeId",
                table: "Abiturients");

            migrationBuilder.DropColumn(
                name: "SubmittingDocumentsTypeId",
                table: "Abiturients");
        }
    }
}
