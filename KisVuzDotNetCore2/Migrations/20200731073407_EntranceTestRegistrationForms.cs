using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KisVuzDotNetCore2.Migrations
{
    public partial class EntranceTestRegistrationForms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntranceTestRegistrationForms",
                columns: table => new
                {
                    EntranceTestRegistrationFormId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AbiturientId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DisciplineName = table.Column<string>(nullable: true),
                    DocumentNumber = table.Column<string>(nullable: true),
                    DocumentSeriya = table.Column<string>(nullable: true),
                    EntranceTestResult = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    LmsEventId = table.Column<int>(nullable: false),
                    Patronymic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntranceTestRegistrationForms", x => x.EntranceTestRegistrationFormId);
                    table.ForeignKey(
                        name: "FK_EntranceTestRegistrationForms_Abiturients_AbiturientId",
                        column: x => x.AbiturientId,
                        principalTable: "Abiturients",
                        principalColumn: "AbiturientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntranceTestRegistrationForms_LmsEvents_LmsEventId",
                        column: x => x.LmsEventId,
                        principalTable: "LmsEvents",
                        principalColumn: "LmsEventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntranceTestRegistrationForms_AbiturientId",
                table: "EntranceTestRegistrationForms",
                column: "AbiturientId");

            migrationBuilder.CreateIndex(
                name: "IX_EntranceTestRegistrationForms_LmsEventId",
                table: "EntranceTestRegistrationForms",
                column: "LmsEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntranceTestRegistrationForms");
        }
    }
}
