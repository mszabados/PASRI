using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class ChangeDescriptionColumnsToLongNameToAvoidKeywordConflict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "description",
                schema: "RE",
                table: "STATE",
                newName: "long_name");

            migrationBuilder.RenameColumn(
                "description",
                schema: "RE",
                table: "COUNTRY",
                newName: "long_name");

            migrationBuilder.RenameColumn(
                "description",
                schema: "PERSON",
                table: "RE_SUFFIX",
                newName: "long_name");

            migrationBuilder.RenameColumn(
                "description",
                schema: "PERSON",
                table: "RE_RELIGION",
                newName: "long_name");

            migrationBuilder.RenameColumn(
                "description",
                schema: "PERSON",
                table: "RE_RACE",
                newName: "long_name");

            migrationBuilder.RenameColumn(
                "description",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                newName: "long_name");

            migrationBuilder.RenameColumn(
                "description",
                schema: "PERSON",
                table: "RE_GENDER",
                newName: "long_name");

            migrationBuilder.RenameColumn(
                "description",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                newName: "long_name");

            migrationBuilder.RenameColumn(
                "description",
                schema: "PERSON",
                table: "RE_ETHNIC",
                newName: "long_name");

            migrationBuilder.RenameColumn(
                "description",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                newName: "long_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "long_name",
                schema: "RE",
                table: "STATE",
                newName: "description");

            migrationBuilder.RenameColumn(
                "long_name",
                schema: "RE",
                table: "COUNTRY",
                newName: "description");

            migrationBuilder.RenameColumn(
                "long_name",
                schema: "PERSON",
                table: "RE_SUFFIX",
                newName: "description");

            migrationBuilder.RenameColumn(
                "long_name",
                schema: "PERSON",
                table: "RE_RELIGION",
                newName: "description");

            migrationBuilder.RenameColumn(
                "long_name",
                schema: "PERSON",
                table: "RE_RACE",
                newName: "description");

            migrationBuilder.RenameColumn(
                "long_name",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                newName: "description");

            migrationBuilder.RenameColumn(
                "long_name",
                schema: "PERSON",
                table: "RE_GENDER",
                newName: "description");

            migrationBuilder.RenameColumn(
                "long_name",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                newName: "description");

            migrationBuilder.RenameColumn(
                "long_name",
                schema: "PERSON",
                table: "RE_ETHNIC",
                newName: "description");

            migrationBuilder.RenameColumn(
                "long_name",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                newName: "description");
        }
    }
}
