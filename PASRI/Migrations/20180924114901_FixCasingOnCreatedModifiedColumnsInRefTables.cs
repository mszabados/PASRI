using Microsoft.EntityFrameworkCore.Migrations;

namespace PASRI.API.Migrations
{
    public partial class FixCasingOnCreatedModifiedColumnsInRefTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Description",
                "RE_SUFFIX",
                "description");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "RE_SUFFIX",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "RE_SUFFIX",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "RE_SUFFIX",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "RE_SUFFIX",
                "created_by");

            migrationBuilder.RenameColumn(
                "Description",
                "RE_STATE",
                "description");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "RE_STATE",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "RE_STATE",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "RE_STATE",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "RE_STATE",
                "created_by");

            migrationBuilder.RenameColumn(
                "Description",
                "RE_RELIGION",
                "description");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "RE_RELIGION",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "RE_RELIGION",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "RE_RELIGION",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "RE_RELIGION",
                "created_by");

            migrationBuilder.RenameColumn(
                "Description",
                "RE_RACE",
                "description");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "RE_RACE",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "RE_RACE",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "RE_RACE",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "RE_RACE",
                "created_by");

            migrationBuilder.RenameColumn(
                "Description",
                "RE_HAIR_COLOR",
                "description");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "RE_HAIR_COLOR",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "RE_HAIR_COLOR",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "RE_HAIR_COLOR",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "RE_HAIR_COLOR",
                "created_by");

            migrationBuilder.RenameColumn(
                "Description",
                "RE_GENDER",
                "description");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "RE_GENDER",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "RE_GENDER",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "RE_GENDER",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "RE_GENDER",
                "created_by");

            migrationBuilder.RenameColumn(
                "Description",
                "RE_EYE_COLOR",
                "description");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "RE_EYE_COLOR",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "RE_EYE_COLOR",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "RE_EYE_COLOR",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "RE_EYE_COLOR",
                "created_by");

            migrationBuilder.RenameColumn(
                "Description",
                "RE_ETHNIC",
                "description");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "RE_ETHNIC",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "RE_ETHNIC",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "RE_ETHNIC",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "RE_ETHNIC",
                "created_by");

            migrationBuilder.RenameColumn(
                "Description",
                "RE_COUNTRY",
                "description");

            migrationBuilder.RenameColumn(
                "ModifiedDate",
                "RE_COUNTRY",
                "modified_date");

            migrationBuilder.RenameColumn(
                "ModifiedBy",
                "RE_COUNTRY",
                "modified_by");

            migrationBuilder.RenameColumn(
                "CreatedDate",
                "RE_COUNTRY",
                "created_date");

            migrationBuilder.RenameColumn(
                "CreatedBy",
                "RE_COUNTRY",
                "created_by");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "description",
                "RE_SUFFIX",
                "Description");

            migrationBuilder.RenameColumn(
                "modified_date",
                "RE_SUFFIX",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "RE_SUFFIX",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "RE_SUFFIX",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "RE_SUFFIX",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "description",
                "RE_STATE",
                "Description");

            migrationBuilder.RenameColumn(
                "modified_date",
                "RE_STATE",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "RE_STATE",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "RE_STATE",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "RE_STATE",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "description",
                "RE_RELIGION",
                "Description");

            migrationBuilder.RenameColumn(
                "modified_date",
                "RE_RELIGION",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "RE_RELIGION",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "RE_RELIGION",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "RE_RELIGION",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "description",
                "RE_RACE",
                "Description");

            migrationBuilder.RenameColumn(
                "modified_date",
                "RE_RACE",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "RE_RACE",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "RE_RACE",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "RE_RACE",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "description",
                "RE_HAIR_COLOR",
                "Description");

            migrationBuilder.RenameColumn(
                "modified_date",
                "RE_HAIR_COLOR",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "RE_HAIR_COLOR",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "RE_HAIR_COLOR",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "RE_HAIR_COLOR",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "description",
                "RE_GENDER",
                "Description");

            migrationBuilder.RenameColumn(
                "modified_date",
                "RE_GENDER",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "RE_GENDER",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "RE_GENDER",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "RE_GENDER",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "description",
                "RE_EYE_COLOR",
                "Description");

            migrationBuilder.RenameColumn(
                "modified_date",
                "RE_EYE_COLOR",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "RE_EYE_COLOR",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "RE_EYE_COLOR",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "RE_EYE_COLOR",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "description",
                "RE_ETHNIC",
                "Description");

            migrationBuilder.RenameColumn(
                "modified_date",
                "RE_ETHNIC",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "RE_ETHNIC",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "RE_ETHNIC",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "RE_ETHNIC",
                "CreatedBy");

            migrationBuilder.RenameColumn(
                "description",
                "RE_COUNTRY",
                "Description");

            migrationBuilder.RenameColumn(
                "modified_date",
                "RE_COUNTRY",
                "ModifiedDate");

            migrationBuilder.RenameColumn(
                "modified_by",
                "RE_COUNTRY",
                "ModifiedBy");

            migrationBuilder.RenameColumn(
                "created_date",
                "RE_COUNTRY",
                "CreatedDate");

            migrationBuilder.RenameColumn(
                "created_by",
                "RE_COUNTRY",
                "CreatedBy");
        }
    }
}
