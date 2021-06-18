using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class nullColumnOnPallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pallets_ImportData_PalletImportDataId",
                table: "Pallets");

            migrationBuilder.DropIndex(
                name: "IX_Pallets_PalletImportDataId",
                table: "Pallets");

            migrationBuilder.AlterColumn<int>(
                name: "PalletImportDataId",
                table: "Pallets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_PalletImportDataId",
                table: "Pallets",
                column: "PalletImportDataId",
                unique: true,
                filter: "[PalletImportDataId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Pallets_ImportData_PalletImportDataId",
                table: "Pallets",
                column: "PalletImportDataId",
                principalTable: "ImportData",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pallets_ImportData_PalletImportDataId",
                table: "Pallets");

            migrationBuilder.DropIndex(
                name: "IX_Pallets_PalletImportDataId",
                table: "Pallets");

            migrationBuilder.AlterColumn<int>(
                name: "PalletImportDataId",
                table: "Pallets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
