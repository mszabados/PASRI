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
                "RE_BLOOD_TYPE",
                table => new
                {
                    blood_type_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(3)"),
                    description = table.Column<string>("varchar(11)"),
                    created_date = table.Column<DateTime>("datetime", nullable: true),
                    created_by = table.Column<string>("varchar(30)", nullable: true),
                    modified_date = table.Column<DateTime>("datetime", nullable: true),
                    modified_by = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_BLOOD_TYPE", x => x.blood_type_id);
                });

            migrationBuilder.CreateTable(
                "RE_COUNTRY",
                table => new
                {
                    country_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(2)"),
                    Description = table.Column<string>("varchar(44)"),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_COUNTRY", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                "RE_ETHNIC",
                table => new
                {
                    country_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(2)"),
                    Description = table.Column<string>("varchar(28)"),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_ETHNIC", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                "RE_EYE_COLOR",
                table => new
                {
                    country_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(2)"),
                    Description = table.Column<string>("varchar(14)"),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_EYE_COLOR", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                "RE_GENDER",
                table => new
                {
                    country_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(1)"),
                    Description = table.Column<string>("varchar(7)"),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_GENDER", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                "RE_HAIR_COLOR",
                table => new
                {
                    country_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(2)"),
                    Description = table.Column<string>("varchar(6)"),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_HAIR_COLOR", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                "RE_RACE",
                table => new
                {
                    country_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(1)"),
                    Description = table.Column<string>("varchar(237)"),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_RACE", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                "RE_RELIGION",
                table => new
                {
                    country_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(2)"),
                    Description = table.Column<string>("varchar(253)"),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_RELIGION", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                "RE_STATE",
                table => new
                {
                    country_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(2)"),
                    Description = table.Column<string>("varchar(35)"),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_STATE", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                "RE_SUFFIX",
                table => new
                {
                    country_id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>("char(6)"),
                    Description = table.Column<string>("varchar(15)"),
                    CreatedDate = table.Column<DateTime>("datetime", nullable: true),
                    CreatedBy = table.Column<string>("varchar(30)", nullable: true),
                    ModifiedDate = table.Column<DateTime>("datetime", nullable: true),
                    ModifiedBy = table.Column<string>("varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RE_SUFFIX", x => x.country_id);
                });

            migrationBuilder.CreateIndex(
                "IX_RE_BLOOD_TYPE_code",
                "RE_BLOOD_TYPE",
                "code",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RE_COUNTRY_code",
                "RE_COUNTRY",
                "code",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RE_ETHNIC_code",
                "RE_ETHNIC",
                "code",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RE_EYE_COLOR_code",
                "RE_EYE_COLOR",
                "code",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RE_GENDER_code",
                "RE_GENDER",
                "code",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RE_HAIR_COLOR_code",
                "RE_HAIR_COLOR",
                "code",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RE_RACE_code",
                "RE_RACE",
                "code",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RE_RELIGION_code",
                "RE_RELIGION",
                "code",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RE_STATE_code",
                "RE_STATE",
                "code",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_RE_SUFFIX_code",
                "RE_SUFFIX",
                "code",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "RE_BLOOD_TYPE");

            migrationBuilder.DropTable(
                "RE_COUNTRY");

            migrationBuilder.DropTable(
                "RE_ETHNIC");

            migrationBuilder.DropTable(
                "RE_EYE_COLOR");

            migrationBuilder.DropTable(
                "RE_GENDER");

            migrationBuilder.DropTable(
                "RE_HAIR_COLOR");

            migrationBuilder.DropTable(
                "RE_RACE");

            migrationBuilder.DropTable(
                "RE_RELIGION");

            migrationBuilder.DropTable(
                "RE_STATE");

            migrationBuilder.DropTable(
                "RE_SUFFIX");
        }
    }
}
