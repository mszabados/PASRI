using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class CreateREAndPERSONSchemasAndMigrateTablesToSchemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_BIRTH_RE_COUNTRY_country_id",
                "BIRTH");

            migrationBuilder.DropPrimaryKey(
                "PK_RE_COUNTRY",
                "RE_COUNTRY");

            migrationBuilder.EnsureSchema(
                "PERSON");

            migrationBuilder.EnsureSchema(
                "RE");

            migrationBuilder.RenameTable(
                "RE_SUFFIX",
                newName: "RE_SUFFIX",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "RE_STATE",
                newName: "RE_STATE",
                newSchema: "RE");

            migrationBuilder.RenameTable(
                "RE_RELIGION",
                newName: "RE_RELIGION",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "RE_RACE",
                newName: "RE_RACE",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "RE_HAIR_COLOR",
                newName: "RE_HAIR_COLOR",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "RE_GENDER",
                newName: "RE_GENDER",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "RE_EYE_COLOR",
                newName: "RE_EYE_COLOR",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "RE_ETHNIC",
                newName: "RE_ETHNIC",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "RE_BLOOD_TYPE",
                newName: "RE_BLOOD_TYPE",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "PERSON",
                newName: "PERSON",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "BIRTH",
                newName: "BIRTH",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                "RE_COUNTRY",
                newName: "COUNTRY",
                newSchema: "RE");

            migrationBuilder.RenameIndex(
                "IX_RE_COUNTRY_code",
                schema: "RE",
                table: "COUNTRY",
                newName: "IX_COUNTRY_code");

            migrationBuilder.AddPrimaryKey(
                "PK_COUNTRY",
                schema: "RE",
                table: "COUNTRY",
                column: "country_id");

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_COUNTRY_country_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "country_id",
                principalSchema: "RE",
                principalTable: "COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_BIRTH_COUNTRY_country_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropPrimaryKey(
                "PK_COUNTRY",
                schema: "RE",
                table: "COUNTRY");

            migrationBuilder.RenameTable(
                "RE_STATE",
                "RE",
                "RE_STATE");

            migrationBuilder.RenameTable(
                "RE_SUFFIX",
                "PERSON",
                "RE_SUFFIX");

            migrationBuilder.RenameTable(
                "RE_RELIGION",
                "PERSON",
                "RE_RELIGION");

            migrationBuilder.RenameTable(
                "RE_RACE",
                "PERSON",
                "RE_RACE");

            migrationBuilder.RenameTable(
                "RE_HAIR_COLOR",
                "PERSON",
                "RE_HAIR_COLOR");

            migrationBuilder.RenameTable(
                "RE_GENDER",
                "PERSON",
                "RE_GENDER");

            migrationBuilder.RenameTable(
                "RE_EYE_COLOR",
                "PERSON",
                "RE_EYE_COLOR");

            migrationBuilder.RenameTable(
                "RE_ETHNIC",
                "PERSON",
                "RE_ETHNIC");

            migrationBuilder.RenameTable(
                "RE_BLOOD_TYPE",
                "PERSON",
                "RE_BLOOD_TYPE");

            migrationBuilder.RenameTable(
                "PERSON",
                "PERSON",
                "PERSON");

            migrationBuilder.RenameTable(
                "BIRTH",
                "PERSON",
                "BIRTH");

            migrationBuilder.RenameTable(
                "COUNTRY",
                "RE",
                "RE_COUNTRY");

            migrationBuilder.RenameIndex(
                "IX_COUNTRY_code",
                table: "RE_COUNTRY",
                newName: "IX_RE_COUNTRY_code");

            migrationBuilder.AddPrimaryKey(
                "PK_RE_COUNTRY",
                "RE_COUNTRY",
                "country_id");

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_RE_COUNTRY_country_id",
                "BIRTH",
                "country_id",
                "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
