using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class PassportNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "PassportDataSet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportSeriya",
                table: "PassportDataSet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "PassportDataSet");

            migrationBuilder.DropColumn(
                name: "PassportSeriya",
                table: "PassportDataSet");
        }
    }
}
