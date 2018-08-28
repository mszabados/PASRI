using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.Migrations
{
    public partial class CreateForeignKeyForLegalNameIdentificationToReferenceSuffixName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                maxLength: 4,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonLegalNameIdentification_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                column: "ReferenceSuffixNameCode",
                unique: true,
                filter: "[ReferenceSuffixNameCode] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonLegalNameIdentification_ReferenceSuffixName_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                column: "ReferenceSuffixNameCode",
                principalTable: "ReferenceSuffixName",
                principalColumn: "Code",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonLegalNameIdentification_ReferenceSuffixName_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification");

            migrationBuilder.DropIndex(
                name: "IX_PersonLegalNameIdentification_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification");

            migrationBuilder.DropColumn(
                name: "ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification");
        }
    }
}
