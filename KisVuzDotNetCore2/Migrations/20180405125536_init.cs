using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AppUserPhoto = table.Column<byte[]>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    Patronymic = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EduForms",
                columns: table => new
                {
                    EduFormId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduForms", x => x.EduFormId);
                });

            migrationBuilder.CreateTable(
                name: "EduKurses",
                columns: table => new
                {
                    EduKursId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduKursName = table.Column<string>(nullable: true),
                    EduKursNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduKurses", x => x.EduKursId);
                });

            migrationBuilder.CreateTable(
                name: "EduLevels",
                columns: table => new
                {
                    EduLevelId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduLevelName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduLevels", x => x.EduLevelId);
                });

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
                name: "FileDataTypeGroups",
                columns: table => new
                {
                    FileDataTypeGroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileDataTypeGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDataTypeGroups", x => x.FileDataTypeGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContentType = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PostName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "StructSubvisionTypes",
                columns: table => new
                {
                    StructSubvisionTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructSubvisionTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructSubvisionTypes", x => x.StructSubvisionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SvedenRucovodstvo",
                columns: table => new
                {
                    RucovodstvoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Fio = table.Column<string>(nullable: true),
                    Post = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SvedenRucovodstvo", x => x.RucovodstvoId);
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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileDataTypes",
                columns: table => new
                {
                    FileDataTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileDataTypeGroupId = table.Column<int>(nullable: false),
                    FileDataTypeName = table.Column<string>(nullable: true),
                    Itemprop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDataTypes", x => x.FileDataTypeId);
                    table.ForeignKey(
                        name: "FK_FileDataTypes_FileDataTypeGroups_FileDataTypeGroupId",
                        column: x => x.FileDataTypeGroupId,
                        principalTable: "FileDataTypeGroups",
                        principalColumn: "FileDataTypeGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduAccreds",
                columns: table => new
                {
                    EduAccredId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduAccredDate = table.Column<DateTime>(nullable: false),
                    EduAccredDateExpiration = table.Column<DateTime>(nullable: false),
                    EduAccredFileId = table.Column<int>(nullable: true),
                    EduAccredNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduAccreds", x => x.EduAccredId);
                    table.ForeignKey(
                        name: "FK_EduAccreds_Files_EduAccredFileId",
                        column: x => x.EduAccredFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StructInstitutes",
                columns: table => new
                {
                    StructInstituteId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    DateOfCreation = table.Column<DateTime>(nullable: false),
                    ExistenceOfFilials = table.Column<bool>(nullable: false),
                    StructInstituteName = table.Column<string>(nullable: true),
                    UniversityId = table.Column<int>(nullable: false),
                    WorkingRegime = table.Column<string>(nullable: true),
                    WorkingSchedule = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructInstitutes", x => x.StructInstituteId);
                    table.ForeignKey(
                        name: "FK_StructInstitutes_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StructInstitutes_StructUniversities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "StructUniversities",
                        principalColumn: "StructUniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileToFileTypes",
                columns: table => new
                {
                    FileToFileTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileDataTypeId = table.Column<int>(nullable: false),
                    FileModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileToFileTypes", x => x.FileToFileTypeId);
                    table.ForeignKey(
                        name: "FK_FileToFileTypes_FileDataTypes_FileDataTypeId",
                        column: x => x.FileDataTypeId,
                        principalTable: "FileDataTypes",
                        principalColumn: "FileDataTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileToFileTypes_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduUgses",
                columns: table => new
                {
                    EduUgsId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduAccredId = table.Column<int>(nullable: true),
                    EduLevelId = table.Column<int>(nullable: false),
                    EduUgsCode = table.Column<string>(nullable: true),
                    EduUgsName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduUgses", x => x.EduUgsId);
                    table.ForeignKey(
                        name: "FK_EduUgses_EduAccreds_EduAccredId",
                        column: x => x.EduAccredId,
                        principalTable: "EduAccreds",
                        principalColumn: "EduAccredId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EduUgses_EduLevels_EduLevelId",
                        column: x => x.EduLevelId,
                        principalTable: "EduLevels",
                        principalColumn: "EduLevelId",
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

            migrationBuilder.CreateTable(
                name: "EduNapravls",
                columns: table => new
                {
                    EduNapravlId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlCode = table.Column<string>(nullable: true),
                    EduNapravlName = table.Column<string>(nullable: true),
                    EduNapravlStandartDocLink = table.Column<string>(nullable: true),
                    EduUgsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduNapravls", x => x.EduNapravlId);
                    table.ForeignKey(
                        name: "FK_EduNapravls_EduUgses_EduUgsId",
                        column: x => x.EduUgsId,
                        principalTable: "EduUgses",
                        principalColumn: "EduUgsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructSubvisions",
                columns: table => new
                {
                    StructSubvisionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructStandingOrderId = table.Column<int>(nullable: true),
                    StructSubvisionAdressId = table.Column<int>(nullable: false),
                    StructSubvisionEmailId = table.Column<int>(nullable: true),
                    StructSubvisionFioChief = table.Column<string>(nullable: true),
                    StructSubvisionName = table.Column<string>(nullable: true),
                    StructSubvisionPostChiefId = table.Column<int>(nullable: false),
                    StructSubvisionSite = table.Column<string>(nullable: true),
                    StructSubvisionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructSubvisions", x => x.StructSubvisionId);
                    table.ForeignKey(
                        name: "FK_StructSubvisions_Files_StructStandingOrderId",
                        column: x => x.StructStandingOrderId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StructSubvisions_Addresses_StructSubvisionAdressId",
                        column: x => x.StructSubvisionAdressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StructSubvisions_Emails_StructSubvisionEmailId",
                        column: x => x.StructSubvisionEmailId,
                        principalTable: "Emails",
                        principalColumn: "EmailId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StructSubvisions_Posts_StructSubvisionPostChiefId",
                        column: x => x.StructSubvisionPostChiefId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StructSubvisions_StructSubvisionTypes_StructSubvisionTypeId",
                        column: x => x.StructSubvisionTypeId,
                        principalTable: "StructSubvisionTypes",
                        principalColumn: "StructSubvisionTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduProfiles",
                columns: table => new
                {
                    EduProfileId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlId = table.Column<int>(nullable: false),
                    EduProfileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduProfiles", x => x.EduProfileId);
                    table.ForeignKey(
                        name: "FK_EduProfiles_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vacants",
                columns: table => new
                {
                    VacantId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduKursId = table.Column<int>(nullable: false),
                    EduNapravlId = table.Column<int>(nullable: false),
                    NumberBFVacant = table.Column<int>(nullable: false),
                    NumberBMVacant = table.Column<int>(nullable: false),
                    NumberBRVacant = table.Column<int>(nullable: false),
                    NumberPVacant = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacants", x => x.VacantId);
                    table.ForeignKey(
                        name: "FK_Vacants_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacants_EduKurses_EduKursId",
                        column: x => x.EduKursId,
                        principalTable: "EduKurses",
                        principalColumn: "EduKursId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vacants_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructFacultets",
                columns: table => new
                {
                    StructFacultetId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructInstituteId = table.Column<int>(nullable: false),
                    StructSubvisionId = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_StructFacultets_StructSubvisions_StructSubvisionId",
                        column: x => x.StructSubvisionId,
                        principalTable: "StructSubvisions",
                        principalColumn: "StructSubvisionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduChislens",
                columns: table => new
                {
                    EduChislenId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduProfileId = table.Column<int>(nullable: false),
                    NumberBFpriem = table.Column<int>(nullable: false),
                    NumberBMpriem = table.Column<int>(nullable: false),
                    NumberBRpriem = table.Column<int>(nullable: false),
                    NumberPpriem = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduChislens", x => x.EduChislenId);
                    table.ForeignKey(
                        name: "FK_EduChislens_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduChislens_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StructKafs",
                columns: table => new
                {
                    StructKafId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StructFacultetId = table.Column<int>(nullable: false),
                    StructSubvisionId = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_StructKafs_StructSubvisions_StructSubvisionId",
                        column: x => x.StructSubvisionId,
                        principalTable: "StructSubvisions",
                        principalColumn: "StructSubvisionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduPlans",
                columns: table => new
                {
                    EduPlanId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduProfileId = table.Column<int>(nullable: false),
                    EduYearBeginningTrainingId = table.Column<int>(nullable: true),
                    EduYearId = table.Column<int>(nullable: true),
                    StructKafId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduPlans", x => x.EduPlanId);
                    table.ForeignKey(
                        name: "FK_EduPlans_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_EduPlans_StructKafs_StructKafId",
                        column: x => x.StructKafId,
                        principalTable: "StructKafs",
                        principalColumn: "StructKafId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EduAccreds_EduAccredFileId",
                table: "EduAccreds",
                column: "EduAccredFileId");

            migrationBuilder.CreateIndex(
                name: "IX_EduChislens_EduFormId",
                table: "EduChislens",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_EduChislens_EduProfileId",
                table: "EduChislens",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EduNapravls_EduUgsId",
                table: "EduNapravls",
                column: "EduUgsId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduFormId",
                table: "EduPlans",
                column: "EduFormId");

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
                name: "IX_EduPlans_StructKafId",
                table: "EduPlans",
                column: "StructKafId");

            migrationBuilder.CreateIndex(
                name: "IX_EduProfiles_EduNapravlId",
                table: "EduProfiles",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_EduUgses_EduAccredId",
                table: "EduUgses",
                column: "EduAccredId");

            migrationBuilder.CreateIndex(
                name: "IX_EduUgses_EduLevelId",
                table: "EduUgses",
                column: "EduLevelId");

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
                name: "IX_FileDataTypes_FileDataTypeGroupId",
                table: "FileDataTypes",
                column: "FileDataTypeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FileToFileTypes_FileDataTypeId",
                table: "FileToFileTypes",
                column: "FileDataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FileToFileTypes_FileModelId",
                table: "FileToFileTypes",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_StructFacultets_StructInstituteId",
                table: "StructFacultets",
                column: "StructInstituteId");

            migrationBuilder.CreateIndex(
                name: "IX_StructFacultets_StructSubvisionId",
                table: "StructFacultets",
                column: "StructSubvisionId");

            migrationBuilder.CreateIndex(
                name: "IX_StructInstitutes_AddressId",
                table: "StructInstitutes",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_StructInstitutes_UniversityId",
                table: "StructInstitutes",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_StructKafs_StructFacultetId",
                table: "StructKafs",
                column: "StructFacultetId");

            migrationBuilder.CreateIndex(
                name: "IX_StructKafs_StructSubvisionId",
                table: "StructKafs",
                column: "StructSubvisionId");

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructStandingOrderId",
                table: "StructSubvisions",
                column: "StructStandingOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructSubvisionAdressId",
                table: "StructSubvisions",
                column: "StructSubvisionAdressId");

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructSubvisionEmailId",
                table: "StructSubvisions",
                column: "StructSubvisionEmailId");

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructSubvisionPostChiefId",
                table: "StructSubvisions",
                column: "StructSubvisionPostChiefId");

            migrationBuilder.CreateIndex(
                name: "IX_StructSubvisions_StructSubvisionTypeId",
                table: "StructSubvisions",
                column: "StructSubvisionTypeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Vacants_EduFormId",
                table: "Vacants",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacants_EduKursId",
                table: "Vacants",
                column: "EduKursId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacants_EduNapravlId",
                table: "Vacants",
                column: "EduNapravlId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EduChislens");

            migrationBuilder.DropTable(
                name: "EduPlans");

            migrationBuilder.DropTable(
                name: "Faxes");

            migrationBuilder.DropTable(
                name: "FileToFileTypes");

            migrationBuilder.DropTable(
                name: "SvedenRucovodstvo");

            migrationBuilder.DropTable(
                name: "Telephones");

            migrationBuilder.DropTable(
                name: "Vacants");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EduProfiles");

            migrationBuilder.DropTable(
                name: "EduYearBeginningTrainings");

            migrationBuilder.DropTable(
                name: "EduYears");

            migrationBuilder.DropTable(
                name: "StructKafs");

            migrationBuilder.DropTable(
                name: "FileDataTypes");

            migrationBuilder.DropTable(
                name: "EduForms");

            migrationBuilder.DropTable(
                name: "EduKurses");

            migrationBuilder.DropTable(
                name: "EduNapravls");

            migrationBuilder.DropTable(
                name: "StructFacultets");

            migrationBuilder.DropTable(
                name: "FileDataTypeGroups");

            migrationBuilder.DropTable(
                name: "EduUgses");

            migrationBuilder.DropTable(
                name: "StructSubvisions");

            migrationBuilder.DropTable(
                name: "EduAccreds");

            migrationBuilder.DropTable(
                name: "EduLevels");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "StructSubvisionTypes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "StructInstitutes");

            migrationBuilder.DropTable(
                name: "StructUniversities");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
