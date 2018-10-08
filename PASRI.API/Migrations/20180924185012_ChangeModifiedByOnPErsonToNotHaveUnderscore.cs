using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class ChangeModifiedByOnPErsonToNotHaveUnderscore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "modified_by",
                "PERSON",
                "modifiedby");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "modifiedby",
                "PERSON",
                "modified_by");
        }
    }
}
