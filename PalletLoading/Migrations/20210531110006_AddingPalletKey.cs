using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class AddingPalletKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PalletTypeId",
                table: "Pallets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Pallets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_PalletTypeId",
                table: "Pallets",
                column: "PalletTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pallets_PalletTypes_PalletTypeId",
                table: "Pallets",
                column: "PalletTypeId",
                principalTable: "PalletTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pallets_PalletTypes_PalletTypeId",
                table: "Pallets");

            migrationBuilder.DropIndex(
                name: "IX_Pallets_PalletTypeId",
                table: "Pallets");

            migrationBuilder.DropColumn(
                name: "PalletTypeId",
                table: "Pallets");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Pallets");
        }
    }
}
