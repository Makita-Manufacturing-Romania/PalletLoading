using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class idHistoryDeletedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RealId",
                table: "ImportDataHistory");

            migrationBuilder.AddColumn<int>(
                name: "PalletImportDataHistoryId",
                table: "Pallets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_PalletImportDataHistoryId",
                table: "Pallets",
                column: "PalletImportDataHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pallets_ImportDataHistory_PalletImportDataHistoryId",
                table: "Pallets",
                column: "PalletImportDataHistoryId",
                principalTable: "ImportDataHistory",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pallets_ImportDataHistory_PalletImportDataHistoryId",
                table: "Pallets");

            migrationBuilder.DropIndex(
                name: "IX_Pallets_PalletImportDataHistoryId",
                table: "Pallets");

            migrationBuilder.DropColumn(
                name: "PalletImportDataHistoryId",
                table: "Pallets");

            migrationBuilder.AddColumn<int>(
                name: "RealId",
                table: "ImportDataHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
