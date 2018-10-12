using Microsoft.EntityFrameworkCore.Migrations;

namespace HRC.DB.Master.Migrations
{
    public partial class ChangedSchemaOnSsnVerificationToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "RE_SSN_VERIFICATION",
                schema: "RE",
                newName: "RE_SSN_VERIFICATION",
                newSchema: "PERSON");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "RE_SSN_VERIFICATION",
                schema: "PERSON",
                newName: "RE_SSN_VERIFICATION",
                newSchema: "RE");
        }
    }
}
