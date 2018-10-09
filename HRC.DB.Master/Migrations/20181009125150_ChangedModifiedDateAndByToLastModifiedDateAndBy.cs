using Microsoft.EntityFrameworkCore.Migrations;

namespace HRC.DB.Master.Migrations
{
    public partial class ChangedModifiedDateAndByToLastModifiedDateAndBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "RE",
                table: "RE_COUNTRY",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "RE",
                table: "RE_COUNTRY",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "RE_SUFFIX",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "RE_SUFFIX",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "RE_RELIGION",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "RE_RELIGION",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "RE_RACE",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "RE_RACE",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "RE_GENDER",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "RE_GENDER",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "RE_ETHNIC",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "RE_ETHNIC",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "PERSON",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "PERSON",
                newName: "last_modified_by");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "PERSON",
                table: "BIRTH",
                newName: "last_modified_date");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                schema: "PERSON",
                table: "BIRTH",
                newName: "last_modified_by");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "RE",
                table: "RE_COUNTRY",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "RE",
                table: "RE_COUNTRY",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "RE_SUFFIX",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "RE_SUFFIX",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "RE_RELIGION",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "RE_RELIGION",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "RE_RACE",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "RE_RACE",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "RE_GENDER",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "RE_GENDER",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "RE_ETHNIC",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "RE_ETHNIC",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "PERSON",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "PERSON",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "last_modified_date",
                schema: "PERSON",
                table: "BIRTH",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "last_modified_by",
                schema: "PERSON",
                table: "BIRTH",
                newName: "modified_by");
        }
    }
}
