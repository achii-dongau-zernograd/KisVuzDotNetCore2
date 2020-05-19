using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AdmissionPrivileges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdmissionPrivilegeTypes",
                columns: table => new
                {
                    AdmissionPrivilegeTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdmissionPrivilegeTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionPrivilegeTypes", x => x.AdmissionPrivilegeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AdmissionPrivileges",
                columns: table => new
                {
                    AdmissionPrivilegeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AdmissionPrivilegeTypeId = table.Column<int>(nullable: false),
                    ApplicationForAdmissionId = table.Column<int>(nullable: false),
                    FileModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdmissionPrivileges", x => x.AdmissionPrivilegeId);
                    table.ForeignKey(
                        name: "FK_AdmissionPrivileges_AdmissionPrivilegeTypes_AdmissionPrivilegeTypeId",
                        column: x => x.AdmissionPrivilegeTypeId,
                        principalTable: "AdmissionPrivilegeTypes",
                        principalColumn: "AdmissionPrivilegeTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionPrivileges_ApplicationForAdmissions_ApplicationForAdmissionId",
                        column: x => x.ApplicationForAdmissionId,
                        principalTable: "ApplicationForAdmissions",
                        principalColumn: "ApplicationForAdmissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdmissionPrivileges_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionPrivileges_AdmissionPrivilegeTypeId",
                table: "AdmissionPrivileges",
                column: "AdmissionPrivilegeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionPrivileges_ApplicationForAdmissionId",
                table: "AdmissionPrivileges",
                column: "ApplicationForAdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AdmissionPrivileges_FileModelId",
                table: "AdmissionPrivileges",
                column: "FileModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdmissionPrivileges");

            migrationBuilder.DropTable(
                name: "AdmissionPrivilegeTypes");
        }
    }
}
