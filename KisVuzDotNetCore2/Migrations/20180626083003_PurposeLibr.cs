using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class PurposeLibr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurposeLibr");
        }
    }
}
