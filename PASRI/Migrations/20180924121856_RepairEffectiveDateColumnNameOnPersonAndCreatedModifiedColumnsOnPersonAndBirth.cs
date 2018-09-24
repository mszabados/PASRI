using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class RepairEffectiveDateColumnNameOnPersonAndCreatedModifiedColumnsOnPersonAndBirth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "PERSON",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "PERSON",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "EffectiveDate",
                table: "PERSON",
                newName: "effect_date");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "PERSON",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "PERSON",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "BIRTH",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "BIRTH",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "BIRTH",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "BIRTH",
                newName: "created_by");

            migrationBuilder.AlterColumn<DateTime>(
                name: "effect_date",
                table: "PERSON",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "PERSON",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "PERSON",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "effect_date",
                table: "PERSON",
                newName: "EffectiveDate");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "PERSON",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "PERSON",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "BIRTH",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "BIRTH",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "BIRTH",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "BIRTH",
                newName: "CreatedBy");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EffectiveDate",
                table: "PERSON",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
