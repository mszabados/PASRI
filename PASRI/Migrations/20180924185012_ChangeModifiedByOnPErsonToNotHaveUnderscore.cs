using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class ChangeModifiedByOnPErsonToNotHaveUnderscore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "PERSON",
                newName: "modifiedby");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modifiedby",
                table: "PERSON",
                newName: "modified_by");
        }
    }
}
