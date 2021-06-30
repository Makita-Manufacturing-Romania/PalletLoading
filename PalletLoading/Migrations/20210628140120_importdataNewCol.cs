using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class importdataNewCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pallets_PalletImportDataId",
                table: "Pallets");

            migrationBuilder.AddColumn<decimal>(
                name: "IPRVCC",
                table: "ImportData",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IPRVDD",
                table: "ImportData",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IPRVMM",
                table: "ImportData",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IPRVYY",
                table: "ImportData",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "IPSEQ",
                table: "ImportData",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_PalletImportDataId",
                table: "Pallets",
                column: "PalletImportDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pallets_PalletImportDataId",
                table: "Pallets");

            migrationBuilder.DropColumn(
                name: "IPRVCC",
                table: "ImportData");

            migrationBuilder.DropColumn(
                name: "IPRVDD",
                table: "ImportData");

            migrationBuilder.DropColumn(
                name: "IPRVMM",
                table: "ImportData");

            migrationBuilder.DropColumn(
                name: "IPRVYY",
                table: "ImportData");

            migrationBuilder.DropColumn(
                name: "IPSEQ",
                table: "ImportData");

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_PalletImportDataId",
                table: "Pallets",
                column: "PalletImportDataId",
                unique: true,
                filter: "[PalletImportDataId] IS NOT NULL");
        }
    }
}
