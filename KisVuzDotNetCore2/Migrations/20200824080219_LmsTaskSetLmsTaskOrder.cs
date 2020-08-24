using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class LmsTaskSetLmsTaskOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LmsTaskSetLmsTaskOrder",
                table: "LmsTaskSetLmsTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LmsTaskSetLmsTaskOrder",
                table: "LmsTaskSetLmsTasks");
        }
    }
}
