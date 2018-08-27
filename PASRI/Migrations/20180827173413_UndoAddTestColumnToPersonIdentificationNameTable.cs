using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.Migrations
{
    public partial class UndoAddTestColumnToPersonIdentificationNameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "PersonIdentification");

            migrationBuilder.AlterColumn<string>(
                name: "First",
                table: "PersonIdentificationName",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "First",
                table: "PersonIdentificationName",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "PersonIdentification",
                nullable: true);
        }
    }
}
