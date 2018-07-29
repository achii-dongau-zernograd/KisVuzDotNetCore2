using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class rowstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RowStatusId",
                table: "RefresherCourses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowStatusId",
                table: "ProfessionalRetrainings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefresherCourses_RowStatusId",
                table: "RefresherCourses",
                column: "RowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalRetrainings_RowStatusId",
                table: "ProfessionalRetrainings",
                column: "RowStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessionalRetrainings_RowStatuses_RowStatusId",
                table: "ProfessionalRetrainings",
                column: "RowStatusId",
                principalTable: "RowStatuses",
                principalColumn: "RowStatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefresherCourses_RowStatuses_RowStatusId",
                table: "RefresherCourses",
                column: "RowStatusId",
                principalTable: "RowStatuses",
                principalColumn: "RowStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessionalRetrainings_RowStatuses_RowStatusId",
                table: "ProfessionalRetrainings");

            migrationBuilder.DropForeignKey(
                name: "FK_RefresherCourses_RowStatuses_RowStatusId",
                table: "RefresherCourses");

            migrationBuilder.DropIndex(
                name: "IX_RefresherCourses_RowStatusId",
                table: "RefresherCourses");

            migrationBuilder.DropIndex(
                name: "IX_ProfessionalRetrainings_RowStatusId",
                table: "ProfessionalRetrainings");

            migrationBuilder.DropColumn(
                name: "RowStatusId",
                table: "RefresherCourses");

            migrationBuilder.DropColumn(
                name: "RowStatusId",
                table: "ProfessionalRetrainings");
        }
    }
}
