using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class fk_at_countries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContainerATs_CountryId",
                table: "ContainerATs",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerATs_Countries_CountryId",
                table: "ContainerATs",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerATs_Countries_CountryId",
                table: "ContainerATs");

            migrationBuilder.DropIndex(
                name: "IX_ContainerATs_CountryId",
                table: "ContainerATs");
        }
    }
}
