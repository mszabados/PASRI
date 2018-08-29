using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.Migrations
{
    public partial class ReplacePrimaryKeysInReferenceTablesToCharCodeFieldRemoveId : Migration
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

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceTypeBlood",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceSuffixName",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceState",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceReligionDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceRaceDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceHairColor",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceGenderDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceEyeColor",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceEthnicGroupDemographic",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
    }
}
