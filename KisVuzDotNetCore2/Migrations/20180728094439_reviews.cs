using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWorkReviewMarks",
                columns: table => new
                {
                    UserWorkReviewMarkId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserWorkReviewMarkName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkReviewMarks", x => x.UserWorkReviewMarkId);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkTypes",
                columns: table => new
                {
                    UserWorkTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserWorkTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkTypes", x => x.UserWorkTypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserWorks",
                columns: table => new
                {
                    UserWorkId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    DatePosting = table.Column<DateTime>(nullable: false),
                    FileModelId = table.Column<int>(nullable: true),
                    UserWorkDescription = table.Column<string>(nullable: true),
                    UserWorkName = table.Column<string>(nullable: true),
                    UserWorkTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorks", x => x.UserWorkId);
                    table.ForeignKey(
                        name: "FK_UserWorks_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserWorks_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserWorks_UserWorkTypes_UserWorkTypeId",
                        column: x => x.UserWorkTypeId,
                        principalTable: "UserWorkTypes",
                        principalColumn: "UserWorkTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWorkReviews",
                columns: table => new
                {
                    UserWorkReviewId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileModelId = table.Column<int>(nullable: false),
                    ReviewerId = table.Column<string>(nullable: true),
                    UserWorkId = table.Column<int>(nullable: false),
                    UserWorkReviewMarkId = table.Column<int>(nullable: false),
                    UserWorkReviewText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkReviews", x => x.UserWorkReviewId);
                    table.ForeignKey(
                        name: "FK_UserWorkReviews_Files_FileModelId",
                        column: x => x.FileModelId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkReviews_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserWorkReviews_UserWorks_UserWorkId",
                        column: x => x.UserWorkId,
                        principalTable: "UserWorks",
                        principalColumn: "UserWorkId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkReviews_UserWorkReviewMarks_UserWorkReviewMarkId",
                        column: x => x.UserWorkReviewMarkId,
                        principalTable: "UserWorkReviewMarks",
                        principalColumn: "UserWorkReviewMarkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkReviews_FileModelId",
                table: "UserWorkReviews",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkReviews_ReviewerId",
                table: "UserWorkReviews",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkReviews_UserWorkId",
                table: "UserWorkReviews",
                column: "UserWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkReviews_UserWorkReviewMarkId",
                table: "UserWorkReviews",
                column: "UserWorkReviewMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorks_AppUserId",
                table: "UserWorks",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorks_FileModelId",
                table: "UserWorks",
                column: "FileModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorks_UserWorkTypeId",
                table: "UserWorks",
                column: "UserWorkTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWorkReviews");

            migrationBuilder.DropTable(
                name: "UserWorks");

            migrationBuilder.DropTable(
                name: "UserWorkReviewMarks");

            migrationBuilder.DropTable(
                name: "UserWorkTypes");
        }
    }
}
