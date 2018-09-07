using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class DropReferenceBloodTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood");

            migrationBuilder.RenameTable(
                name: "ReferenceTypeBlood",
                newName: "ReferenceBloodTypes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceBloodTypes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceBloodTypes",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceBloodTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceBloodTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "char(2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceBloodTypes",
                table: "ReferenceBloodTypes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReferenceBloodTypes",
                table: "ReferenceBloodTypes");

            migrationBuilder.RenameTable(
                name: "ReferenceBloodTypes",
                newName: "ReferenceTypeBlood");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "ReferenceTypeBlood",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "ReferenceTypeBlood",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "ReferenceTypeBlood",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "ReferenceTypeBlood",
                type: "char(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReferenceTypeBlood",
                table: "ReferenceTypeBlood",
                column: "Id");
        }
    }
}
