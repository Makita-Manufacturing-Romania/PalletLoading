using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class changeFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataHistoryId",
                table: "PartCenterPallets");

            migrationBuilder.AddForeignKey(
                name: "FK_PartCenterPallets_ImportDataHistory_ImportDataHistoryId",
                table: "PartCenterPallets",
                column: "ImportDataHistoryId",
                principalTable: "ImportDataHistory",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartCenterPallets_ImportDataHistory_ImportDataHistoryId",
                table: "PartCenterPallets");

            migrationBuilder.AddForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataHistoryId",
                table: "PartCenterPallets",
                column: "ImportDataHistoryId",
                principalTable: "ImportData",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
