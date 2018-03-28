using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class university : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "StructInstitutes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "StructInstitutes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "ExistenceOfFilials",
                table: "StructInstitutes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UniversityId",
                table: "StructInstitutes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkingRegime",
                table: "StructInstitutes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingSchedule",
                table: "StructInstitutes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Settlement = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "StructUniversities",
                columns: table => new
                {
                    StructUniversityId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    DateOfCreation = table.Column<DateTime>(nullable: false),
                    ExistenceOfFilials = table.Column<bool>(nullable: false),
                    StructUniversityName = table.Column<string>(nullable: true),
                    WorkingRegime = table.Column<string>(nullable: true),
                    WorkingSchedule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructUniversities", x => x.StructUniversityId);
                    table.ForeignKey(
                        name: "FK_StructUniversities_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmailComment = table.Column<string>(nullable: true),
                    EmailValue = table.Column<string>(nullable: true),
                    StructInstituteId = table.Column<int>(nullable: true),
                    StructUniversityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailId);
                    table.ForeignKey(
                        name: "FK_Emails_StructInstitutes_StructInstituteId",
                        column: x => x.StructInstituteId,
                        principalTable: "StructInstitutes",
                        principalColumn: "StructInstituteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emails_StructUniversities_StructUniversityId",
                        column: x => x.StructUniversityId,
                        principalTable: "StructUniversities",
                        principalColumn: "StructUniversityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Faxes",
                columns: table => new
                {
                    FaxId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FaxComment = table.Column<string>(nullable: true),
                    FaxValue = table.Column<string>(nullable: true),
                    StructInstituteId = table.Column<int>(nullable: true),
                    StructUniversityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faxes", x => x.FaxId);
                    table.ForeignKey(
                        name: "FK_Faxes_StructInstitutes_StructInstituteId",
                        column: x => x.StructInstituteId,
                        principalTable: "StructInstitutes",
                        principalColumn: "StructInstituteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faxes_StructUniversities_StructUniversityId",
                        column: x => x.StructUniversityId,
                        principalTable: "StructUniversities",
                        principalColumn: "StructUniversityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Telephones",
                columns: table => new
                {
                    TelephoneId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructInstituteId = table.Column<int>(nullable: true),
                    StructUniversityId = table.Column<int>(nullable: true),
                    TelephoneComment = table.Column<string>(nullable: true),
                    TelephoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telephones", x => x.TelephoneId);
                    table.ForeignKey(
                        name: "FK_Telephones_StructInstitutes_StructInstituteId",
                        column: x => x.StructInstituteId,
                        principalTable: "StructInstitutes",
                        principalColumn: "StructInstituteId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Telephones_StructUniversities_StructUniversityId",
                        column: x => x.StructUniversityId,
                        principalTable: "StructUniversities",
                        principalColumn: "StructUniversityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StructInstitutes_AddressId",
                table: "StructInstitutes",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_StructInstitutes_UniversityId",
                table: "StructInstitutes",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_StructInstituteId",
                table: "Emails",
                column: "StructInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_StructUniversityId",
                table: "Emails",
                column: "StructUniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Faxes_StructInstituteId",
                table: "Faxes",
                column: "StructInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Faxes_StructUniversityId",
                table: "Faxes",
                column: "StructUniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_StructUniversities_AddressId",
                table: "StructUniversities",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Telephones_StructInstituteId",
                table: "Telephones",
                column: "StructInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_Telephones_StructUniversityId",
                table: "Telephones",
                column: "StructUniversityId");

            migrationBuilder.AddForeignKey(
                name: "FK_StructInstitutes_Addresses_AddressId",
                table: "StructInstitutes",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StructInstitutes_StructUniversities_UniversityId",
                table: "StructInstitutes",
                column: "UniversityId",
                principalTable: "StructUniversities",
                principalColumn: "StructUniversityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StructInstitutes_Addresses_AddressId",
                table: "StructInstitutes");

            migrationBuilder.DropForeignKey(
                name: "FK_StructInstitutes_StructUniversities_UniversityId",
                table: "StructInstitutes");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Faxes");

            migrationBuilder.DropTable(
                name: "Telephones");

            migrationBuilder.DropTable(
                name: "StructUniversities");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_StructInstitutes_AddressId",
                table: "StructInstitutes");

            migrationBuilder.DropIndex(
                name: "IX_StructInstitutes_UniversityId",
                table: "StructInstitutes");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "StructInstitutes");

            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "StructInstitutes");

            migrationBuilder.DropColumn(
                name: "ExistenceOfFilials",
                table: "StructInstitutes");

            migrationBuilder.DropColumn(
                name: "UniversityId",
                table: "StructInstitutes");

            migrationBuilder.DropColumn(
                name: "WorkingRegime",
                table: "StructInstitutes");

            migrationBuilder.DropColumn(
                name: "WorkingSchedule",
                table: "StructInstitutes");
        }
    }
}
