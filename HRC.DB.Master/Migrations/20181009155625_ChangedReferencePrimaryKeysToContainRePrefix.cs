using Microsoft.EntityFrameworkCore.Migrations;

namespace HRC.DB.Master.Migrations
{
    public partial class ChangedReferencePrimaryKeysToContainRePrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PERSON_RE_SUFFIX_suffix_id",
                schema: "PERSON",
                table: "PERSON");

            migrationBuilder.RenameColumn(
                name: "suffix_id",
                schema: "PERSON",
                table: "RE_SUFFIX",
                newName: "re_suffix_id");

            migrationBuilder.RenameColumn(
                name: "religion_id",
                schema: "PERSON",
                table: "RE_RELIGION",
                newName: "re_religion_id");

            migrationBuilder.RenameColumn(
                name: "race_id",
                schema: "PERSON",
                table: "RE_RACE",
                newName: "re_race_id");

            migrationBuilder.RenameColumn(
                name: "hair_color_id",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                newName: "re_hair_color_id");

            migrationBuilder.RenameColumn(
                name: "gender_id",
                schema: "PERSON",
                table: "RE_GENDER",
                newName: "re_gender_id");

            migrationBuilder.RenameColumn(
                name: "eye_color_id",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                newName: "re_eye_color_id");

            migrationBuilder.RenameColumn(
                name: "ethnic_id",
                schema: "PERSON",
                table: "RE_ETHNIC",
                newName: "re_ethnic_id");

            migrationBuilder.RenameColumn(
                name: "blood_type_id",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                newName: "re_blood_type_id");

            migrationBuilder.RenameColumn(
                name: "suffix_id",
                schema: "PERSON",
                table: "PERSON",
                newName: "re_suffix_id");

            migrationBuilder.RenameIndex(
                name: "IX_PERSON_suffix_id",
                schema: "PERSON",
                table: "PERSON",
                newName: "IX_PERSON_re_suffix_id");

            migrationBuilder.AddForeignKey(
                name: "FK_PERSON_RE_SUFFIX_re_suffix_id",
                schema: "PERSON",
                table: "PERSON",
                column: "re_suffix_id",
                principalSchema: "PERSON",
                principalTable: "RE_SUFFIX",
                principalColumn: "re_suffix_id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PERSON_RE_SUFFIX_re_suffix_id",
                schema: "PERSON",
                table: "PERSON");

            migrationBuilder.RenameColumn(
                name: "re_suffix_id",
                schema: "PERSON",
                table: "RE_SUFFIX",
                newName: "suffix_id");

            migrationBuilder.RenameColumn(
                name: "re_religion_id",
                schema: "PERSON",
                table: "RE_RELIGION",
                newName: "religion_id");

            migrationBuilder.RenameColumn(
                name: "re_race_id",
                schema: "PERSON",
                table: "RE_RACE",
                newName: "race_id");

            migrationBuilder.RenameColumn(
                name: "re_hair_color_id",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                newName: "hair_color_id");

            migrationBuilder.RenameColumn(
                name: "re_gender_id",
                schema: "PERSON",
                table: "RE_GENDER",
                newName: "gender_id");

            migrationBuilder.RenameColumn(
                name: "re_eye_color_id",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                newName: "eye_color_id");

            migrationBuilder.RenameColumn(
                name: "re_ethnic_id",
                schema: "PERSON",
                table: "RE_ETHNIC",
                newName: "ethnic_id");

            migrationBuilder.RenameColumn(
                name: "re_blood_type_id",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                newName: "blood_type_id");

            migrationBuilder.RenameColumn(
                name: "re_suffix_id",
                schema: "PERSON",
                table: "PERSON",
                newName: "suffix_id");

            migrationBuilder.RenameIndex(
                name: "IX_PERSON_re_suffix_id",
                schema: "PERSON",
                table: "PERSON",
                newName: "IX_PERSON_suffix_id");

            migrationBuilder.AddForeignKey(
                name: "FK_PERSON_RE_SUFFIX_suffix_id",
                schema: "PERSON",
                table: "PERSON",
                column: "suffix_id",
                principalSchema: "PERSON",
                principalTable: "RE_SUFFIX",
                principalColumn: "suffix_id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
