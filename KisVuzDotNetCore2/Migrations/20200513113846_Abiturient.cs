using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class Abiturient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressCurrentId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Settlement",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AbiturientStatuses",
                columns: table => new
                {
                    AbiturientStatusId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientStatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbiturientStatuses", x => x.AbiturientStatusId);
                });

            migrationBuilder.CreateTable(
                name: "ForeignLanguages",
                columns: table => new
                {
                    ForeignLanguageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ForeignLanguageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForeignLanguages", x => x.ForeignLanguageId);
                });

            migrationBuilder.CreateTable(
                name: "PassportDataSet",
                columns: table => new
                {
                    PassportDataId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    DataVidachi = table.Column<DateTime>(nullable: false),
                    KemVidan = table.Column<string>(nullable: false),
                    KodPodrazdeleniya = table.Column<string>(nullable: false),
                    MestoRojdeniya = table.Column<string>(nullable: false),
                    RowStatusId = table.Column<int>(nullable: true),
                    UserDocumentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassportDataSet", x => x.PassportDataId);
                    table.ForeignKey(
                        name: "FK_PassportDataSet_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassportDataSet_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PassportDataSet_RowStatuses_RowStatusId",
                        column: x => x.RowStatusId,
                        principalTable: "RowStatuses",
                        principalColumn: "RowStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PassportDataSet_UserDocuments_UserDocumentId",
                        column: x => x.UserDocumentId,
                        principalTable: "UserDocuments",
                        principalColumn: "UserDocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotaTypes",
                columns: table => new
                {
                    QuotaTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuotaTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotaTypes", x => x.QuotaTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Abiturients",
                columns: table => new
                {
                    AbiturientId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientStatusId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    IsHostelRequired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abiturients", x => x.AbiturientId);
                    table.ForeignKey(
                        name: "FK_Abiturients_AbiturientStatuses_AbiturientStatusId",
                        column: x => x.AbiturientStatusId,
                        principalTable: "AbiturientStatuses",
                        principalColumn: "AbiturientStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Abiturients_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppUserForeignLanguages",
                columns: table => new
                {
                    AppUserForeignLanguageId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    ForeignLanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserForeignLanguages", x => x.AppUserForeignLanguageId);
                    table.ForeignKey(
                        name: "FK_AppUserForeignLanguages_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserForeignLanguages_ForeignLanguages_ForeignLanguageId",
                        column: x => x.ForeignLanguageId,
                        principalTable: "ForeignLanguages",
                        principalColumn: "ForeignLanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationForAdmissions",
                columns: table => new
                {
                    ApplicationForAdmissionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    EduFormId = table.Column<int>(nullable: false),
                    EduProfileId = table.Column<int>(nullable: false),
                    QuotaTypeId = table.Column<int>(nullable: false),
                    RegNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationForAdmissions", x => x.ApplicationForAdmissionId);
                    table.ForeignKey(
                        name: "FK_ApplicationForAdmissions_Abiturients_AbiturientId",
                        column: x => x.AbiturientId,
                        principalTable: "Abiturients",
                        principalColumn: "AbiturientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationForAdmissions_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationForAdmissions_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationForAdmissions_QuotaTypes_QuotaTypeId",
                        column: x => x.QuotaTypeId,
                        principalTable: "QuotaTypes",
                        principalColumn: "QuotaTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressCurrentId",
                table: "AspNetUsers",
                column: "AddressCurrentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Abiturients_AbiturientStatusId",
                table: "Abiturients",
                column: "AbiturientStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Abiturients_AppUserId",
                table: "Abiturients",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationForAdmissions_AbiturientId",
                table: "ApplicationForAdmissions",
                column: "AbiturientId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationForAdmissions_EduFormId",
                table: "ApplicationForAdmissions",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationForAdmissions_EduProfileId",
                table: "ApplicationForAdmissions",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationForAdmissions_QuotaTypeId",
                table: "ApplicationForAdmissions",
                column: "QuotaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserForeignLanguages_AppUserId",
                table: "AppUserForeignLanguages",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserForeignLanguages_ForeignLanguageId",
                table: "AppUserForeignLanguages",
                column: "ForeignLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PassportDataSet_AddressId",
                table: "PassportDataSet",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PassportDataSet_AppUserId",
                table: "PassportDataSet",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PassportDataSet_RowStatusId",
                table: "PassportDataSet",
                column: "RowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PassportDataSet_UserDocumentId",
                table: "PassportDataSet",
                column: "UserDocumentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressCurrentId",
                table: "AspNetUsers",
                column: "AddressCurrentId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_AddressCurrentId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ApplicationForAdmissions");

            migrationBuilder.DropTable(
                name: "AppUserForeignLanguages");

            migrationBuilder.DropTable(
                name: "PassportDataSet");

            migrationBuilder.DropTable(
                name: "Abiturients");

            migrationBuilder.DropTable(
                name: "QuotaTypes");

            migrationBuilder.DropTable(
                name: "ForeignLanguages");

            migrationBuilder.DropTable(
                name: "AbiturientStatuses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressCurrentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AddressCurrentId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Settlement",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
