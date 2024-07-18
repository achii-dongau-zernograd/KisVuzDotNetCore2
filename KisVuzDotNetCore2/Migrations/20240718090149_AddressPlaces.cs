using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class AddressPlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressPlaces",
                columns: table => new
                {
                    AddressPlaceId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressPlaceName = table.Column<string>(nullable: true),
                    IsPlaceDop = table.Column<bool>(nullable: false),
                    IsPlaceGia = table.Column<bool>(nullable: false),
                    IsPlaceOppo = table.Column<bool>(nullable: false),
                    IsPlacePodg = table.Column<bool>(nullable: false),
                    IsPlacePrac = table.Column<bool>(nullable: false),
                    IsPlaceSet = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressPlaces", x => x.AddressPlaceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressPlaces");
        }
    }
}
