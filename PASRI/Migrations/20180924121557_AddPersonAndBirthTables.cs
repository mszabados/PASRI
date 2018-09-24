using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class AddPersonAndBirthTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PERSON",
                columns: table => new
                {
                    person_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    middle_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    last_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    suffix_id = table.Column<int>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON", x => x.person_id);
                    table.ForeignKey(
                        name: "FK_PERSON_RE_SUFFIX_suffix_id",
                        column: x => x.suffix_id,
                        principalTable: "RE_SUFFIX",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BIRTH",
                columns: table => new
                {
                    birth_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    person_id = table.Column<int>(nullable: false),
                    birth_date = table.Column<DateTime>(type: "date", nullable: false),
                    birth_city = table.Column<string>(type: "varchar(100)", nullable: false),
                    country_id = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIRTH", x => x.birth_id);
                    table.ForeignKey(
                        name: "FK_BIRTH_RE_COUNTRY_CountryId",
                        column: x => x.CountryId,
                        principalTable: "RE_COUNTRY",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BIRTH_PERSON_person_id",
                        column: x => x.person_id,
                        principalTable: "PERSON",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BIRTH_RE_STATE_country_id",
                        column: x => x.country_id,
                        principalTable: "RE_STATE",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BIRTH_CountryId",
                table: "BIRTH",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_BIRTH_person_id",
                table: "BIRTH",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BIRTH_country_id",
                table: "BIRTH",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_suffix_id",
                table: "PERSON",
                column: "suffix_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BIRTH");

            migrationBuilder.DropTable(
                name: "PERSON");
        }
    }
}
