using Microsoft.EntityFrameworkCore.Migrations;

namespace HRC.DB.Master.Migrations
{
    public partial class ChangedNomenclatureForReferenceStateProvinceAndReferenceCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_COUNTRY_country_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_STATE_COUNTRY_country_id",
                schema: "RE",
                table: "STATE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STATE",
                schema: "RE",
                table: "STATE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_COUNTRY",
                schema: "RE",
                table: "COUNTRY");

            migrationBuilder.RenameTable(
                name: "STATE",
                schema: "RE",
                newName: "RE_STATE_PROVINCE",
                newSchema: "RE");

            migrationBuilder.RenameTable(
                name: "COUNTRY",
                schema: "RE",
                newName: "RE_COUNTRY",
                newSchema: "RE");

            migrationBuilder.RenameColumn(
                name: "state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                newName: "re_state_province_id");

            migrationBuilder.RenameColumn(
                name: "country_id",
                schema: "PERSON",
                table: "BIRTH",
                newName: "re_country_id");

            migrationBuilder.RenameIndex(
                name: "IX_BIRTH_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                newName: "IX_BIRTH_re_state_province_id");

            migrationBuilder.RenameIndex(
                name: "IX_BIRTH_country_id",
                schema: "PERSON",
                table: "BIRTH",
                newName: "IX_BIRTH_re_country_id");

            migrationBuilder.RenameColumn(
                name: "country_id",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                newName: "re_country_id");

            migrationBuilder.RenameColumn(
                name: "state_id",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                newName: "re_state_province_id");

            migrationBuilder.RenameIndex(
                name: "IX_STATE_country_id",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                newName: "IX_RE_STATE_PROVINCE_re_country_id");

            migrationBuilder.RenameIndex(
                name: "IX_STATE_code",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                newName: "IX_RE_STATE_PROVINCE_code");

            migrationBuilder.RenameColumn(
                name: "country_id",
                schema: "RE",
                table: "RE_COUNTRY",
                newName: "re_country_id");

            migrationBuilder.RenameIndex(
                name: "IX_COUNTRY_code",
                schema: "RE",
                table: "RE_COUNTRY",
                newName: "IX_RE_COUNTRY_code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RE_STATE_PROVINCE",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                column: "re_state_province_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RE_COUNTRY",
                schema: "RE",
                table: "RE_COUNTRY",
                column: "re_country_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_re_country_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "re_country_id",
                principalSchema: "RE",
                principalTable: "RE_COUNTRY",
                principalColumn: "re_country_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_STATE_PROVINCE_re_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "re_state_province_id",
                principalSchema: "RE",
                principalTable: "RE_STATE_PROVINCE",
                principalColumn: "re_state_province_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RE_STATE_PROVINCE_RE_COUNTRY_re_country_id",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                column: "re_country_id",
                principalSchema: "RE",
                principalTable: "RE_COUNTRY",
                principalColumn: "re_country_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_re_country_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_STATE_PROVINCE_re_state_province_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_RE_STATE_PROVINCE_RE_COUNTRY_re_country_id",
                schema: "RE",
                table: "RE_STATE_PROVINCE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RE_STATE_PROVINCE",
                schema: "RE",
                table: "RE_STATE_PROVINCE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RE_COUNTRY",
                schema: "RE",
                table: "RE_COUNTRY");

            migrationBuilder.RenameTable(
                name: "RE_STATE_PROVINCE",
                schema: "RE",
                newName: "STATE",
                newSchema: "RE");

            migrationBuilder.RenameTable(
                name: "RE_COUNTRY",
                schema: "RE",
                newName: "COUNTRY",
                newSchema: "RE");

            migrationBuilder.RenameColumn(
                name: "re_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                newName: "state_province_id");

            migrationBuilder.RenameColumn(
                name: "re_country_id",
                schema: "PERSON",
                table: "BIRTH",
                newName: "country_id");

            migrationBuilder.RenameIndex(
                name: "IX_BIRTH_re_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                newName: "IX_BIRTH_state_province_id");

            migrationBuilder.RenameIndex(
                name: "IX_BIRTH_re_country_id",
                schema: "PERSON",
                table: "BIRTH",
                newName: "IX_BIRTH_country_id");

            migrationBuilder.RenameColumn(
                name: "re_country_id",
                schema: "RE",
                table: "STATE",
                newName: "country_id");

            migrationBuilder.RenameColumn(
                name: "re_state_province_id",
                schema: "RE",
                table: "STATE",
                newName: "state_id");

            migrationBuilder.RenameIndex(
                name: "IX_RE_STATE_PROVINCE_re_country_id",
                schema: "RE",
                table: "STATE",
                newName: "IX_STATE_country_id");

            migrationBuilder.RenameIndex(
                name: "IX_RE_STATE_PROVINCE_code",
                schema: "RE",
                table: "STATE",
                newName: "IX_STATE_code");

            migrationBuilder.RenameColumn(
                name: "re_country_id",
                schema: "RE",
                table: "COUNTRY",
                newName: "country_id");

            migrationBuilder.RenameIndex(
                name: "IX_RE_COUNTRY_code",
                schema: "RE",
                table: "COUNTRY",
                newName: "IX_COUNTRY_code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STATE",
                schema: "RE",
                table: "STATE",
                column: "state_id");

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
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "state_province_id",
                principalSchema: "RE",
                principalTable: "STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_STATE_COUNTRY_country_id",
                schema: "RE",
                table: "STATE",
                column: "country_id",
                principalSchema: "RE",
                principalTable: "COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
