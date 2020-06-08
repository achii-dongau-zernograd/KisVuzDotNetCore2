using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class NumberOfPointsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumberOfPoints",
                table: "LmsEventLmsTasksetsAppUserAnswers",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumberOfPoints",
                table: "LmsEventLmsTasksetsAppUserAnswers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
