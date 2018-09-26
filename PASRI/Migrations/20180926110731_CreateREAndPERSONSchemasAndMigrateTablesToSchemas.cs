using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class CreateREAndPERSONSchemasAndMigrateTablesToSchemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_country_id",
                table: "BIRTH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RE_COUNTRY",
                table: "RE_COUNTRY");

            migrationBuilder.EnsureSchema(
                name: "PERSON");

            migrationBuilder.EnsureSchema(
                name: "RE");

            migrationBuilder.RenameTable(
                name: "RE_SUFFIX",
                newName: "RE_SUFFIX",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "RE_STATE",
                newName: "RE_STATE",
                newSchema: "RE");

            migrationBuilder.RenameTable(
                name: "RE_RELIGION",
                newName: "RE_RELIGION",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "RE_RACE",
                newName: "RE_RACE",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "RE_HAIR_COLOR",
                newName: "RE_HAIR_COLOR",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "RE_GENDER",
                newName: "RE_GENDER",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "RE_EYE_COLOR",
                newName: "RE_EYE_COLOR",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "RE_ETHNIC",
                newName: "RE_ETHNIC",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "RE_BLOOD_TYPE",
                newName: "RE_BLOOD_TYPE",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "PERSON",
                newName: "PERSON",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "BIRTH",
                newName: "BIRTH",
                newSchema: "PERSON");

            migrationBuilder.RenameTable(
                name: "RE_COUNTRY",
                newName: "COUNTRY",
                newSchema: "RE");

            migrationBuilder.RenameIndex(
                name: "IX_RE_COUNTRY_code",
                schema: "RE",
                table: "COUNTRY",
                newName: "IX_COUNTRY_code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_COUNTRY",
                schema: "RE",
                table: "COUNTRY",
                column: "country_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_COUNTRY_country_id",
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
                name: "FK_BIRTH_COUNTRY_country_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COUNTRY",
                schema: "RE",
                table: "COUNTRY");

            migrationBuilder.RenameTable(
                name: "RE_STATE",
                schema: "RE",
                newName: "RE_STATE");

            migrationBuilder.RenameTable(
                name: "RE_SUFFIX",
                schema: "PERSON",
                newName: "RE_SUFFIX");

            migrationBuilder.RenameTable(
                name: "RE_RELIGION",
                schema: "PERSON",
                newName: "RE_RELIGION");

            migrationBuilder.RenameTable(
                name: "RE_RACE",
                schema: "PERSON",
                newName: "RE_RACE");

            migrationBuilder.RenameTable(
                name: "RE_HAIR_COLOR",
                schema: "PERSON",
                newName: "RE_HAIR_COLOR");

            migrationBuilder.RenameTable(
                name: "RE_GENDER",
                schema: "PERSON",
                newName: "RE_GENDER");

            migrationBuilder.RenameTable(
                name: "RE_EYE_COLOR",
                schema: "PERSON",
                newName: "RE_EYE_COLOR");

            migrationBuilder.RenameTable(
                name: "RE_ETHNIC",
                schema: "PERSON",
                newName: "RE_ETHNIC");

            migrationBuilder.RenameTable(
                name: "RE_BLOOD_TYPE",
                schema: "PERSON",
                newName: "RE_BLOOD_TYPE");

            migrationBuilder.RenameTable(
                name: "PERSON",
                schema: "PERSON",
                newName: "PERSON");

            migrationBuilder.RenameTable(
                name: "BIRTH",
                schema: "PERSON",
                newName: "BIRTH");

            migrationBuilder.RenameTable(
                name: "COUNTRY",
                schema: "RE",
                newName: "RE_COUNTRY");

            migrationBuilder.RenameIndex(
                name: "IX_COUNTRY_code",
                table: "RE_COUNTRY",
                newName: "IX_RE_COUNTRY_code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RE_COUNTRY",
                table: "RE_COUNTRY",
                column: "country_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_country_id",
                table: "BIRTH",
                column: "country_id",
                principalTable: "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
