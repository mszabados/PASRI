using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class RepairEffectiveDateColumnNameOnPersonAndCreatedModifiedColumnsOnPersonAndBirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "PERSON",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "PERSON",
                "modified_by");

            migrationBuilder.RenameColumn(
                "EffectiveDate",
                "PERSON",
                "effect_date");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "PERSON",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "PERSON",
                "created_by");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "BIRTH",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "BIRTH",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "BIRTH",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "BIRTH",
                "created_by");

            migrationBuilder.AlterColumn<DateTime>(
                "effect_date",
                "PERSON",
                "date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "modified_date",
                "PERSON",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "PERSON",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "effect_date",
                "PERSON",
                "EffectiveDate");

            migrationBuilder.RenameColumn(
                "created_date",
                "PERSON",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "PERSON",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "modified_date",
                "BIRTH",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "BIRTH",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "BIRTH",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "BIRTH",
                "CreatedBy");

            migrationBuilder.AlterColumn<DateTime>(
                "EffectiveDate",
                "PERSON",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
