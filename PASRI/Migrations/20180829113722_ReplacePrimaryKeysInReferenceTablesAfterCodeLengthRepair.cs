using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class ReplacePrimaryKeysInReferenceTablesAfterCodeLengthRepair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceSuffixName",
                table: "ReferenceSuffixName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceState",
                table: "ReferenceState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceReligionDemographic",
                table: "ReferenceReligionDemographic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceRaceDemographic",
                table: "ReferenceRaceDemographic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceHairColor",
                table: "ReferenceHairColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceGenderDemographic",
                table: "ReferenceGenderDemographic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceEyeColor",
                table: "ReferenceEyeColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceEthnicGroupDemographic",
                table: "ReferenceEthnicGroupDemographic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceCountry",
                table: "ReferenceCountry");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceTypeBlood");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceSuffixName");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceState");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceReligionDemographic");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceRaceDemographic");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceHairColor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceGenderDemographic");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceEyeColor");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceEthnicGroupDemographic");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceCountry");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceTypeBlood",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceTypeBlood",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceSuffixName",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceSuffixName",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceState",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceState",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceReligionDemographic",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceReligionDemographic",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceRaceDemographic",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceRaceDemographic",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceHairColor",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceHairColor",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceGenderDemographic",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceGenderDemographic",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceEyeColor",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceEyeColor",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceEthnicGroupDemographic",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceEthnicGroupDemographic",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceCountry",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceCountry",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                type: "char(4)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 4,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Middle",
                table: "PersonLegalNameIdentification",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Last",
                table: "PersonLegalNameIdentification",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Full",
                table: "PersonLegalNameIdentification",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "First",
                table: "PersonLegalNameIdentification",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceSuffixName",
                table: "ReferenceSuffixName",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceState",
                table: "ReferenceState",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceReligionDemographic",
                table: "ReferenceReligionDemographic",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceRaceDemographic",
                table: "ReferenceRaceDemographic",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceHairColor",
                table: "ReferenceHairColor",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceGenderDemographic",
                table: "ReferenceGenderDemographic",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceEyeColor",
                table: "ReferenceEyeColor",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceEthnicGroupDemographic",
                table: "ReferenceEthnicGroupDemographic",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceCountry",
                table: "ReferenceCountry",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_PersonLegalNameIdentification_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                column: "ReferenceSuffixNameCode",
                unique: true,
                filter: "[ReferenceSuffixNameCode] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLegalNameIdentification_ReferenceSuffixName_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                column: "ReferenceSuffixNameCode",
                principalTable: "ReferenceSuffixName",
                principalColumn: "Code",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonLegalNameIdentification_ReferenceSuffixName_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceSuffixName",
                table: "ReferenceSuffixName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceState",
                table: "ReferenceState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceReligionDemographic",
                table: "ReferenceReligionDemographic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceRaceDemographic",
                table: "ReferenceRaceDemographic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceHairColor",
                table: "ReferenceHairColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceGenderDemographic",
                table: "ReferenceGenderDemographic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceEyeColor",
                table: "ReferenceEyeColor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceEthnicGroupDemographic",
                table: "ReferenceEthnicGroupDemographic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceCountry",
                table: "ReferenceCountry");

            migrationBuilder.DropIndex(
                name: "IX_PersonLegalNameIdentification_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceTypeBlood",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceTypeBlood",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceTypeBlood",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceSuffixName",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceSuffixName",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceSuffixName",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceState",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceState",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceState",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceReligionDemographic",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceReligionDemographic",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceReligionDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceRaceDemographic",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceRaceDemographic",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceRaceDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceHairColor",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceHairColor",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceHairColor",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceGenderDemographic",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceGenderDemographic",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceGenderDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceEyeColor",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceEyeColor",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceEyeColor",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceEthnicGroupDemographic",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceEthnicGroupDemographic",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceEthnicGroupDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceCountry",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceCountry",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceCountry",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                type: "char",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Middle",
                table: "PersonLegalNameIdentification",
                type: "varchar",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Last",
                table: "PersonLegalNameIdentification",
                type: "varchar",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Full",
                table: "PersonLegalNameIdentification",
                type: "varchar",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "First",
                table: "PersonLegalNameIdentification",
                type: "varchar",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceSuffixName",
                table: "ReferenceSuffixName",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceState",
                table: "ReferenceState",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceReligionDemographic",
                table: "ReferenceReligionDemographic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceRaceDemographic",
                table: "ReferenceRaceDemographic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceHairColor",
                table: "ReferenceHairColor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceGenderDemographic",
                table: "ReferenceGenderDemographic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceEyeColor",
                table: "ReferenceEyeColor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceEthnicGroupDemographic",
                table: "ReferenceEthnicGroupDemographic",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceCountry",
                table: "ReferenceCountry",
                column: "Id");
        }
    }
}
