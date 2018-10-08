using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class ChangedModifiedByOnPersonToHaveUnderscore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "modifiedby",
                "PERSON",
                "modified_by");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "modified_by",
                "PERSON",
                "modifiedby");
        }
    }
}
