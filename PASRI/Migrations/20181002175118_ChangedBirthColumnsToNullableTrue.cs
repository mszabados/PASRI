using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class ChangedBirthColumnsToNullableTrue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_COUNTRY_country_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birth_date",
                schema: "PERSON",
                table: "BIRTH",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "country_id",
                schema: "PERSON",
                table: "BIRTH",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "birth_city",
                schema: "PERSON",
                table: "BIRTH",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_COUNTRY_country_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "country_id",
                principalSchema: "RE",
                principalTable: "COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "state_province_id",
                principalSchema: "RE",
                principalTable: "STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_COUNTRY_country_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.DropForeignKey(
                name: "FK_BIRTH_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH");

            migrationBuilder.AlterColumn<DateTime>(
                name: "birth_date",
                schema: "PERSON",
                table: "BIRTH",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "country_id",
                schema: "PERSON",
                table: "BIRTH",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "birth_city",
                schema: "PERSON",
                table: "BIRTH",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_COUNTRY_country_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "country_id",
                principalSchema: "RE",
                principalTable: "COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BIRTH_STATE_state_province_id",
                schema: "PERSON",
                table: "BIRTH",
                column: "state_province_id",
                principalSchema: "RE",
                principalTable: "STATE",
                principalColumn: "state_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
