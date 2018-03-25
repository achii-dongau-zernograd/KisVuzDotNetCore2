using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AddingEduYearsYearBeginningPlanStructInstitutesFacultetsKafs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EduYearBeginningTrainings",
                columns: table => new
                {
                    EduYearBeginningTrainingId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduYearBeginningTrainingName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduYearBeginningTrainings", x => x.EduYearBeginningTrainingId);
                });

            migrationBuilder.CreateTable(
                name: "EduYears",
                columns: table => new
                {
                    EduYearId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduYearName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduYears", x => x.EduYearId);
                });

            migrationBuilder.CreateTable(
                name: "StructInstitutes",
                columns: table => new
                {
                    StructInstituteId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructInstituteName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructInstitutes", x => x.StructInstituteId);
                });

            migrationBuilder.CreateTable(
                name: "EduPlans",
                columns: table => new
                {
                    EduPlanId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduProfileId = table.Column<int>(nullable: false),
                    EduYearBeginningTrainingId = table.Column<int>(nullable: true),
                    EduYearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduPlans", x => x.EduPlanId);
                    table.ForeignKey(
                        name: "FK_EduPlans_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPlans_EduYearBeginningTrainings_EduYearBeginningTrainingId",
                        column: x => x.EduYearBeginningTrainingId,
                        principalTable: "EduYearBeginningTrainings",
                        principalColumn: "EduYearBeginningTrainingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EduPlans_EduYears_EduYearId",
                        column: x => x.EduYearId,
                        principalTable: "EduYears",
                        principalColumn: "EduYearId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StructFacultets",
                columns: table => new
                {
                    StructFacultetId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructFacultetName = table.Column<string>(nullable: true),
                    StructInstituteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructFacultets", x => x.StructFacultetId);
                    table.ForeignKey(
                        name: "FK_StructFacultets_StructInstitutes_StructInstituteId",
                        column: x => x.StructInstituteId,
                        principalTable: "StructInstitutes",
                        principalColumn: "StructInstituteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructKafs",
                columns: table => new
                {
                    StructKafId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructFacultetId = table.Column<int>(nullable: false),
                    StructKafName = table.Column<int>(nullable: false),
                    StructKafNameSokr = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructKafs", x => x.StructKafId);
                    table.ForeignKey(
                        name: "FK_StructKafs_StructFacultets_StructFacultetId",
                        column: x => x.StructFacultetId,
                        principalTable: "StructFacultets",
                        principalColumn: "StructFacultetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduProfileId",
                table: "EduPlans",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduYearBeginningTrainingId",
                table: "EduPlans",
                column: "EduYearBeginningTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduYearId",
                table: "EduPlans",
                column: "EduYearId");

            migrationBuilder.CreateIndex(
                name: "IX_StructFacultets_StructInstituteId",
                table: "StructFacultets",
                column: "StructInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_StructKafs_StructFacultetId",
                table: "StructKafs",
                column: "StructFacultetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EduPlans");

            migrationBuilder.DropTable(
                name: "StructKafs");

            migrationBuilder.DropTable(
                name: "EduYearBeginningTrainings");

            migrationBuilder.DropTable(
                name: "EduYears");

            migrationBuilder.DropTable(
                name: "StructFacultets");

            migrationBuilder.DropTable(
                name: "StructInstitutes");
        }
    }
}
