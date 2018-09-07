using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class AddIdToReferenceBloodTypeAndSetAsPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ReferenceTypeBlood",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ReferenceTypeBlood");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood",
                column: "Code");
        }
    }
}
