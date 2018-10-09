using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRC.DB.Master.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "PERSON");

            migrationBuilder.EnsureSchema(
                name: "RE");

            migrationBuilder.CreateTable(
                name: "RE_BLOOD_TYPE",
                schema: "PERSON",
                columns: table => new
                {
                    blood_type_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(3)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(11)", nullable: false),
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
                name: "RE_ETHNIC",
                schema: "PERSON",
                columns: table => new
                {
                    ethnic_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(28)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_ETHNIC", x => x.ethnic_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_EYE_COLOR",
                schema: "PERSON",
                columns: table => new
                {
                    eye_color_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(14)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_EYE_COLOR", x => x.eye_color_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_GENDER",
                schema: "PERSON",
                columns: table => new
                {
                    gender_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(7)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_GENDER", x => x.gender_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_HAIR_COLOR",
                schema: "PERSON",
                columns: table => new
                {
                    hair_color_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(6)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_HAIR_COLOR", x => x.hair_color_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_RACE",
                schema: "PERSON",
                columns: table => new
                {
                    race_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(237)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_RACE", x => x.race_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_RELIGION",
                schema: "PERSON",
                columns: table => new
                {
                    religion_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(253)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_RELIGION", x => x.religion_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_SUFFIX",
                schema: "PERSON",
                columns: table => new
                {
                    suffix_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(6)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(15)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_SUFFIX", x => x.suffix_id);
                });

            migrationBuilder.CreateTable(
                name: "COUNTRY",
                schema: "RE",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(44)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUNTRY", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "STATE",
                schema: "RE",
                columns: table => new
                {
                    state_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(35)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STATE", x => x.state_id);
                });

            migrationBuilder.CreateTable(
                name: "PERSON",
                schema: "PERSON",
                columns: table => new
                {
                    person_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "varchar(80)", nullable: false),
                    middle_name = table.Column<string>(type: "varchar(80)", nullable: true),
                    last_name = table.Column<string>(type: "varchar(80)", nullable: false),
                    suffix_id = table.Column<int>(nullable: true),
                    effect_date = table.Column<DateTime>(type: "date", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSON", x => x.person_id);
                    table.ForeignKey(
                        name: "FK_PERSON_RE_SUFFIX_suffix_id",
                        column: x => x.suffix_id,
                        principalSchema: "PERSON",
                        principalTable: "RE_SUFFIX",
                        principalColumn: "suffix_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BIRTH",
                schema: "PERSON",
                columns: table => new
                {
                    birth_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    person_id = table.Column<int>(nullable: false),
                    birth_date = table.Column<DateTime>(type: "date", nullable: true),
                    birth_city = table.Column<string>(type: "varchar(100)", nullable: true),
                    state_province_id = table.Column<int>(type: "int", nullable: true),
                    country_id = table.Column<int>(type: "int", nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BIRTH", x => x.birth_id);
                    table.ForeignKey(
                        name: "FK_BIRTH_COUNTRY_country_id",
                        column: x => x.country_id,
                        principalSchema: "RE",
                        principalTable: "COUNTRY",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_BIRTH_PERSON_person_id",
                        column: x => x.person_id,
                        principalSchema: "PERSON",
                        principalTable: "PERSON",
                        principalColumn: "person_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BIRTH_STATE_state_province_id",
                        column: x => x.state_province_id,
                        principalSchema: "RE",
                        principalTable: "STATE",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BIRTH_country_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_BIRTH_person_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BIRTH_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "state_province_id");

            migrationBuilder.CreateIndex(
                name: "IX_PERSON_suffix_id",
                schema: "PERSON",
                table: "PERSON",
                column: "suffix_id");

            migrationBuilder.CreateIndex(
                name: "IX_RE_BLOOD_TYPE_code",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_ETHNIC_code",
                schema: "PERSON",
                table: "RE_ETHNIC",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_EYE_COLOR_code",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_GENDER_code",
                schema: "PERSON",
                table: "RE_GENDER",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_HAIR_COLOR_code",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_RACE_code",
                schema: "PERSON",
                table: "RE_RACE",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_RELIGION_code",
                schema: "PERSON",
                table: "RE_RELIGION",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_SUFFIX_code",
                schema: "PERSON",
                table: "RE_SUFFIX",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_COUNTRY_code",
                schema: "RE",
                table: "COUNTRY",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STATE_code",
                schema: "RE",
                table: "STATE",
                column: "code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BIRTH",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_BLOOD_TYPE",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_ETHNIC",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_EYE_COLOR",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_GENDER",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_HAIR_COLOR",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_RACE",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_RELIGION",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "COUNTRY",
                schema: "RE");

            migrationBuilder.DropTable(
                name: "PERSON",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "STATE",
                schema: "RE");

            migrationBuilder.DropTable(
                name: "RE_SUFFIX",
                schema: "PERSON");
        }
    }
}
