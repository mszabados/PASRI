using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class UpdateStateTableToNotHaveREPrefixAfterItJoinedRESchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RE_STATE",
                schema: "RE",
                table: "RE_STATE");

            migrationBuilder.RenameTable(
                name: "RE_STATE",
                schema: "RE",
                newName: "STATE",
                newSchema: "RE");

            migrationBuilder.RenameIndex(
                name: "IX_RE_STATE_code",
                schema: "RE",
                table: "STATE",
                newName: "IX_STATE_code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_STATE",
                schema: "RE",
                table: "STATE",
                column: "state_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "state_province_id",
                principalSchema: "RE",
                principalTable: "STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_STATE",
                schema: "RE",
                table: "STATE");

            migrationBuilder.RenameTable(
                name: "STATE",
                schema: "RE",
                newName: "RE_STATE",
                newSchema: "RE");

            migrationBuilder.RenameIndex(
                name: "IX_STATE_code",
                schema: "RE",
                table: "RE_STATE",
                newName: "IX_RE_STATE_code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RE_STATE",
                schema: "RE",
                table: "RE_STATE",
                column: "state_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "state_province_id",
                principalSchema: "RE",
                principalTable: "RE_STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
