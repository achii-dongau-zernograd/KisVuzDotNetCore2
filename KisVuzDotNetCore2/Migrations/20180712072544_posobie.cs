using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class posobie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<int>(nullable: true),
                    AppUserId1 = table.Column<string>(nullable: true),
                    AuthorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorId);
                    table.ForeignKey(
                        name: "FK_Author_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UchPosobieFormaIzdaniya",
                columns: table => new
                {
                    UchPosobieFormaIzdaniyaId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UchPosobieFormaIzdaniyaName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UchPosobieFormaIzdaniya", x => x.UchPosobieFormaIzdaniyaId);
                });

            migrationBuilder.CreateTable(
                name: "UchPosobieVid",
                columns: table => new
                {
                    UchPosobieVidId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UchPosobieVidName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UchPosobieVid", x => x.UchPosobieVidId);
                });

            migrationBuilder.CreateTable(
                name: "UchPosobie",
                columns: table => new
                {
                    UchPosobieId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BiblOpisanie = table.Column<string>(nullable: true),
                    FileModelId = table.Column<int>(nullable: false),
                    GodIzdaniya = table.Column<string>(nullable: true),
                    UchPosobieFormaIzdaniyaId = table.Column<int>(nullable: true),
                    UchPosobieName = table.Column<string>(nullable: true),
                    UchPosobieVidId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UchPosobie", x => x.UchPosobieId);
                    table.ForeignKey(
                        name: "FK_UchPosobie_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UchPosobie_UchPosobieFormaIzdaniya_UchPosobieFormaIzdaniyaId",
                        column: x => x.UchPosobieFormaIzdaniyaId,
                        principalTable: "UchPosobieFormaIzdaniya",
                        principalColumn: "UchPosobieFormaIzdaniyaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UchPosobie_UchPosobieVid_UchPosobieVidId",
                        column: x => x.UchPosobieVidId,
                        principalTable: "UchPosobieVid",
                        principalColumn: "UchPosobieVidId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UchPosobieAuthor",
                columns: table => new
                {
                    UchPosobieAuthorId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AuthorId = table.Column<int>(nullable: false),
                    UchPosobieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UchPosobieAuthor", x => x.UchPosobieAuthorId);
                    table.ForeignKey(
                        name: "FK_UchPosobieAuthor_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Author",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UchPosobieAuthor_UchPosobie_UchPosobieId",
                        column: x => x.UchPosobieId,
                        principalTable: "UchPosobie",
                        principalColumn: "UchPosobieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UchPosobieDisciplineName",
                columns: table => new
                {
                    UchPosobieDisciplineNameId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisciplineNameId = table.Column<int>(nullable: false),
                    UchPosobieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UchPosobieDisciplineName", x => x.UchPosobieDisciplineNameId);
                    table.ForeignKey(
                        name: "FK_UchPosobieDisciplineName_DisciplineNames_DisciplineNameId",
                        column: x => x.DisciplineNameId,
                        principalTable: "DisciplineNames",
                        principalColumn: "DisciplineNameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UchPosobieDisciplineName_UchPosobie_UchPosobieId",
                        column: x => x.UchPosobieId,
                        principalTable: "UchPosobie",
                        principalColumn: "UchPosobieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UchPosobieEduForm",
                columns: table => new
                {
                    UchPosobieEduFormId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduFormId = table.Column<int>(nullable: false),
                    UchPosobieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UchPosobieEduForm", x => x.UchPosobieEduFormId);
                    table.ForeignKey(
                        name: "FK_UchPosobieEduForm_EduForms_EduFormId",
                        column: x => x.EduFormId,
                        principalTable: "EduForms",
                        principalColumn: "EduFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UchPosobieEduForm_UchPosobie_UchPosobieId",
                        column: x => x.UchPosobieId,
                        principalTable: "UchPosobie",
                        principalColumn: "UchPosobieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UchPosobieEduNapravl",
                columns: table => new
                {
                    UchPosobieEduNapravlId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EduNapravlId = table.Column<int>(nullable: false),
                    UchPosobieId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UchPosobieEduNapravl", x => x.UchPosobieEduNapravlId);
                    table.ForeignKey(
                        name: "FK_UchPosobieEduNapravl_EduNapravls_EduNapravlId",
                        column: x => x.EduNapravlId,
                        principalTable: "EduNapravls",
                        principalColumn: "EduNapravlId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UchPosobieEduNapravl_UchPosobie_UchPosobieId",
                        column: x => x.UchPosobieId,
                        principalTable: "UchPosobie",
                        principalColumn: "UchPosobieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Author_AppUserId1",
                table: "Author",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobie_FileModelId",
                table: "UchPosobie",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobie_UchPosobieFormaIzdaniyaId",
                table: "UchPosobie",
                column: "UchPosobieFormaIzdaniyaId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobie_UchPosobieVidId",
                table: "UchPosobie",
                column: "UchPosobieVidId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobieAuthor_AuthorId",
                table: "UchPosobieAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobieAuthor_UchPosobieId",
                table: "UchPosobieAuthor",
                column: "UchPosobieId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobieDisciplineName_DisciplineNameId",
                table: "UchPosobieDisciplineName",
                column: "DisciplineNameId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobieDisciplineName_UchPosobieId",
                table: "UchPosobieDisciplineName",
                column: "UchPosobieId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobieEduForm_EduFormId",
                table: "UchPosobieEduForm",
                column: "EduFormId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobieEduForm_UchPosobieId",
                table: "UchPosobieEduForm",
                column: "UchPosobieId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobieEduNapravl_EduNapravlId",
                table: "UchPosobieEduNapravl",
                column: "EduNapravlId");

            migrationBuilder.CreateIndex(
                name: "IX_UchPosobieEduNapravl_UchPosobieId",
                table: "UchPosobieEduNapravl",
                column: "UchPosobieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UchPosobieAuthor");

            migrationBuilder.DropTable(
                name: "UchPosobieDisciplineName");

            migrationBuilder.DropTable(
                name: "UchPosobieEduForm");

            migrationBuilder.DropTable(
                name: "UchPosobieEduNapravl");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "UchPosobie");

            migrationBuilder.DropTable(
                name: "UchPosobieFormaIzdaniya");

            migrationBuilder.DropTable(
                name: "UchPosobieVid");
        }
    }
}
