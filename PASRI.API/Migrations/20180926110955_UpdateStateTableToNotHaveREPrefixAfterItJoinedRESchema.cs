using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class UpdateStateTableToNotHaveREPrefixAfterItJoinedRESchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_BIRTH_RE_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropPrimaryKey(
                "PK_RE_STATE",
                schema: "RE",
                table: "RE_STATE");

            migrationBuilder.RenameTable(
                "RE_STATE",
                "RE",
                "STATE",
                "RE");

            migrationBuilder.RenameIndex(
                "IX_RE_STATE_code",
                schema: "RE",
                table: "STATE",
                newName: "IX_STATE_code");

            migrationBuilder.AddPrimaryKey(
                "PK_STATE",
                schema: "RE",
                table: "STATE",
                column: "state_id");

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_STATE_state_province_id",
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
                "FK_BIRTH_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropPrimaryKey(
                "PK_STATE",
                schema: "RE",
                table: "STATE");

            migrationBuilder.RenameTable(
                "STATE",
                "RE",
                "RE_STATE",
                "RE");

            migrationBuilder.RenameIndex(
                "IX_STATE_code",
                schema: "RE",
                table: "RE_STATE",
                newName: "IX_RE_STATE_code");

            migrationBuilder.AddPrimaryKey(
                "PK_RE_STATE",
                schema: "RE",
                table: "RE_STATE",
                column: "state_id");

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_RE_STATE_state_province_id",
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
