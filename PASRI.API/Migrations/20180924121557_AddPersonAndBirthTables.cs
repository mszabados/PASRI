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
                "PERSON",
                table => new
                {
                    person_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>("varchar(255)"),
                    middle_name = table.Column<string>("varchar(255)", nullable: true),
                    last_name = table.Column<string>("varchar(255)"),
                    suffix_id = table.Column<int>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON", x => x.person_id);
                    table.ForeignKey(
                        "FK_PERSON_RE_SUFFIX_suffix_id",
                        x => x.suffix_id,
                        "RE_SUFFIX",
                        "country_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                "BIRTH",
                table => new
                {
                    birth_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    person_id = table.Column<int>(),
                    birth_date = table.Column<DateTime>("date"),
                    birth_city = table.Column<string>("varchar(100)"),
                    country_id = table.Column<int>("int"),
                    CountryId = table.Column<int>(),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIRTH", x => x.birth_id);
                    table.ForeignKey(
                        "FK_BIRTH_RE_COUNTRY_CountryId",
                        x => x.CountryId,
                        "RE_COUNTRY",
                        "country_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_BIRTH_PERSON_person_id",
                        x => x.person_id,
                        "PERSON",
                        "person_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_BIRTH_RE_STATE_country_id",
                        x => x.country_id,
                        "RE_STATE",
                        "country_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_BIRTH_CountryId",
                "BIRTH",
                "CountryId");

            migrationBuilder.CreateIndex(
                "IX_BIRTH_person_id",
                "BIRTH",
                "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_BIRTH_country_id",
                "BIRTH",
                "country_id");

            migrationBuilder.CreateIndex(
                "IX_PERSON_suffix_id",
                "PERSON",
                "suffix_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BIRTH");

            migrationBuilder.DropTable(
                "PERSON");
        }
    }
}
