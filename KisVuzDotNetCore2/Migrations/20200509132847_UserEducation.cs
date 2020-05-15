using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class UserEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "UserDocuments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowStatusId",
                table: "UserDocuments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PopulatedLocalityId",
                table: "Addresses",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "EducationalInstitutionTypes",
                columns: table => new
                {
                    EducationalInstitutionTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EducationalInstitutionTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstitutionTypes", x => x.EducationalInstitutionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "GpsCoordinates",
                columns: table => new
                {
                    GpsCoordinateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Latitude = table.Column<decimal>(nullable: false),
                    Longitude = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsCoordinates", x => x.GpsCoordinateId);
                });

            migrationBuilder.CreateTable(
                name: "PopulatedLocalityTypes",
                columns: table => new
                {
                    PopulatedLocalityTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PopulatedLocalityTypeName = table.Column<string>(nullable: true),
                    PopulatedLocalityTypeNameShort = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopulatedLocalityTypes", x => x.PopulatedLocalityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<int>(nullable: false),
                    RegionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: true),
                    GpsCoordinateId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Locations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Locations_GpsCoordinates_GpsCoordinateId",
                        column: x => x.GpsCoordinateId,
                        principalTable: "GpsCoordinates",
                        principalColumn: "GpsCoordinateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DistrictName = table.Column<string>(nullable: true),
                    GpsGeometryCenterId = table.Column<int>(nullable: true),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_Districts_GpsCoordinates_GpsGeometryCenterId",
                        column: x => x.GpsGeometryCenterId,
                        principalTable: "GpsCoordinates",
                        principalColumn: "GpsCoordinateId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Districts_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalInstitutions",
                columns: table => new
                {
                    EducationalInstitutionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EducationalInstitutionName = table.Column<string>(nullable: true),
                    EducationalInstitutionTypeId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalInstitutions", x => x.EducationalInstitutionId);
                    table.ForeignKey(
                        name: "FK_EducationalInstitutions_EducationalInstitutionTypes_EducationalInstitutionTypeId",
                        column: x => x.EducationalInstitutionTypeId,
                        principalTable: "EducationalInstitutionTypes",
                        principalColumn: "EducationalInstitutionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalInstitutions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PopulatedLocalities",
                columns: table => new
                {
                    PopulatedLocalityId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DistrictId = table.Column<int>(nullable: false),
                    PopulatedLocalityName = table.Column<string>(nullable: true),
                    PopulatedLocalityTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopulatedLocalities", x => x.PopulatedLocalityId);
                    table.ForeignKey(
                        name: "FK_PopulatedLocalities_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PopulatedLocalities_PopulatedLocalityTypes_PopulatedLocalityTypeId",
                        column: x => x.PopulatedLocalityTypeId,
                        principalTable: "PopulatedLocalityTypes",
                        principalColumn: "PopulatedLocalityTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEducations",
                columns: table => new
                {
                    UserEducationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    EducationDocumentDate = table.Column<DateTime>(nullable: false),
                    EducationDocumentNumber = table.Column<string>(nullable: true),
                    EducationDocumentNumberReg = table.Column<string>(nullable: true),
                    EducationDocumentSeries = table.Column<string>(nullable: true),
                    EducationalInstitutionId = table.Column<int>(nullable: true),
                    FileDataTypeId = table.Column<int>(nullable: false),
                    QualificationId = table.Column<int>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    RowStatusId = table.Column<int>(nullable: true),
                    UserDocumentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEducations", x => x.UserEducationId);
                    table.ForeignKey(
                        name: "FK_UserEducations_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEducations_EducationalInstitutions_EducationalInstitutionId",
                        column: x => x.EducationalInstitutionId,
                        principalTable: "EducationalInstitutions",
                        principalColumn: "EducationalInstitutionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEducations_FileDataTypes_FileDataTypeId",
                        column: x => x.FileDataTypeId,
                        principalTable: "FileDataTypes",
                        principalColumn: "FileDataTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEducations_Qualifications_QualificationId",
                        column: x => x.QualificationId,
                        principalTable: "Qualifications",
                        principalColumn: "QualificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEducations_RowStatuses_RowStatusId",
                        column: x => x.RowStatusId,
                        principalTable: "RowStatuses",
                        principalColumn: "RowStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEducations_UserDocuments_UserDocumentId",
                        column: x => x.UserDocumentId,
                        principalTable: "UserDocuments",
                        principalColumn: "UserDocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDocuments_RowStatusId",
                table: "UserDocuments",
                column: "RowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PopulatedLocalityId",
                table: "Addresses",
                column: "PopulatedLocalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_GpsGeometryCenterId",
                table: "Districts",
                column: "GpsGeometryCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_RegionId",
                table: "Districts",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_EducationalInstitutionTypeId",
                table: "EducationalInstitutions",
                column: "EducationalInstitutionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalInstitutions_LocationId",
                table: "EducationalInstitutions",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AddressId",
                table: "Locations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_GpsCoordinateId",
                table: "Locations",
                column: "GpsCoordinateId");

            migrationBuilder.CreateIndex(
                name: "IX_PopulatedLocalities_DistrictId",
                table: "PopulatedLocalities",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_PopulatedLocalities_PopulatedLocalityTypeId",
                table: "PopulatedLocalities",
                column: "PopulatedLocalityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_AppUserId",
                table: "UserEducations",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_EducationalInstitutionId",
                table: "UserEducations",
                column: "EducationalInstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_FileDataTypeId",
                table: "UserEducations",
                column: "FileDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_QualificationId",
                table: "UserEducations",
                column: "QualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_RowStatusId",
                table: "UserEducations",
                column: "RowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEducations_UserDocumentId",
                table: "UserEducations",
                column: "UserDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PopulatedLocalities_PopulatedLocalityId",
                table: "Addresses",
                column: "PopulatedLocalityId",
                principalTable: "PopulatedLocalities",
                principalColumn: "PopulatedLocalityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDocuments_RowStatuses_RowStatusId",
                table: "UserDocuments",
                column: "RowStatusId",
                principalTable: "RowStatuses",
                principalColumn: "RowStatusId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PopulatedLocalities_PopulatedLocalityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDocuments_RowStatuses_RowStatusId",
                table: "UserDocuments");

            migrationBuilder.DropTable(
                name: "PopulatedLocalities");

            migrationBuilder.DropTable(
                name: "UserEducations");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "PopulatedLocalityTypes");

            migrationBuilder.DropTable(
                name: "EducationalInstitutions");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "EducationalInstitutionTypes");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "GpsCoordinates");

            migrationBuilder.DropIndex(
                name: "IX_UserDocuments_RowStatusId",
                table: "UserDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PopulatedLocalityId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "UserDocuments");

            migrationBuilder.DropColumn(
                name: "RowStatusId",
                table: "UserDocuments");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PopulatedLocalityId",
                table: "Addresses");
        }
    }
}
