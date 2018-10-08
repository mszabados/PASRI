using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class FixedPrimaryKeyNamesOnReferenceTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_BIRTH_RE_COUNTRY_CountryId",
                "BIRTH");

            migrationBuilder.DropForeignKey(
                "FK_BIRTH_RE_STATE_country_id",
                "BIRTH");

            migrationBuilder.DropIndex(
                "IX_BIRTH_CountryId",
                "BIRTH");

            migrationBuilder.DropColumn(
                "CountryId",
                "BIRTH");

            migrationBuilder.RenameColumn(
                "country_id",
                "RE_SUFFIX",
                "suffix_id");

            migrationBuilder.RenameColumn(
                "country_id",
                "RE_RELIGION",
                "religion_id");

            migrationBuilder.RenameColumn(
                "country_id",
                "RE_RACE",
                "race_id");

            migrationBuilder.RenameColumn(
                "country_id",
                "RE_HAIR_COLOR",
                "hair_color_id");

            migrationBuilder.RenameColumn(
                "country_id",
                "RE_GENDER",
                "gender_id");

            migrationBuilder.RenameColumn(
                "country_id",
                "RE_EYE_COLOR",
                "eye_color_id");

            migrationBuilder.RenameColumn(
                "country_id",
                "RE_ETHNIC",
                "ethnic_id");

            migrationBuilder.AddColumn<int>(
                "state_province_id",
                "BIRTH",
                "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_BIRTH_state_province_id",
                "BIRTH",
                "state_province_id");

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_RE_COUNTRY_country_id",
                "BIRTH",
                "country_id",
                "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_RE_STATE_state_province_id",
                "BIRTH",
                "state_province_id",
                "RE_STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_BIRTH_RE_COUNTRY_country_id",
                "BIRTH");

            migrationBuilder.DropForeignKey(
                "FK_BIRTH_RE_STATE_state_province_id",
                "BIRTH");

            migrationBuilder.DropIndex(
                "IX_BIRTH_state_province_id",
                "BIRTH");

            migrationBuilder.DropColumn(
                "state_province_id",
                "BIRTH");

            migrationBuilder.RenameColumn(
                "suffix_id",
                "RE_SUFFIX",
                "country_id");

            migrationBuilder.RenameColumn(
                "religion_id",
                "RE_RELIGION",
                "country_id");

            migrationBuilder.RenameColumn(
                "race_id",
                "RE_RACE",
                "country_id");

            migrationBuilder.RenameColumn(
                "hair_color_id",
                "RE_HAIR_COLOR",
                "country_id");

            migrationBuilder.RenameColumn(
                "gender_id",
                "RE_GENDER",
                "country_id");

            migrationBuilder.RenameColumn(
                "eye_color_id",
                "RE_EYE_COLOR",
                "country_id");

            migrationBuilder.RenameColumn(
                "ethnic_id",
                "RE_ETHNIC",
                "country_id");

            migrationBuilder.AddColumn<int>(
                "CountryId",
                "BIRTH",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_BIRTH_CountryId",
                "BIRTH",
                "CountryId");

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_RE_COUNTRY_CountryId",
                "BIRTH",
                "CountryId",
                "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_RE_STATE_country_id",
                "BIRTH",
                "country_id",
                "RE_STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
