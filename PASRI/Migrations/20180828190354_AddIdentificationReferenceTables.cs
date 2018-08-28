using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.Migrations
{
    public partial class AddIdentificationReferenceTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceSuffixName",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ReferenceCountry",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceCountry", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceEthnicGroupDemographic",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceEthnicGroupDemographic", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceEyeColor",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceEyeColor", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceGenderDemographic",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 1, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceGenderDemographic", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceHairColor",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceHairColor", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceRaceDemographic",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 1, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceRaceDemographic", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceReligionDemographic",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceReligionDemographic", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceState",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 2, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceState", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "ReferenceTypeBlood",
                columns: table => new
                {
                    Code = table.Column<string>(maxLength: 1, nullable: false),
                    DisplayText = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferenceTypeBlood", x => x.Code);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReferenceCountry");

            migrationBuilder.DropTable(
                name: "ReferenceEthnicGroupDemographic");

            migrationBuilder.DropTable(
                name: "ReferenceEyeColor");

            migrationBuilder.DropTable(
                name: "ReferenceGenderDemographic");

            migrationBuilder.DropTable(
                name: "ReferenceHairColor");

            migrationBuilder.DropTable(
                name: "ReferenceRaceDemographic");

            migrationBuilder.DropTable(
                name: "ReferenceReligionDemographic");

            migrationBuilder.DropTable(
                name: "ReferenceState");

            migrationBuilder.DropTable(
                name: "ReferenceTypeBlood");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceSuffixName",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }
    }
}
