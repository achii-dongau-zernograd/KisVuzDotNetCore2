using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class UchredLaw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UchredLaw",
                columns: table => new
                {
                    UchredLawId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressUchred = table.Column<string>(nullable: true),
                    FullnameUchred = table.Column<string>(nullable: true),
                    NameUchred = table.Column<string>(nullable: true),
                    TelUchred = table.Column<string>(nullable: true),
                    WebsiteUchred = table.Column<string>(nullable: true),
                    mailUchred = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UchredLaw", x => x.UchredLawId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UchredLaw");
        }
    }
}
