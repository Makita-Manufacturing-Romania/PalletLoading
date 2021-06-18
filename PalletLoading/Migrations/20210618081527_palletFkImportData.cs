using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class palletFkImportData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PalletImportDataId",
                table: "Pallets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_PalletImportDataId",
                table: "Pallets",
                column: "PalletImportDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pallets_ImportData_PalletImportDataId",
                table: "Pallets",
                column: "PalletImportDataId",
                principalTable: "ImportData",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pallets_ImportData_PalletImportDataId",
                table: "Pallets");

            migrationBuilder.DropIndex(
                name: "IX_Pallets_PalletImportDataId",
                table: "Pallets");

            migrationBuilder.DropColumn(
                name: "PalletImportDataId",
                table: "Pallets");
        }
    }
}
