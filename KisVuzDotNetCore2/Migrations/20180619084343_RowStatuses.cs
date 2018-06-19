using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class RowStatuses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RowStatusId",
                table: "Qualifications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RowStatuses",
                columns: table => new
                {
                    RowStatusId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RowStatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RowStatuses", x => x.RowStatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_RowStatusId",
                table: "Qualifications",
                column: "RowStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualifications_RowStatuses_RowStatusId",
                table: "Qualifications",
                column: "RowStatusId",
                principalTable: "RowStatuses",
                principalColumn: "RowStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qualifications_RowStatuses_RowStatusId",
                table: "Qualifications");

            migrationBuilder.DropTable(
                name: "RowStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Qualifications_RowStatusId",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "RowStatusId",
                table: "Qualifications");
        }
    }
}
