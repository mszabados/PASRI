using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class LengthenReferenceBloodTypeCodeLengthToTwoCharacters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceTypeBlood",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceTypeBlood",
                type: "char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");
        }
    }
}
