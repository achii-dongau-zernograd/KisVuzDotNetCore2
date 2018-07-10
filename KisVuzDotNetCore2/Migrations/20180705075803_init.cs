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
                name: "AcademicDegreeGroups",
                columns: table => new
                {
                    AcademicDegreeGroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AcademicDegreeGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicDegreeGroups", x => x.AcademicDegreeGroupId);
                });

            migrationBuilder.CreateTable(
                name: "AcademicStats",
                columns: table => new
                {
                    AcademicStatId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AcademicStatName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicStats", x => x.AcademicStatId);
                });

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
                name: "BlokDisciplChastName",
                columns: table => new
                {
                    BlokDisciplChastNameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlokDisciplChastNameName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlokDisciplChastName", x => x.BlokDisciplChastNameId);
                });

            migrationBuilder.CreateTable(
                name: "BlokDisciplName",
                columns: table => new
                {
                    BlokDisciplNameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlokDisciplNameName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlokDisciplName", x => x.BlokDisciplNameId);
                });

            migrationBuilder.CreateTable(
                name: "DisciplineNames",
                columns: table => new
                {
                    DisciplineNameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisciplineNameName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineNames", x => x.DisciplineNameId);
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
                name: "EduLevelGroups",
                columns: table => new
                {
                    EduLevelGroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduLevelGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduLevelGroups", x => x.EduLevelGroupId);
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
                name: "EduOPEduYearNames",
                columns: table => new
                {
                    EduOPEduYearNameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduOPEduYearNameName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduOPEduYearNames", x => x.EduOPEduYearNameId);
                });

            migrationBuilder.CreateTable(
                name: "EduProgramPodg",
                columns: table => new
                {
                    EduProgramPodgId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduProgramPodgName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduProgramPodg", x => x.EduProgramPodgId);
                });

            migrationBuilder.CreateTable(
                name: "EduQualification",
                columns: table => new
                {
                    EduQualificationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduQualificationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduQualification", x => x.EduQualificationId);
                });

            migrationBuilder.CreateTable(
                name: "EduSrok",
                columns: table => new
                {
                    EduSrokId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduSrokName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduSrok", x => x.EduSrokId);
                });

            migrationBuilder.CreateTable(
                name: "EduVidDeyat",
                columns: table => new
                {
                    EduVidDeyatId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduVidDeyatName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduVidDeyat", x => x.EduVidDeyatId);
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
                name: "ElectronCatalog",
                columns: table => new
                {
                    ElectronCatalogId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DescriptionEc = table.Column<string>(nullable: true),
                    NameEc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronCatalog", x => x.ElectronCatalogId);
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
                name: "FilInfo",
                columns: table => new
                {
                    FilInfoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressFil = table.Column<string>(nullable: true),
                    NameFil = table.Column<string>(nullable: true),
                    WebsiteFil = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilInfo", x => x.FilInfoId);
                });

            migrationBuilder.CreateTable(
                name: "GraduateYear",
                columns: table => new
                {
                    GraduateYearId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GraduateYearName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraduateYear", x => x.GraduateYearId);
                });

            migrationBuilder.CreateTable(
                name: "HostelInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Itemprop = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    NameIndicator = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostelInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PomeshenieType",
                columns: table => new
                {
                    PomeshenieTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PomeshenieTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PomeshenieType", x => x.PomeshenieTypeId);
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
                name: "PurposeLibr",
                columns: table => new
                {
                    PurposeLibrId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Adress = table.Column<string>(nullable: true),
                    NumberPlaces = table.Column<string>(nullable: true),
                    PrisposoblOvz = table.Column<string>(nullable: true),
                    Square = table.Column<string>(nullable: true),
                    VidPom = table.Column<string>(nullable: true),
                    itemprop = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurposeLibr", x => x.PurposeLibrId);
                });

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

            migrationBuilder.CreateTable(
                name: "RucovodstvoFil",
                columns: table => new
                {
                    RucovodstvoFilId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Fio = table.Column<string>(nullable: true),
                    NameFil = table.Column<string>(nullable: true),
                    Post = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RucovodstvoFil", x => x.RucovodstvoFilId);
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
                name: "UchredLaw",
                columns: table => new
                {
                    UchredLawId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressUchred = table.Column<string>(nullable: true),
                    FullnameUchred = table.Column<string>(nullable: true),
                    NameUchred = table.Column<string>(nullable: true),
                    TelUchred = table.Column<string>(nullable: true),
                    WebsiteUchred = table.Column<string>(nullable: true),
                    mailUchred = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UchredLaw", x => x.UchredLawId);
                });

            migrationBuilder.CreateTable(
                name: "Volume",
                columns: table => new
                {
                    VolumeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FinBFVolume = table.Column<double>(nullable: false),
                    FinBMVolume = table.Column<double>(nullable: false),
                    FinBRVolume = table.Column<double>(nullable: false),
                    FinPVolume = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volume", x => x.VolumeId);
                });

            migrationBuilder.CreateTable(
                name: "AcademicDegrees",
                columns: table => new
                {
                    AcademicDegreeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AcademicDegreeGroupId = table.Column<int>(nullable: false),
                    AcademicDegreeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicDegrees", x => x.AcademicDegreeId);
                    table.ForeignKey(
                        name: "FK_AcademicDegrees_AcademicDegreeGroups_AcademicDegreeGroupId",
                        column: x => x.AcademicDegreeGroupId,
                        principalTable: "AcademicDegreeGroups",
                        principalColumn: "AcademicDegreeGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Korpus",
                columns: table => new
                {
                    KorpusId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressId = table.Column<int>(nullable: false),
                    KorpusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpus", x => x.KorpusId);
                    table.ForeignKey(
                        name: "FK_Korpus_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ElectronBiblSystem",
                columns: table => new
                {
                    ElectronBiblSystemId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CopyDogovorId = table.Column<int>(nullable: true),
                    DateEnd = table.Column<DateTime>(nullable: true),
                    DateStart = table.Column<DateTime>(nullable: false),
                    LinkEbs = table.Column<string>(nullable: true),
                    NameEbs = table.Column<string>(nullable: true),
                    NumberDogovor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronBiblSystem", x => x.ElectronBiblSystemId);
                    table.ForeignKey(
                        name: "FK_ElectronBiblSystem_Files_CopyDogovorId",
                        column: x => x.CopyDogovorId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElectronObrazovatInformRes",
                columns: table => new
                {
                    ElectronObrazovatInformResId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DescriptionRes = table.Column<string>(nullable: true),
                    IsSobstv = table.Column<bool>(nullable: false),
                    LinkRes = table.Column<string>(nullable: true),
                    NameRes = table.Column<string>(nullable: true),
                    ResId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectronObrazovatInformRes", x => x.ElectronObrazovatInformResId);
                    table.ForeignKey(
                        name: "FK_ElectronObrazovatInformRes_Files_ResId",
                        column: x => x.ResId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AcademicDegreeId = table.Column<int>(nullable: true),
                    AcademicStatId = table.Column<int>(nullable: true),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AppUserPhoto = table.Column<byte[]>(nullable: true),
                    Birthdate = table.Column<DateTime>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DateStartWorking = table.Column<DateTime>(nullable: true),
                    DateStartWorkingSpec = table.Column<DateTime>(nullable: true),
                    EduLevelGroupId = table.Column<int>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AcademicDegrees_AcademicDegreeId",
                        column: x => x.AcademicDegreeId,
                        principalTable: "AcademicDegrees",
                        principalColumn: "AcademicDegreeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AcademicStats_AcademicStatId",
                        column: x => x.AcademicStatId,
                        principalTable: "AcademicStats",
                        principalColumn: "AcademicStatId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_EduLevelGroups_EduLevelGroupId",
                        column: x => x.EduLevelGroupId,
                        principalTable: "EduLevelGroups",
                        principalColumn: "EduLevelGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pomeshenie",
                columns: table => new
                {
                    PomeshenieId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KorpusId = table.Column<int>(nullable: false),
                    PomeshenieName = table.Column<string>(nullable: true),
                    PomeshenieOvz = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pomeshenie", x => x.PomeshenieId);
                    table.ForeignKey(
                        name: "FK_Pomeshenie_Korpus_KorpusId",
                        column: x => x.KorpusId,
                        principalTable: "Korpus",
                        principalColumn: "KorpusId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ProfessionalRetrainings",
                columns: table => new
                {
                    ProfessionalRetrainingId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    ProfessionalRetrainingCity = table.Column<string>(nullable: true),
                    ProfessionalRetrainingDateFinish = table.Column<DateTime>(nullable: false),
                    ProfessionalRetrainingDateIssue = table.Column<DateTime>(nullable: false),
                    ProfessionalRetrainingDateStart = table.Column<DateTime>(nullable: false),
                    ProfessionalRetrainingDiplomNumber = table.Column<string>(nullable: true),
                    ProfessionalRetrainingDiplomRegNumber = table.Column<string>(nullable: true),
                    ProfessionalRetrainingFileId = table.Column<int>(nullable: false),
                    ProfessionalRetrainingHours = table.Column<int>(nullable: false),
                    ProfessionalRetrainingInstitition = table.Column<string>(nullable: true),
                    ProfessionalRetrainingProgramName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalRetrainings", x => x.ProfessionalRetrainingId);
                    table.ForeignKey(
                        name: "FK_ProfessionalRetrainings_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfessionalRetrainings_Files_ProfessionalRetrainingFileId",
                        column: x => x.ProfessionalRetrainingFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualifications",
                columns: table => new
                {
                    QualificationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    NapravlName = table.Column<string>(nullable: true),
                    QualificationName = table.Column<string>(nullable: true),
                    RowStatusId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifications", x => x.QualificationId);
                    table.ForeignKey(
                        name: "FK_Qualifications_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qualifications_RowStatuses_RowStatusId",
                        column: x => x.RowStatusId,
                        principalTable: "RowStatuses",
                        principalColumn: "RowStatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefresherCourses",
                columns: table => new
                {
                    RefresherCourseId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    RefresherCourseCity = table.Column<string>(nullable: true),
                    RefresherCourseDateFinish = table.Column<DateTime>(nullable: false),
                    RefresherCourseDateIssue = table.Column<DateTime>(nullable: false),
                    RefresherCourseDateStart = table.Column<DateTime>(nullable: false),
                    RefresherCourseFileId = table.Column<int>(nullable: false),
                    RefresherCourseHours = table.Column<int>(nullable: false),
                    RefresherCourseInstitition = table.Column<string>(nullable: true),
                    RefresherCourseName = table.Column<string>(nullable: true),
                    RefresherCourseRegNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefresherCourses", x => x.RefresherCourseId);
                    table.ForeignKey(
                        name: "FK_RefresherCourses_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefresherCourses_Files_RefresherCourseFileId",
                        column: x => x.RefresherCourseFileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    TeacherFio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Teachers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Oborudovanie",
                columns: table => new
                {
                    OborudovanieId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OborudovanieCount = table.Column<int>(nullable: false),
                    OborudovanieName = table.Column<string>(nullable: true),
                    PomeshenieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oborudovanie", x => x.OborudovanieId);
                    table.ForeignKey(
                        name: "FK_Oborudovanie_Pomeshenie_PomeshenieId",
                        column: x => x.PomeshenieId,
                        principalTable: "Pomeshenie",
                        principalColumn: "PomeshenieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PomeshenieTypepomesheniya",
                columns: table => new
                {
                    PomeshenieTypepomesheniyaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PomeshenieId = table.Column<int>(nullable: false),
                    PomeshenieTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PomeshenieTypepomesheniya", x => x.PomeshenieTypepomesheniyaId);
                    table.ForeignKey(
                        name: "FK_PomeshenieTypepomesheniya_Pomeshenie_PomeshenieId",
                        column: x => x.PomeshenieId,
                        principalTable: "Pomeshenie",
                        principalColumn: "PomeshenieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PomeshenieTypepomesheniya_PomeshenieType_PomeshenieTypeId",
                        column: x => x.PomeshenieTypeId,
                        principalTable: "PomeshenieType",
                        principalColumn: "PomeshenieTypeId",
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
                    EduQualificationId = table.Column<int>(nullable: false),
                    EduUgsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduNapravls", x => x.EduNapravlId);
                    table.ForeignKey(
                        name: "FK_EduNapravls_EduQualification_EduQualificationId",
                        column: x => x.EduQualificationId,
                        principalTable: "EduQualification",
                        principalColumn: "EduQualificationId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "BlankNums",
                columns: table => new
                {
                    BlankNumId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlId = table.Column<int>(nullable: false),
                    Och = table.Column<int>(nullable: false),
                    OchZaoch = table.Column<int>(nullable: false),
                    Zaoch = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlankNums", x => x.BlankNumId);
                    table.ForeignKey(
                        name: "FK_BlankNums_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduNir",
                columns: table => new
                {
                    EduNirId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlId = table.Column<int>(nullable: false),
                    NirNapravls = table.Column<string>(nullable: true),
                    NumArticles = table.Column<string>(nullable: true),
                    NumArticlesZ = table.Column<string>(nullable: true),
                    NumFinance = table.Column<string>(nullable: true),
                    NumMonograf = table.Column<string>(nullable: true),
                    NumNpr = table.Column<string>(nullable: true),
                    NumPatents = table.Column<string>(nullable: true),
                    NumPatentsZ = table.Column<string>(nullable: true),
                    NumStud = table.Column<string>(nullable: true),
                    NumSvidetelstv = table.Column<string>(nullable: true),
                    NumSvidetelstvZ = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduNir", x => x.EduNirId);
                    table.ForeignKey(
                        name: "FK_EduNir_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "eduPerevod",
                columns: table => new
                {
                    EduPerevodId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduNapravlId = table.Column<int>(nullable: false),
                    NumberExpPerevod = table.Column<string>(nullable: true),
                    NumberOutPerevod = table.Column<string>(nullable: true),
                    NumberResPerevod = table.Column<string>(nullable: true),
                    NumberToPerevod = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_eduPerevod", x => x.EduPerevodId);
                    table.ForeignKey(
                        name: "FK_eduPerevod_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_eduPerevod_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduPriem",
                columns: table => new
                {
                    EduPriemId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduNapravlId = table.Column<int>(nullable: false),
                    FinBF = table.Column<string>(nullable: true),
                    FinBM = table.Column<string>(nullable: true),
                    FinBR = table.Column<string>(nullable: true),
                    FinPV = table.Column<string>(nullable: true),
                    SredSum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduPriem", x => x.EduPriemId);
                    table.ForeignKey(
                        name: "FK_EduPriem_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPriem_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
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
                name: "PriemExam",
                columns: table => new
                {
                    PriemExamId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlId = table.Column<int>(nullable: false),
                    FormProv = table.Column<string>(nullable: true),
                    MinKol = table.Column<string>(nullable: true),
                    VstupIsp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriemExam", x => x.PriemExamId);
                    table.ForeignKey(
                        name: "FK_PriemExam_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "priemKolMest",
                columns: table => new
                {
                    priemKolMestId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlId = table.Column<int>(nullable: false),
                    PriemKolMestCommon_och = table.Column<int>(nullable: false),
                    PriemKolMestCommon_och_zaoch = table.Column<int>(nullable: false),
                    PriemKolMestCommon_zaoch = table.Column<int>(nullable: false),
                    PriemKolMestPaid_och = table.Column<int>(nullable: false),
                    PriemKolMestPaid_och_zaoch = table.Column<int>(nullable: false),
                    PriemKolMestPaid_zaoch = table.Column<int>(nullable: false),
                    priemKolMestQuota_och = table.Column<int>(nullable: false),
                    priemKolMestQuota_och_zaoch = table.Column<int>(nullable: false),
                    priemKolMestQuota_zaoch = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priemKolMest", x => x.priemKolMestId);
                    table.ForeignKey(
                        name: "FK_priemKolMest_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "priemKolTarget",
                columns: table => new
                {
                    priemKolTargetId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlId = table.Column<int>(nullable: false),
                    Mesta_v_pridelah_celevoi_och = table.Column<int>(nullable: false),
                    Mesta_v_pridelah_celevoi_och_zaoch = table.Column<int>(nullable: false),
                    Mesta_v_pridelah_celevoi_zaoch = table.Column<int>(nullable: false),
                    Mesta_v_pridelah_osoboi_och = table.Column<int>(nullable: false),
                    Mesta_v_pridelah_osoboi_och_zaoch = table.Column<int>(nullable: false),
                    Mesta_v_pridelah_osoboi_zaoch = table.Column<int>(nullable: false),
                    Mesta_v_ramkah_och_zaoch = table.Column<int>(nullable: false),
                    Mesta_v_ramkah_zaoch = table.Column<int>(nullable: false),
                    Mesta_v_ramkahtQuota_och = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priemKolTarget", x => x.priemKolTargetId);
                    table.ForeignKey(
                        name: "FK_priemKolTarget_EduNapravls_EduNapravlId",
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
                name: "EduGraduate",
                columns: table => new
                {
                    EduGraduateId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduProfileId = table.Column<int>(nullable: false),
                    GraduateNumber = table.Column<int>(nullable: false),
                    GraduateSredBall = table.Column<string>(nullable: true),
                    GraduateYearId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduGraduate", x => x.EduGraduateId);
                    table.ForeignKey(
                        name: "FK_EduGraduate_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduGraduate_GraduateYear_GraduateYearId",
                        column: x => x.GraduateYearId,
                        principalTable: "GraduateYear",
                        principalColumn: "GraduateYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduOPYears",
                columns: table => new
                {
                    EduOPYearId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduOPEduYearNameId = table.Column<int>(nullable: false),
                    EduProfileId = table.Column<int>(nullable: false),
                    EduYearBeginningTrainingId = table.Column<int>(nullable: false),
                    IsOVZ = table.Column<bool>(nullable: false),
                    VOchn = table.Column<string>(nullable: true),
                    VOchnZaochn = table.Column<string>(nullable: true),
                    VZaochn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduOPYears", x => x.EduOPYearId);
                    table.ForeignKey(
                        name: "FK_EduOPYears_EduOPEduYearNames_EduOPEduYearNameId",
                        column: x => x.EduOPEduYearNameId,
                        principalTable: "EduOPEduYearNames",
                        principalColumn: "EduOPEduYearNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduOPYears_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduOPYears_EduYearBeginningTrainings_EduYearBeginningTrainingId",
                        column: x => x.EduYearBeginningTrainingId,
                        principalTable: "EduYearBeginningTrainings",
                        principalColumn: "EduYearBeginningTrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduPr",
                columns: table => new
                {
                    EduPrId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    EduPrPreddiplomn = table.Column<string>(nullable: true),
                    EduPrProizv = table.Column<string>(nullable: true),
                    EduPrUchebn = table.Column<string>(nullable: true),
                    EduProfileId = table.Column<int>(nullable: false),
                    EduYearBeginningTrainingId = table.Column<int>(nullable: false),
                    IsOVZ = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduPr", x => x.EduPrId);
                    table.ForeignKey(
                        name: "FK_EduPr_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPr_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPr_EduYearBeginningTrainings_EduYearBeginningTrainingId",
                        column: x => x.EduYearBeginningTrainingId,
                        principalTable: "EduYearBeginningTrainings",
                        principalColumn: "EduYearBeginningTrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GraduateTrudoustroustvo",
                columns: table => new
                {
                    GraduateTrudoustroustvoId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduProfileId = table.Column<int>(nullable: false),
                    GraduateNumberTrudoustroustvo = table.Column<int>(nullable: false),
                    GraduateYearId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GraduateTrudoustroustvo", x => x.GraduateTrudoustroustvoId);
                    table.ForeignKey(
                        name: "FK_GraduateTrudoustroustvo_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GraduateTrudoustroustvo_GraduateYear_GraduateYearId",
                        column: x => x.GraduateYearId,
                        principalTable: "GraduateYear",
                        principalColumn: "GraduateYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEduProfileDisciplineNames",
                columns: table => new
                {
                    TeacherEduProfileDisciplineNameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisciplineNameId = table.Column<int>(nullable: false),
                    EduProfileId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEduProfileDisciplineNames", x => x.TeacherEduProfileDisciplineNameId);
                    table.ForeignKey(
                        name: "FK_TeacherEduProfileDisciplineNames_DisciplineNames_DisciplineNameId",
                        column: x => x.DisciplineNameId,
                        principalTable: "DisciplineNames",
                        principalColumn: "DisciplineNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherEduProfileDisciplineNames_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherEduProfileDisciplineNames_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
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
                    EduPlanPdfId = table.Column<int>(nullable: true),
                    EduProfileId = table.Column<int>(nullable: false),
                    EduProgramPodgId = table.Column<int>(nullable: false),
                    EduSrokId = table.Column<int>(nullable: false),
                    ProtokolDate = table.Column<DateTime>(nullable: false),
                    ProtokolNumber = table.Column<string>(nullable: true),
                    StructKafId = table.Column<int>(nullable: false),
                    UtverjdDate = table.Column<DateTime>(nullable: false)
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
                        name: "FK_EduPlans_Files_EduPlanPdfId",
                        column: x => x.EduPlanPdfId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EduPlans_EduProfiles_EduProfileId",
                        column: x => x.EduProfileId,
                        principalTable: "EduProfiles",
                        principalColumn: "EduProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPlans_EduProgramPodg_EduProgramPodgId",
                        column: x => x.EduProgramPodgId,
                        principalTable: "EduProgramPodg",
                        principalColumn: "EduProgramPodgId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPlans_EduSrok_EduSrokId",
                        column: x => x.EduSrokId,
                        principalTable: "EduSrok",
                        principalColumn: "EduSrokId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPlans_StructKafs_StructKafId",
                        column: x => x.StructKafId,
                        principalTable: "StructKafs",
                        principalColumn: "StructKafId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherStructKafPostStavka",
                columns: table => new
                {
                    TeacherStructKafPostStavkaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PostId = table.Column<int>(nullable: false),
                    Stavka = table.Column<double>(nullable: false),
                    StavkaDate = table.Column<DateTime>(nullable: false),
                    StructKafId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherStructKafPostStavka", x => x.TeacherStructKafPostStavkaId);
                    table.ForeignKey(
                        name: "FK_TeacherStructKafPostStavka_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherStructKafPostStavka_StructKafs_StructKafId",
                        column: x => x.StructKafId,
                        principalTable: "StructKafs",
                        principalColumn: "StructKafId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherStructKafPostStavka_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlokDiscipl",
                columns: table => new
                {
                    BlokDisciplId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlokDisciplNameId = table.Column<int>(nullable: false),
                    EduPlanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlokDiscipl", x => x.BlokDisciplId);
                    table.ForeignKey(
                        name: "FK_BlokDiscipl_BlokDisciplName_BlokDisciplNameId",
                        column: x => x.BlokDisciplNameId,
                        principalTable: "BlokDisciplName",
                        principalColumn: "BlokDisciplNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlokDiscipl_EduPlans_EduPlanId",
                        column: x => x.EduPlanId,
                        principalTable: "EduPlans",
                        principalColumn: "EduPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduPlanEduVidDeyats",
                columns: table => new
                {
                    EduPlanEduVidDeyatId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduPlanId = table.Column<int>(nullable: false),
                    EduVidDeyatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduPlanEduVidDeyats", x => x.EduPlanEduVidDeyatId);
                    table.ForeignKey(
                        name: "FK_EduPlanEduVidDeyats_EduPlans_EduPlanId",
                        column: x => x.EduPlanId,
                        principalTable: "EduPlans",
                        principalColumn: "EduPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPlanEduVidDeyats_EduVidDeyat_EduVidDeyatId",
                        column: x => x.EduVidDeyatId,
                        principalTable: "EduVidDeyat",
                        principalColumn: "EduVidDeyatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduPlanEduYearBeginningTraining",
                columns: table => new
                {
                    EduPlanEduYearBeginningTrainingId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduPlanId = table.Column<int>(nullable: false),
                    EduYearBeginningTrainingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduPlanEduYearBeginningTraining", x => x.EduPlanEduYearBeginningTrainingId);
                    table.ForeignKey(
                        name: "FK_EduPlanEduYearBeginningTraining_EduPlans_EduPlanId",
                        column: x => x.EduPlanId,
                        principalTable: "EduPlans",
                        principalColumn: "EduPlanId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EduPlanEduYearBeginningTraining_EduYearBeginningTrainings_EduYearBeginningTrainingId",
                        column: x => x.EduYearBeginningTrainingId,
                        principalTable: "EduYearBeginningTrainings",
                        principalColumn: "EduYearBeginningTrainingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EduYears",
                columns: table => new
                {
                    EduYearId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduPlanId = table.Column<int>(nullable: true),
                    EduYearName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EduYears", x => x.EduYearId);
                    table.ForeignKey(
                        name: "FK_EduYears_EduPlans_EduPlanId",
                        column: x => x.EduPlanId,
                        principalTable: "EduPlans",
                        principalColumn: "EduPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlokDisciplChast",
                columns: table => new
                {
                    BlokDisciplChastId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlokDisciplChastNameId = table.Column<int>(nullable: false),
                    BlokDisciplId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlokDisciplChast", x => x.BlokDisciplChastId);
                    table.ForeignKey(
                        name: "FK_BlokDisciplChast_BlokDisciplChastName_BlokDisciplChastNameId",
                        column: x => x.BlokDisciplChastNameId,
                        principalTable: "BlokDisciplChastName",
                        principalColumn: "BlokDisciplChastNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlokDisciplChast_BlokDiscipl_BlokDisciplId",
                        column: x => x.BlokDisciplId,
                        principalTable: "BlokDiscipl",
                        principalColumn: "BlokDisciplId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Discipline",
                columns: table => new
                {
                    DisciplineId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BlokDisciplChastId = table.Column<int>(nullable: true),
                    DisciplineCode = table.Column<string>(nullable: true),
                    DisciplineName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discipline", x => x.DisciplineId);
                    table.ForeignKey(
                        name: "FK_Discipline_BlokDisciplChast_BlokDisciplChastId",
                        column: x => x.BlokDisciplChastId,
                        principalTable: "BlokDisciplChast",
                        principalColumn: "BlokDisciplChastId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kurs",
                columns: table => new
                {
                    KursId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisciplineId = table.Column<int>(nullable: true),
                    EduKursId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kurs", x => x.KursId);
                    table.ForeignKey(
                        name: "FK_Kurs_Discipline_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Discipline",
                        principalColumn: "DisciplineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kurs_EduKurses_EduKursId",
                        column: x => x.EduKursId,
                        principalTable: "EduKurses",
                        principalColumn: "EduKursId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Semestr",
                columns: table => new
                {
                    SemestrId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    KursId = table.Column<int>(nullable: false),
                    SemestrName = table.Column<string>(nullable: true),
                    SemestrNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semestr", x => x.SemestrId);
                    table.ForeignKey(
                        name: "FK_Semestr_Kurs_KursId",
                        column: x => x.KursId,
                        principalTable: "Kurs",
                        principalColumn: "KursId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormKontrol",
                columns: table => new
                {
                    FormKontrolId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FormKontrolName = table.Column<string>(nullable: true),
                    SemestrId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormKontrol", x => x.FormKontrolId);
                    table.ForeignKey(
                        name: "FK_FormKontrol_Semestr_SemestrId",
                        column: x => x.SemestrId,
                        principalTable: "Semestr",
                        principalColumn: "SemestrId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VidUchebRaboti",
                columns: table => new
                {
                    VidUchebRabotiId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SemestrId = table.Column<int>(nullable: true),
                    VidUchebRabotiName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VidUchebRaboti", x => x.VidUchebRabotiId);
                    table.ForeignKey(
                        name: "FK_VidUchebRaboti_Semestr_SemestrId",
                        column: x => x.SemestrId,
                        principalTable: "Semestr",
                        principalColumn: "SemestrId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicDegrees_AcademicDegreeGroupId",
                table: "AcademicDegrees",
                column: "AcademicDegreeGroupId");

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
                name: "IX_AspNetUsers_AcademicDegreeId",
                table: "AspNetUsers",
                column: "AcademicDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AcademicStatId",
                table: "AspNetUsers",
                column: "AcademicStatId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EduLevelGroupId",
                table: "AspNetUsers",
                column: "EduLevelGroupId");

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
                name: "IX_BlankNums_EduNapravlId",
                table: "BlankNums",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_BlokDiscipl_BlokDisciplNameId",
                table: "BlokDiscipl",
                column: "BlokDisciplNameId");

            migrationBuilder.CreateIndex(
                name: "IX_BlokDiscipl_EduPlanId",
                table: "BlokDiscipl",
                column: "EduPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_BlokDisciplChast_BlokDisciplChastNameId",
                table: "BlokDisciplChast",
                column: "BlokDisciplChastNameId");

            migrationBuilder.CreateIndex(
                name: "IX_BlokDisciplChast_BlokDisciplId",
                table: "BlokDisciplChast",
                column: "BlokDisciplId");

            migrationBuilder.CreateIndex(
                name: "IX_Discipline_BlokDisciplChastId",
                table: "Discipline",
                column: "BlokDisciplChastId");

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
                name: "IX_EduGraduate_EduProfileId",
                table: "EduGraduate",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EduGraduate_GraduateYearId",
                table: "EduGraduate",
                column: "GraduateYearId");

            migrationBuilder.CreateIndex(
                name: "IX_EduNapravls_EduQualificationId",
                table: "EduNapravls",
                column: "EduQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EduNapravls_EduUgsId",
                table: "EduNapravls",
                column: "EduUgsId");

            migrationBuilder.CreateIndex(
                name: "IX_EduNir_EduNapravlId",
                table: "EduNir",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_EduOPYears_EduOPEduYearNameId",
                table: "EduOPYears",
                column: "EduOPEduYearNameId");

            migrationBuilder.CreateIndex(
                name: "IX_EduOPYears_EduProfileId",
                table: "EduOPYears",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EduOPYears_EduYearBeginningTrainingId",
                table: "EduOPYears",
                column: "EduYearBeginningTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_eduPerevod_EduFormId",
                table: "eduPerevod",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_eduPerevod_EduNapravlId",
                table: "eduPerevod",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlanEduVidDeyats_EduPlanId",
                table: "EduPlanEduVidDeyats",
                column: "EduPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlanEduVidDeyats_EduVidDeyatId",
                table: "EduPlanEduVidDeyats",
                column: "EduVidDeyatId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlanEduYearBeginningTraining_EduPlanId",
                table: "EduPlanEduYearBeginningTraining",
                column: "EduPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlanEduYearBeginningTraining_EduYearBeginningTrainingId",
                table: "EduPlanEduYearBeginningTraining",
                column: "EduYearBeginningTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduFormId",
                table: "EduPlans",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduPlanPdfId",
                table: "EduPlans",
                column: "EduPlanPdfId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduProfileId",
                table: "EduPlans",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduProgramPodgId",
                table: "EduPlans",
                column: "EduProgramPodgId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_EduSrokId",
                table: "EduPlans",
                column: "EduSrokId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_StructKafId",
                table: "EduPlans",
                column: "StructKafId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPr_EduFormId",
                table: "EduPr",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPr_EduProfileId",
                table: "EduPr",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPr_EduYearBeginningTrainingId",
                table: "EduPr",
                column: "EduYearBeginningTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPriem_EduFormId",
                table: "EduPriem",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_EduPriem_EduNapravlId",
                table: "EduPriem",
                column: "EduNapravlId");

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
                name: "IX_EduYears_EduPlanId",
                table: "EduYears",
                column: "EduPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronBiblSystem_CopyDogovorId",
                table: "ElectronBiblSystem",
                column: "CopyDogovorId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectronObrazovatInformRes_ResId",
                table: "ElectronObrazovatInformRes",
                column: "ResId");

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
                name: "IX_FormKontrol_SemestrId",
                table: "FormKontrol",
                column: "SemestrId");

            migrationBuilder.CreateIndex(
                name: "IX_GraduateTrudoustroustvo_EduProfileId",
                table: "GraduateTrudoustroustvo",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GraduateTrudoustroustvo_GraduateYearId",
                table: "GraduateTrudoustroustvo",
                column: "GraduateYearId");

            migrationBuilder.CreateIndex(
                name: "IX_Korpus_AddressId",
                table: "Korpus",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurs_DisciplineId",
                table: "Kurs",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Kurs_EduKursId",
                table: "Kurs",
                column: "EduKursId");

            migrationBuilder.CreateIndex(
                name: "IX_Oborudovanie_PomeshenieId",
                table: "Oborudovanie",
                column: "PomeshenieId");

            migrationBuilder.CreateIndex(
                name: "IX_Pomeshenie_KorpusId",
                table: "Pomeshenie",
                column: "KorpusId");

            migrationBuilder.CreateIndex(
                name: "IX_PomeshenieTypepomesheniya_PomeshenieId",
                table: "PomeshenieTypepomesheniya",
                column: "PomeshenieId");

            migrationBuilder.CreateIndex(
                name: "IX_PomeshenieTypepomesheniya_PomeshenieTypeId",
                table: "PomeshenieTypepomesheniya",
                column: "PomeshenieTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PriemExam_EduNapravlId",
                table: "PriemExam",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_priemKolMest_EduNapravlId",
                table: "priemKolMest",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_priemKolTarget_EduNapravlId",
                table: "priemKolTarget",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalRetrainings_AppUserId",
                table: "ProfessionalRetrainings",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalRetrainings_ProfessionalRetrainingFileId",
                table: "ProfessionalRetrainings",
                column: "ProfessionalRetrainingFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_AppUserId",
                table: "Qualifications",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_RowStatusId",
                table: "Qualifications",
                column: "RowStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RefresherCourses_AppUserId",
                table: "RefresherCourses",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefresherCourses_RefresherCourseFileId",
                table: "RefresherCourses",
                column: "RefresherCourseFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Semestr_KursId",
                table: "Semestr",
                column: "KursId");

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
                name: "IX_TeacherEduProfileDisciplineNames_DisciplineNameId",
                table: "TeacherEduProfileDisciplineNames",
                column: "DisciplineNameId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherEduProfileDisciplineNames_EduProfileId",
                table: "TeacherEduProfileDisciplineNames",
                column: "EduProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherEduProfileDisciplineNames_TeacherId",
                table: "TeacherEduProfileDisciplineNames",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_AppUserId",
                table: "Teachers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStructKafPostStavka_PostId",
                table: "TeacherStructKafPostStavka",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStructKafPostStavka_StructKafId",
                table: "TeacherStructKafPostStavka",
                column: "StructKafId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherStructKafPostStavka_TeacherId",
                table: "TeacherStructKafPostStavka",
                column: "TeacherId");

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

            migrationBuilder.CreateIndex(
                name: "IX_VidUchebRaboti_SemestrId",
                table: "VidUchebRaboti",
                column: "SemestrId");
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
                name: "BlankNums");

            migrationBuilder.DropTable(
                name: "EduChislens");

            migrationBuilder.DropTable(
                name: "EduGraduate");

            migrationBuilder.DropTable(
                name: "EduNir");

            migrationBuilder.DropTable(
                name: "EduOPYears");

            migrationBuilder.DropTable(
                name: "eduPerevod");

            migrationBuilder.DropTable(
                name: "EduPlanEduVidDeyats");

            migrationBuilder.DropTable(
                name: "EduPlanEduYearBeginningTraining");

            migrationBuilder.DropTable(
                name: "EduPr");

            migrationBuilder.DropTable(
                name: "EduPriem");

            migrationBuilder.DropTable(
                name: "EduYears");

            migrationBuilder.DropTable(
                name: "ElectronBiblSystem");

            migrationBuilder.DropTable(
                name: "ElectronCatalog");

            migrationBuilder.DropTable(
                name: "ElectronObrazovatInformRes");

            migrationBuilder.DropTable(
                name: "Faxes");

            migrationBuilder.DropTable(
                name: "FileToFileTypes");

            migrationBuilder.DropTable(
                name: "FilInfo");

            migrationBuilder.DropTable(
                name: "FormKontrol");

            migrationBuilder.DropTable(
                name: "GraduateTrudoustroustvo");

            migrationBuilder.DropTable(
                name: "HostelInfo");

            migrationBuilder.DropTable(
                name: "Oborudovanie");

            migrationBuilder.DropTable(
                name: "PomeshenieTypepomesheniya");

            migrationBuilder.DropTable(
                name: "PriemExam");

            migrationBuilder.DropTable(
                name: "priemKolMest");

            migrationBuilder.DropTable(
                name: "priemKolTarget");

            migrationBuilder.DropTable(
                name: "ProfessionalRetrainings");

            migrationBuilder.DropTable(
                name: "PurposeLibr");

            migrationBuilder.DropTable(
                name: "Qualifications");

            migrationBuilder.DropTable(
                name: "RefresherCourses");

            migrationBuilder.DropTable(
                name: "RucovodstvoFil");

            migrationBuilder.DropTable(
                name: "SvedenRucovodstvo");

            migrationBuilder.DropTable(
                name: "TeacherEduProfileDisciplineNames");

            migrationBuilder.DropTable(
                name: "TeacherStructKafPostStavka");

            migrationBuilder.DropTable(
                name: "Telephones");

            migrationBuilder.DropTable(
                name: "UchredLaw");

            migrationBuilder.DropTable(
                name: "Vacants");

            migrationBuilder.DropTable(
                name: "VidUchebRaboti");

            migrationBuilder.DropTable(
                name: "Volume");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "EduOPEduYearNames");

            migrationBuilder.DropTable(
                name: "EduVidDeyat");

            migrationBuilder.DropTable(
                name: "EduYearBeginningTrainings");

            migrationBuilder.DropTable(
                name: "FileDataTypes");

            migrationBuilder.DropTable(
                name: "GraduateYear");

            migrationBuilder.DropTable(
                name: "Pomeshenie");

            migrationBuilder.DropTable(
                name: "PomeshenieType");

            migrationBuilder.DropTable(
                name: "RowStatuses");

            migrationBuilder.DropTable(
                name: "DisciplineNames");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Semestr");

            migrationBuilder.DropTable(
                name: "FileDataTypeGroups");

            migrationBuilder.DropTable(
                name: "Korpus");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Kurs");

            migrationBuilder.DropTable(
                name: "AcademicDegrees");

            migrationBuilder.DropTable(
                name: "AcademicStats");

            migrationBuilder.DropTable(
                name: "EduLevelGroups");

            migrationBuilder.DropTable(
                name: "Discipline");

            migrationBuilder.DropTable(
                name: "EduKurses");

            migrationBuilder.DropTable(
                name: "AcademicDegreeGroups");

            migrationBuilder.DropTable(
                name: "BlokDisciplChast");

            migrationBuilder.DropTable(
                name: "BlokDisciplChastName");

            migrationBuilder.DropTable(
                name: "BlokDiscipl");

            migrationBuilder.DropTable(
                name: "BlokDisciplName");

            migrationBuilder.DropTable(
                name: "EduPlans");

            migrationBuilder.DropTable(
                name: "EduForms");

            migrationBuilder.DropTable(
                name: "EduProfiles");

            migrationBuilder.DropTable(
                name: "EduProgramPodg");

            migrationBuilder.DropTable(
                name: "EduSrok");

            migrationBuilder.DropTable(
                name: "StructKafs");

            migrationBuilder.DropTable(
                name: "EduNapravls");

            migrationBuilder.DropTable(
                name: "StructFacultets");

            migrationBuilder.DropTable(
                name: "EduQualification");

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
