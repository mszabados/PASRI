using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RE_BLOOD_TYPE",
                columns: table => new
                {
                    blood_type_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(3)", nullable: false),
                    description = table.Column<string>(type: "varchar(11)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_BLOOD_TYPE", x => x.blood_type_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_COUNTRY",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(44)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_COUNTRY", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_ETHNIC",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(28)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_ETHNIC", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_EYE_COLOR",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(14)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_EYE_COLOR", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_GENDER",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    Description = table.Column<string>(type: "varchar(7)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_GENDER", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_HAIR_COLOR",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(6)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_HAIR_COLOR", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_RACE",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    Description = table.Column<string>(type: "varchar(237)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_RACE", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_RELIGION",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(253)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_RELIGION", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_STATE",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    Description = table.Column<string>(type: "varchar(35)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_STATE", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_SUFFIX",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(6)", nullable: false),
                    Description = table.Column<string>(type: "varchar(15)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_SUFFIX", x => x.country_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RE_BLOOD_TYPE_code",
                table: "RE_BLOOD_TYPE",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_COUNTRY_code",
                table: "RE_COUNTRY",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_ETHNIC_code",
                table: "RE_ETHNIC",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_EYE_COLOR_code",
                table: "RE_EYE_COLOR",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_GENDER_code",
                table: "RE_GENDER",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_HAIR_COLOR_code",
                table: "RE_HAIR_COLOR",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_RACE_code",
                table: "RE_RACE",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_RELIGION_code",
                table: "RE_RELIGION",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_STATE_code",
                table: "RE_STATE",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_SUFFIX_code",
                table: "RE_SUFFIX",
                column: "code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RE_BLOOD_TYPE");

            migrationBuilder.DropTable(
                name: "RE_COUNTRY");

            migrationBuilder.DropTable(
                name: "RE_ETHNIC");

            migrationBuilder.DropTable(
                name: "RE_EYE_COLOR");

            migrationBuilder.DropTable(
                name: "RE_GENDER");

            migrationBuilder.DropTable(
                name: "RE_HAIR_COLOR");

            migrationBuilder.DropTable(
                name: "RE_RACE");

            migrationBuilder.DropTable(
                name: "RE_RELIGION");

            migrationBuilder.DropTable(
                name: "RE_STATE");

            migrationBuilder.DropTable(
                name: "RE_SUFFIX");
        }
    }
}
