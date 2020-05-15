using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AbiturientIndividualAchievments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbiturientIndividualAchievmentTypes",
                columns: table => new
                {
                    AbiturientIndividualAchievmentTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientIndividualAchievmentTypeName = table.Column<string>(nullable: true),
                    Point = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbiturientIndividualAchievmentTypes", x => x.AbiturientIndividualAchievmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AbiturientIndividualAchievments",
                columns: table => new
                {
                    AbiturientIndividualAchievmentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientId = table.Column<int>(nullable: false),
                    AbiturientIndividualAchievmentTypeId = table.Column<int>(nullable: false),
                    FileModelId = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    RowStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbiturientIndividualAchievments", x => x.AbiturientIndividualAchievmentId);
                    table.ForeignKey(
                        name: "FK_AbiturientIndividualAchievments_Abiturients_AbiturientId",
                        column: x => x.AbiturientId,
                        principalTable: "Abiturients",
                        principalColumn: "AbiturientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbiturientIndividualAchievments_AbiturientIndividualAchievmentTypes_AbiturientIndividualAchievmentTypeId",
                        column: x => x.AbiturientIndividualAchievmentTypeId,
                        principalTable: "AbiturientIndividualAchievmentTypes",
                        principalColumn: "AbiturientIndividualAchievmentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbiturientIndividualAchievments_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbiturientIndividualAchievments_RowStatuses_RowStatusId",
                        column: x => x.RowStatusId,
                        principalTable: "RowStatuses",
                        principalColumn: "RowStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbiturientIndividualAchievments_AbiturientId",
                table: "AbiturientIndividualAchievments",
                column: "AbiturientId");

            migrationBuilder.CreateIndex(
                name: "IX_AbiturientIndividualAchievments_AbiturientIndividualAchievmentTypeId",
                table: "AbiturientIndividualAchievments",
                column: "AbiturientIndividualAchievmentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AbiturientIndividualAchievments_FileModelId",
                table: "AbiturientIndividualAchievments",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AbiturientIndividualAchievments_RowStatusId",
                table: "AbiturientIndividualAchievments",
                column: "RowStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbiturientIndividualAchievments");

            migrationBuilder.DropTable(
                name: "AbiturientIndividualAchievmentTypes");
        }
    }
}
