using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ConsentToEnrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsentToEnrollments",
                columns: table => new
                {
                    ConsentToEnrollmentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ApplicationForAdmissionId = table.Column<int>(nullable: false),
                    FileModelId = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    RowStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsentToEnrollments", x => x.ConsentToEnrollmentId);
                    table.ForeignKey(
                        name: "FK_ConsentToEnrollments_ApplicationForAdmissions_ApplicationForAdmissionId",
                        column: x => x.ApplicationForAdmissionId,
                        principalTable: "ApplicationForAdmissions",
                        principalColumn: "ApplicationForAdmissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsentToEnrollments_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConsentToEnrollments_RowStatuses_RowStatusId",
                        column: x => x.RowStatusId,
                        principalTable: "RowStatuses",
                        principalColumn: "RowStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsentToEnrollments_ApplicationForAdmissionId",
                table: "ConsentToEnrollments",
                column: "ApplicationForAdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentToEnrollments_FileModelId",
                table: "ConsentToEnrollments",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsentToEnrollments_RowStatusId",
                table: "ConsentToEnrollments",
                column: "RowStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsentToEnrollments");
        }
    }
}
