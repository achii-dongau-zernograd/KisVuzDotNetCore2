using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AppUserAbiturientConsultants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserAbiturientConsultants",
                columns: table => new
                {
                    AppUserAbiturientConsultantId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserAbiturientConsultants", x => x.AppUserAbiturientConsultantId);
                    table.ForeignKey(
                        name: "FK_AppUserAbiturientConsultants_Abiturients_AbiturientId",
                        column: x => x.AbiturientId,
                        principalTable: "Abiturients",
                        principalColumn: "AbiturientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserAbiturientConsultants_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserAbiturientConsultants_AbiturientId",
                table: "AppUserAbiturientConsultants",
                column: "AbiturientId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserAbiturientConsultants_AppUserId",
                table: "AppUserAbiturientConsultants",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserAbiturientConsultants");
        }
    }
}
