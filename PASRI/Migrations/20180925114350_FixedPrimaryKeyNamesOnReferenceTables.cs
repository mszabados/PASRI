using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class FixedPrimaryKeyNamesOnReferenceTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_CountryId",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_STATE_country_id",
                table: "BIRTH");

            migrationBuilder.DropIndex(
                name: "IX_BIRTH_CountryId",
                table: "BIRTH");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "BIRTH");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "RE_SUFFIX",
                newName: "suffix_id");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "RE_RELIGION",
                newName: "religion_id");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "RE_RACE",
                newName: "race_id");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "RE_HAIR_COLOR",
                newName: "hair_color_id");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "RE_GENDER",
                newName: "gender_id");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "RE_EYE_COLOR",
                newName: "eye_color_id");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "RE_ETHNIC",
                newName: "ethnic_id");

            migrationBuilder.AddColumn<int>(
                name: "state_province_id",
                table: "BIRTH",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BIRTH_state_province_id",
                table: "BIRTH",
                column: "state_province_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_country_id",
                table: "BIRTH",
                column: "country_id",
                principalTable: "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_STATE_state_province_id",
                table: "BIRTH",
                column: "state_province_id",
                principalTable: "RE_STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_country_id",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_STATE_state_province_id",
                table: "BIRTH");

            migrationBuilder.DropIndex(
                name: "IX_BIRTH_state_province_id",
                table: "BIRTH");

            migrationBuilder.DropColumn(
                name: "state_province_id",
                table: "BIRTH");

            migrationBuilder.RenameColumn(
                name: "suffix_id",
                table: "RE_SUFFIX",
                newName: "country_id");

            migrationBuilder.RenameColumn(
                name: "religion_id",
                table: "RE_RELIGION",
                newName: "country_id");

            migrationBuilder.RenameColumn(
                name: "race_id",
                table: "RE_RACE",
                newName: "country_id");

            migrationBuilder.RenameColumn(
                name: "hair_color_id",
                table: "RE_HAIR_COLOR",
                newName: "country_id");

            migrationBuilder.RenameColumn(
                name: "gender_id",
                table: "RE_GENDER",
                newName: "country_id");

            migrationBuilder.RenameColumn(
                name: "eye_color_id",
                table: "RE_EYE_COLOR",
                newName: "country_id");

            migrationBuilder.RenameColumn(
                name: "ethnic_id",
                table: "RE_ETHNIC",
                newName: "country_id");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "BIRTH",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BIRTH_CountryId",
                table: "BIRTH",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_CountryId",
                table: "BIRTH",
                column: "CountryId",
                principalTable: "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_STATE_country_id",
                table: "BIRTH",
                column: "country_id",
                principalTable: "RE_STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
