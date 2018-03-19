using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class StructOrgUprav : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StructOrgUprav",
                columns: table => new
                {
                    StructOrgUpravId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressStr = table.Column<string>(nullable: true),
                    DivisionClauseDocLink = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Fio = table.Column<string>(nullable: true),
                    IsOrgUprav = table.Column<bool>(nullable: false),
                    Post = table.Column<string>(nullable: true),
                    Site = table.Column<string>(nullable: true),
                    StructOrgUpravName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructOrgUprav", x => x.StructOrgUpravId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StructOrgUprav");
        }
    }
}
