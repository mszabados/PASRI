using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class CreateBirthToRebuildTableWithStateProvinceIdOrderedCorrectly2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Birth_RE_COUNTRY_CountryId",
                "Birth");

            migrationBuilder.DropForeignKey(
                "FK_Birth_PERSON_PersonId",
                "Birth");

            migrationBuilder.DropForeignKey(
                "FK_Birth_RE_STATE_StateProvinceId",
                "Birth");

            migrationBuilder.DropPrimaryKey(
                "PK_Birth",
                "Birth");

            migrationBuilder.RenameTable(
                "Birth",
                newName: "BIRTH");

            migrationBuilder.RenameColumn(
                "StateProvinceId",
                "BIRTH",
                "state_province_id");

            migrationBuilder.RenameColumn(
                "PersonId",
                "BIRTH",
                "person_id");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "BIRTH",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "BIRTH",
                "modified_by");

            migrationBuilder.RenameColumn(
                "Date",
                "BIRTH",
                "birth_date");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "BIRTH",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "BIRTH",
                "created_by");

            migrationBuilder.RenameColumn(
                "CountryId",
                "BIRTH",
                "country_id");

            migrationBuilder.RenameColumn(
                "City",
                "BIRTH",
                "birth_city");

            migrationBuilder.RenameColumn(
                "Id",
                "BIRTH",
                "birth_id");

            migrationBuilder.RenameIndex(
                "IX_Birth_StateProvinceId",
                table: "BIRTH",
                newName: "IX_BIRTH_state_province_id");

            migrationBuilder.RenameIndex(
                "IX_Birth_PersonId",
                table: "BIRTH",
                newName: "IX_BIRTH_person_id");

            migrationBuilder.RenameIndex(
                "IX_Birth_CountryId",
                table: "BIRTH",
                newName: "IX_BIRTH_country_id");

            migrationBuilder.AlterColumn<DateTime>(
                "modified_date",
                "BIRTH",
                "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "modified_by",
                "BIRTH",
                "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                "birth_date",
                "BIRTH",
                "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                "created_date",
                "BIRTH",
                "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "created_by",
                "BIRTH",
                "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "birth_city",
                "BIRTH",
                "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                "PK_BIRTH",
                "BIRTH",
                "birth_id");

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_RE_COUNTRY_country_id",
                "BIRTH",
                "country_id",
                "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_PERSON_person_id",
                "BIRTH",
                "person_id",
                "PERSON",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_BIRTH_RE_STATE_state_province_id",
                "BIRTH",
                "state_province_id",
                "RE_STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_BIRTH_RE_COUNTRY_country_id",
                "BIRTH");

            migrationBuilder.DropForeignKey(
                "FK_BIRTH_PERSON_person_id",
                "BIRTH");

            migrationBuilder.DropForeignKey(
                "FK_BIRTH_RE_STATE_state_province_id",
                "BIRTH");

            migrationBuilder.DropPrimaryKey(
                "PK_BIRTH",
                "BIRTH");

            migrationBuilder.RenameTable(
                "BIRTH",
                newName: "Birth");

            migrationBuilder.RenameColumn(
                "state_province_id",
                "Birth",
                "StateProvinceId");

            migrationBuilder.RenameColumn(
                "person_id",
                "Birth",
                "PersonId");

            migrationBuilder.RenameColumn(
                "modified_date",
                "Birth",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "Birth",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "birth_date",
                "Birth",
                "Date");

            migrationBuilder.RenameColumn(
                "created_date",
                "Birth",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "Birth",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "country_id",
                "Birth",
                "CountryId");

            migrationBuilder.RenameColumn(
                "birth_city",
                "Birth",
                "City");

            migrationBuilder.RenameColumn(
                "birth_id",
                "Birth",
                "Id");

            migrationBuilder.RenameIndex(
                "IX_BIRTH_state_province_id",
                table: "Birth",
                newName: "IX_Birth_StateProvinceId");

            migrationBuilder.RenameIndex(
                "IX_BIRTH_person_id",
                table: "Birth",
                newName: "IX_Birth_PersonId");

            migrationBuilder.RenameIndex(
                "IX_BIRTH_country_id",
                table: "Birth",
                newName: "IX_Birth_CountryId");

            migrationBuilder.AlterColumn<DateTime>(
                "ModifiedDate",
                "Birth",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "ModifiedBy",
                "Birth",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                "Date",
                "Birth",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                "CreatedDate",
                "Birth",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "CreatedBy",
                "Birth",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                "City",
                "Birth",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddPrimaryKey(
                "PK_Birth",
                "Birth",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_Birth_RE_COUNTRY_CountryId",
                "Birth",
                "CountryId",
                "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Birth_PERSON_PersonId",
                "Birth",
                "PersonId",
                "PERSON",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Birth_RE_STATE_StateProvinceId",
                "Birth",
                "StateProvinceId",
                "RE_STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
