using Microsoft.EntityFrameworkCore.Migrations;

namespace HRC.DB.Master.Migrations
{
    public partial class AddCountryForeignKeyToStateProvince : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "country_id",
                schema: "RE",
                table: "STATE",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_STATE_country_id",
                schema: "RE",
                table: "STATE",
                column: "country_id");

            migrationBuilder.AddForeignKey(
                name: "FK_STATE_COUNTRY_country_id",
                schema: "RE",
                table: "STATE",
                column: "country_id",
                principalSchema: "RE",
                principalTable: "COUNTRY",
                principalColumn: "country_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STATE_COUNTRY_country_id",
                schema: "RE",
                table: "STATE");

            migrationBuilder.DropIndex(
                name: "IX_STATE_country_id",
                schema: "RE",
                table: "STATE");

            migrationBuilder.DropColumn(
                name: "country_id",
                schema: "RE",
                table: "STATE");
        }
    }
}
