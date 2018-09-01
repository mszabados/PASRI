using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class RemoveKeysRelatedToNVarcharCodesInReferenceTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonLegalNameIdentification_ReferenceSuffixName_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLegalNameIdentification_ReferenceSuffixName_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                column: "ReferenceSuffixNameCode",
                principalTable: "ReferenceSuffixName",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonLegalNameIdentification_ReferenceSuffixName_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLegalNameIdentification_ReferenceSuffixName_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                column: "ReferenceSuffixNameCode",
                principalTable: "ReferenceSuffixName",
                principalColumn: "Code",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
