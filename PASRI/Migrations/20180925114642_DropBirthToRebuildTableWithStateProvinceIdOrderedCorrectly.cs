using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class DropBirthToRebuildTableWithStateProvinceIdOrderedCorrectly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_country_id",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_PERSON_person_id",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_RE_STATE_state_province_id",
                table: "BIRTH");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BIRTH",
                table: "BIRTH");

            migrationBuilder.RenameTable(
                name: "BIRTH",
                newName: "Birth");

            migrationBuilder.RenameColumn(
                name: "state_province_id",
                table: "Birth",
                newName: "StateProvinceId");

            migrationBuilder.RenameColumn(
                name: "person_id",
                table: "Birth",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "Birth",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "Birth",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Birth",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "Birth",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "Birth",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "country_id",
                table: "Birth",
                newName: "CountryId");

            migrationBuilder.RenameColumn(
                name: "birth_city",
                table: "Birth",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "birth_id",
                table: "Birth",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_BIRTH_state_province_id",
                table: "Birth",
                newName: "IX_Birth_StateProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_BIRTH_person_id",
                table: "Birth",
                newName: "IX_Birth_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_BIRTH_country_id",
                table: "Birth",
                newName: "IX_Birth_CountryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Birth",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "Birth",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Birth",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Birth",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Birth",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Birth",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Birth",
                table: "Birth",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Birth_RE_COUNTRY_CountryId",
                table: "Birth",
                column: "CountryId",
                principalTable: "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Birth_PERSON_PersonId",
                table: "Birth",
                column: "PersonId",
                principalTable: "PERSON",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Birth_RE_STATE_StateProvinceId",
                table: "Birth",
                column: "StateProvinceId",
                principalTable: "RE_STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birth_RE_COUNTRY_CountryId",
                table: "Birth");

            migrationBuilder.DropForeignKey(
                name: "FK_Birth_PERSON_PersonId",
                table: "Birth");

            migrationBuilder.DropForeignKey(
                name: "FK_Birth_RE_STATE_StateProvinceId",
                table: "Birth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Birth",
                table: "Birth");

            migrationBuilder.RenameTable(
                name: "Birth",
                newName: "BIRTH");

            migrationBuilder.RenameColumn(
                name: "StateProvinceId",
                table: "BIRTH",
                newName: "state_province_id");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "BIRTH",
                newName: "person_id");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "BIRTH",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "BIRTH",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "BIRTH",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "BIRTH",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "BIRTH",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "BIRTH",
                newName: "country_id");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "BIRTH",
                newName: "birth_city");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BIRTH",
                newName: "birth_id");

            migrationBuilder.RenameIndex(
                name: "IX_Birth_StateProvinceId",
                table: "BIRTH",
                newName: "IX_BIRTH_state_province_id");

            migrationBuilder.RenameIndex(
                name: "IX_Birth_PersonId",
                table: "BIRTH",
                newName: "IX_BIRTH_person_id");

            migrationBuilder.RenameIndex(
                name: "IX_Birth_CountryId",
                table: "BIRTH",
                newName: "IX_BIRTH_country_id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "modified_date",
                table: "BIRTH",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "modified_by",
                table: "BIRTH",
                type: "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "birth_date",
                table: "BIRTH",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "BIRTH",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "BIRTH",
                type: "varchar(30)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birth_city",
                table: "BIRTH",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BIRTH",
                table: "BIRTH",
                column: "birth_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_COUNTRY_country_id",
                table: "BIRTH",
                column: "country_id",
                principalTable: "RE_COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_PERSON_person_id",
                table: "BIRTH",
                column: "person_id",
                principalTable: "PERSON",
                principalColumn: "person_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_RE_STATE_state_province_id",
                table: "BIRTH",
                column: "state_province_id",
                principalTable: "RE_STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
