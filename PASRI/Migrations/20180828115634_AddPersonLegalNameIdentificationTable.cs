using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.Migrations
{
    public partial class AddPersonLegalNameIdentificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonIdentificationName");

            migrationBuilder.CreateTable(
                name: "PersonNameIdentification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonIdentificationId = table.Column<int>(nullable: false),
                    DoDServicePersonDocumentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNameIdentification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonNameIdentification_PersonIdentification_PersonIdentificationId",
                        column: x => x.PersonIdentificationId,
                        principalTable: "PersonIdentification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonLegalNameIdentification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonNameIdentificationId = table.Column<int>(nullable: false),
                    First = table.Column<string>(nullable: true),
                    Middle = table.Column<string>(nullable: true),
                    Last = table.Column<string>(nullable: true),
                    Full = table.Column<string>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLegalNameIdentification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonLegalNameIdentification_PersonNameIdentification_PersonNameIdentificationId",
                        column: x => x.PersonNameIdentificationId,
                        principalTable: "PersonNameIdentification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonLegalNameIdentification_PersonNameIdentificationId",
                table: "PersonLegalNameIdentification",
                column: "PersonNameIdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNameIdentification_PersonIdentificationId",
                table: "PersonNameIdentification",
                column: "PersonIdentificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonLegalNameIdentification");

            migrationBuilder.DropTable(
                name: "PersonNameIdentification");

            migrationBuilder.CreateTable(
                name: "PersonIdentificationName",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    First = table.Column<string>(maxLength: 255, nullable: true),
                    Full = table.Column<string>(maxLength: 255, nullable: true),
                    Last = table.Column<string>(maxLength: 255, nullable: true),
                    Middle = table.Column<string>(maxLength: 255, nullable: true),
                    PersonIdentificationId = table.Column<int>(nullable: false)
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
    }
}
