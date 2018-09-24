using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class FixCasingOnCreatedModifiedColumnsInRefTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RE_SUFFIX",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RE_SUFFIX",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RE_SUFFIX",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RE_SUFFIX",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RE_SUFFIX",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RE_STATE",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RE_STATE",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RE_STATE",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RE_STATE",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RE_STATE",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RE_RELIGION",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RE_RELIGION",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RE_RELIGION",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RE_RELIGION",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RE_RELIGION",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RE_RACE",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RE_RACE",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RE_RACE",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RE_RACE",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RE_RACE",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RE_HAIR_COLOR",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RE_HAIR_COLOR",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RE_HAIR_COLOR",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RE_HAIR_COLOR",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RE_HAIR_COLOR",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RE_GENDER",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RE_GENDER",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RE_GENDER",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RE_GENDER",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RE_GENDER",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RE_EYE_COLOR",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RE_EYE_COLOR",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RE_EYE_COLOR",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RE_EYE_COLOR",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RE_EYE_COLOR",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RE_ETHNIC",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RE_ETHNIC",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RE_ETHNIC",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RE_ETHNIC",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RE_ETHNIC",
                newName: "created_by");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "RE_COUNTRY",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "RE_COUNTRY",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "RE_COUNTRY",
                newName: "modified_by");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "RE_COUNTRY",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "RE_COUNTRY",
                newName: "created_by");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "RE_SUFFIX",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "RE_SUFFIX",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "RE_SUFFIX",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "RE_SUFFIX",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "RE_SUFFIX",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RE_STATE",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "RE_STATE",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "RE_STATE",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "RE_STATE",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "RE_STATE",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RE_RELIGION",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "RE_RELIGION",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "RE_RELIGION",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "RE_RELIGION",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "RE_RELIGION",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RE_RACE",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "RE_RACE",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "RE_RACE",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "RE_RACE",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "RE_RACE",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RE_HAIR_COLOR",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "RE_HAIR_COLOR",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "RE_HAIR_COLOR",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "RE_HAIR_COLOR",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "RE_HAIR_COLOR",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RE_GENDER",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "RE_GENDER",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "RE_GENDER",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "RE_GENDER",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "RE_GENDER",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RE_EYE_COLOR",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "RE_EYE_COLOR",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "RE_EYE_COLOR",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "RE_EYE_COLOR",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "RE_EYE_COLOR",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RE_ETHNIC",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "RE_ETHNIC",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "RE_ETHNIC",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "RE_ETHNIC",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "RE_ETHNIC",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "RE_COUNTRY",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                table: "RE_COUNTRY",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modified_by",
                table: "RE_COUNTRY",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "RE_COUNTRY",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "RE_COUNTRY",
                newName: "CreatedBy");
        }
    }
}
