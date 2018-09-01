using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class AddPersonIdentificationNameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonIdentificationName",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonIdentificationId = table.Column<int>(nullable: false),
                    First = table.Column<string>(maxLength: 255, nullable: true),
                    Middle = table.Column<string>(maxLength: 255, nullable: true),
                    Last = table.Column<string>(maxLength: 255, nullable: true),
                    Full = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonIdentificationName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonIdentificationName_PersonIdentification_PersonIdentificationId",
                        column: x => x.PersonIdentificationId,
                        principalTable: "PersonIdentification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentificationName_PersonIdentificationId",
                table: "PersonIdentificationName",
                column: "PersonIdentificationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonIdentificationName");
        }
    }
}
