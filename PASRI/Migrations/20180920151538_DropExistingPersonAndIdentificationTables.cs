using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class DropExistingPersonAndIdentificationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonLegalNameIdentification");

            migrationBuilder.DropTable(
                name: "PersonNameIdentification");

            migrationBuilder.DropTable(
                name: "PersonIdentification");

            migrationBuilder.DropTable(
                name: "Person");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonIdentification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonIdentification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonIdentification_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonNameIdentification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoDServicePersonDocumentID = table.Column<int>(nullable: false),
                    PersonIdentificationId = table.Column<int>(nullable: false)
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
                    EffectiveDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    First = table.Column<string>(type: "varchar(255)", nullable: true),
                    Full = table.Column<string>(type: "varchar(255)", nullable: true),
                    Last = table.Column<string>(type: "varchar(255)", nullable: true),
                    Middle = table.Column<string>(type: "varchar(255)", nullable: true),
                    PersonNameIdentificationId = table.Column<int>(nullable: false),
                    ReferenceSuffixNameCode = table.Column<string>(type: "char(4)", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_PersonLegalNameIdentification_ReferenceSuffixName_ReferenceSuffixNameCode",
                        column: x => x.ReferenceSuffixNameCode,
                        principalTable: "ReferenceSuffixName",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonIdentification_PersonId",
                table: "PersonIdentification",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonLegalNameIdentification_PersonNameIdentificationId",
                table: "PersonLegalNameIdentification",
                column: "PersonNameIdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonLegalNameIdentification_ReferenceSuffixNameCode",
                table: "PersonLegalNameIdentification",
                column: "ReferenceSuffixNameCode",
                unique: true,
                filter: "[ReferenceSuffixNameCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNameIdentification_PersonIdentificationId",
                table: "PersonNameIdentification",
                column: "PersonIdentificationId");
        }
    }
}
