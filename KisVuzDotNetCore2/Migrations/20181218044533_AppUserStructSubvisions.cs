using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AppUserStructSubvisions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmploymentForms",
                columns: table => new
                {
                    EmploymentFormId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmploymentFormName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentForms", x => x.EmploymentFormId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserStructSubvisions",
                columns: table => new
                {
                    AppUserStructSubvisionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    EmploymentFormId = table.Column<int>(nullable: false),
                    PostId = table.Column<int>(nullable: false),
                    StructSubvisionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserStructSubvisions", x => x.AppUserStructSubvisionId);
                    table.ForeignKey(
                        name: "FK_AppUserStructSubvisions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserStructSubvisions_EmploymentForms_EmploymentFormId",
                        column: x => x.EmploymentFormId,
                        principalTable: "EmploymentForms",
                        principalColumn: "EmploymentFormId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserStructSubvisions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserStructSubvisions_StructSubvisions_StructSubvisionId",
                        column: x => x.StructSubvisionId,
                        principalTable: "StructSubvisions",
                        principalColumn: "StructSubvisionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserStructSubvisions_AppUserId",
                table: "AppUserStructSubvisions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserStructSubvisions_EmploymentFormId",
                table: "AppUserStructSubvisions",
                column: "EmploymentFormId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserStructSubvisions_PostId",
                table: "AppUserStructSubvisions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserStructSubvisions_StructSubvisionId",
                table: "AppUserStructSubvisions",
                column: "StructSubvisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserStructSubvisions");

            migrationBuilder.DropTable(
                name: "EmploymentForms");
        }
    }
}
