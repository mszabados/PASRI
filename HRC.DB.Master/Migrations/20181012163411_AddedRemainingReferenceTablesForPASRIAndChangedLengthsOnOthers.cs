using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRC.DB.Master.Migrations
{
    public partial class AddedRemainingReferenceTablesForPASRIAndChangedLengthsOnOthers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(35)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                type: "varchar(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "RE",
                table: "RE_COUNTRY",
                type: "varchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(44)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "PERSON",
                table: "RE_SUFFIX",
                type: "varchar(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(6)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_RELIGION",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(253)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_RACE",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(237)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(14)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_ETHNIC",
                type: "varchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(28)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(11)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                type: "char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(3)");

            migrationBuilder.CreateTable(
                name: "RE_ACCESSION_SOURCE",
                schema: "PERSON",
                columns: table => new
                {
                    re_accession_source_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(210)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_ACCESSION_SOURCE", x => x.re_accession_source_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_BASIS_CITIZEN",
                schema: "PERSON",
                columns: table => new
                {
                    re_basis_citizen_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_BASIS_CITIZEN", x => x.re_basis_citizen_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_CITZ_QUAL_MIL_SVC",
                schema: "PERSON",
                columns: table => new
                {
                    re_citz_qual_mil_svc_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(80)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_CITZ_QUAL_MIL_SVC", x => x.re_citz_qual_mil_svc_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_MARRIAGE",
                schema: "PERSON",
                columns: table => new
                {
                    re_marriage_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(20)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_MARRIAGE", x => x.re_marriage_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_PERSONNEL_CLASS",
                schema: "PERSON",
                columns: table => new
                {
                    re_personnel_class_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(2)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(60)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_PERSONNEL_CLASS", x => x.re_personnel_class_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_RANK",
                schema: "PERSON",
                columns: table => new
                {
                    re_rank_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(3)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(30)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_RANK", x => x.re_rank_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_SERVICE_BRANCH",
                schema: "PERSON",
                columns: table => new
                {
                    re_service_branch_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(70)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_SERVICE_BRANCH", x => x.re_service_branch_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_SSN_VERIFICATION",
                schema: "RE",
                columns: table => new
                {
                    re_ssn_verification_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(50)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_SSN_VERIFICATION", x => x.re_ssn_verification_id);
                });

            migrationBuilder.CreateTable(
                name: "RE_PAY_PLAN",
                schema: "PERSON",
                columns: table => new
                {
                    re_pay_plan_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    re_personnel_class_id = table.Column<int>(nullable: false),
                    code = table.Column<string>(type: "char(4)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(10)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_PAY_PLAN", x => x.re_pay_plan_id);
                    table.ForeignKey(
                        name: "FK_RE_PAY_PLAN_RE_PERSONNEL_CLASS_re_personnel_class_id",
                        column: x => x.re_personnel_class_id,
                        principalSchema: "PERSON",
                        principalTable: "RE_PERSONNEL_CLASS",
                        principalColumn: "re_personnel_class_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RE_BRANCH_COMPONENT",
                schema: "PERSON",
                columns: table => new
                {
                    re_branch_component_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    re_service_branch_id = table.Column<int>(nullable: false),
                    code = table.Column<string>(type: "char(1)", nullable: false),
                    long_name = table.Column<string>(type: "varchar(30)", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by = table.Column<string>(type: "varchar(30)", nullable: true),
                    last_modified_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    last_modified_by = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_BRANCH_COMPONENT", x => x.re_branch_component_id);
                    table.ForeignKey(
                        name: "FK_RE_BRANCH_COMPONENT_RE_SERVICE_BRANCH_re_service_branch_id",
                        column: x => x.re_service_branch_id,
                        principalSchema: "PERSON",
                        principalTable: "RE_SERVICE_BRANCH",
                        principalColumn: "re_service_branch_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RE_ACCESSION_SOURCE_code",
                schema: "PERSON",
                table: "RE_ACCESSION_SOURCE",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_BASIS_CITIZEN_code",
                schema: "PERSON",
                table: "RE_BASIS_CITIZEN",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_BRANCH_COMPONENT_code",
                schema: "PERSON",
                table: "RE_BRANCH_COMPONENT",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_BRANCH_COMPONENT_re_service_branch_id",
                schema: "PERSON",
                table: "RE_BRANCH_COMPONENT",
                column: "re_service_branch_id");

            migrationBuilder.CreateIndex(
                name: "IX_RE_CITZ_QUAL_MIL_SVC_code",
                schema: "PERSON",
                table: "RE_CITZ_QUAL_MIL_SVC",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_MARRIAGE_code",
                schema: "PERSON",
                table: "RE_MARRIAGE",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_PAY_PLAN_code",
                schema: "PERSON",
                table: "RE_PAY_PLAN",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_PAY_PLAN_re_personnel_class_id",
                schema: "PERSON",
                table: "RE_PAY_PLAN",
                column: "re_personnel_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_RE_PERSONNEL_CLASS_code",
                schema: "PERSON",
                table: "RE_PERSONNEL_CLASS",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_RANK_code",
                schema: "PERSON",
                table: "RE_RANK",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_SERVICE_BRANCH_code",
                schema: "PERSON",
                table: "RE_SERVICE_BRANCH",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RE_SSN_VERIFICATION_code",
                schema: "RE",
                table: "RE_SSN_VERIFICATION",
                column: "code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RE_ACCESSION_SOURCE",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_BASIS_CITIZEN",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_BRANCH_COMPONENT",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_CITZ_QUAL_MIL_SVC",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_MARRIAGE",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_PAY_PLAN",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_RANK",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_SSN_VERIFICATION",
                schema: "RE");

            migrationBuilder.DropTable(
                name: "RE_SERVICE_BRANCH",
                schema: "PERSON");

            migrationBuilder.DropTable(
                name: "RE_PERSONNEL_CLASS",
                schema: "PERSON");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                type: "varchar(35)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "RE",
                table: "RE_STATE_PROVINCE",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(3)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "RE",
                table: "RE_COUNTRY",
                type: "varchar(44)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "PERSON",
                table: "RE_SUFFIX",
                type: "char(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(6)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_RELIGION",
                type: "varchar(253)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_RACE",
                type: "varchar(237)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_HAIR_COLOR",
                type: "varchar(6)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_EYE_COLOR",
                type: "varchar(14)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_ETHNIC",
                type: "varchar(28)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)");

            migrationBuilder.AlterColumn<string>(
                name: "long_name",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                type: "varchar(11)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                schema: "PERSON",
                table: "RE_BLOOD_TYPE",
                type: "char(3)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)");
        }
    }
}
