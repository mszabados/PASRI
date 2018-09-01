using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class DropCodeKeysAndRepairIssueWithDisplayTextAndCodeLengths : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceTypeBlood",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceTypeBlood",
                type: "char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 1);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceTypeBlood",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceSuffixName",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceSuffixName",
                type: "char(4)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 4);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceSuffixName",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceState",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceState",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 2);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceState",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceReligionDemographic",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceReligionDemographic",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 2);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceReligionDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceRaceDemographic",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceRaceDemographic",
                type: "char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 1);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceRaceDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceHairColor",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceHairColor",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 2);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceHairColor",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceGenderDemographic",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceGenderDemographic",
                type: "char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 1);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceGenderDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceEyeColor",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceEyeColor",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 2);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceEyeColor",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceEthnicGroupDemographic",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceEthnicGroupDemographic",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 2);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceEthnicGroupDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceCountry",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceCountry",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldMaxLength: 2);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceCountry",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceTypeBlood",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceTypeBlood",
                type: "char",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceSuffixName",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceSuffixName",
                type: "char",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(4)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceState",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceState",
                type: "char",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceReligionDemographic",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceReligionDemographic",
                type: "char",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceRaceDemographic",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceRaceDemographic",
                type: "char",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceHairColor",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceHairColor",
                type: "char",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceGenderDemographic",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceGenderDemographic",
                type: "char",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceEyeColor",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceEyeColor",
                type: "char",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceEthnicGroupDemographic",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceEthnicGroupDemographic",
                type: "char",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceCountry",
                type: "varchar",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceCountry",
                type: "char",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");

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
    }
}
