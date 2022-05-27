using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class addtablePCPallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataHistoryId",
                table: "PartCenterPallets");

            migrationBuilder.DropForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataId",
                table: "PartCenterPallets");

            migrationBuilder.AlterColumn<decimal>(
                name: "Pallet_number",
                table: "PartCenterPallets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "nvarchar(35)",
                oldMaxLength: 35,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImportDataId",
                table: "PartCenterPallets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ImportDataHistoryId",
                table: "PartCenterPallets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataHistoryId",
                table: "PartCenterPallets",
                column: "ImportDataHistoryId",
                principalTable: "ImportData",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataId",
                table: "PartCenterPallets",
                column: "ImportDataId",
                principalTable: "ImportData",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataHistoryId",
                table: "PartCenterPallets");

            migrationBuilder.DropForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataId",
                table: "PartCenterPallets");

            migrationBuilder.AlterColumn<string>(
                name: "Pallet_number",
                table: "PartCenterPallets",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "ImportDataId",
                table: "PartCenterPallets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImportDataHistoryId",
                table: "PartCenterPallets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataHistoryId",
                table: "PartCenterPallets",
                column: "ImportDataHistoryId",
                principalTable: "ImportData",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PartCenterPallets_ImportData_ImportDataId",
                table: "PartCenterPallets",
                column: "ImportDataId",
                principalTable: "ImportData",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
