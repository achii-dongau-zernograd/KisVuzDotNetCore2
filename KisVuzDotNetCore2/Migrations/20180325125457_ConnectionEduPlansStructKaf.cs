using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class ConnectionEduPlansStructKaf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StructKafId",
                table: "EduPlans",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EduPlans_StructKafId",
                table: "EduPlans",
                column: "StructKafId");

            migrationBuilder.AddForeignKey(
                name: "FK_EduPlans_StructKafs_StructKafId",
                table: "EduPlans",
                column: "StructKafId",
                principalTable: "StructKafs",
                principalColumn: "StructKafId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EduPlans_StructKafs_StructKafId",
                table: "EduPlans");

            migrationBuilder.DropIndex(
                name: "IX_EduPlans_StructKafId",
                table: "EduPlans");

            migrationBuilder.DropColumn(
                name: "StructKafId",
                table: "EduPlans");
        }
    }
}
