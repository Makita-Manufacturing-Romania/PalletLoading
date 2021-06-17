using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class addForeignKey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pallet_Container_ContainerId",
                table: "Pallet");

            migrationBuilder.DropIndex(
                name: "IX_Pallet_ContainerId",
                table: "Pallet");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Pallet");

            migrationBuilder.AddColumn<int>(
                name: "PalletId",
                table: "Container",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Container_PalletId",
                table: "Container",
                column: "PalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Pallet_PalletId",
                table: "Container",
                column: "PalletId",
                principalTable: "Pallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Pallet_PalletId",
                table: "Container");

            migrationBuilder.DropIndex(
                name: "IX_Container_PalletId",
                table: "Container");

            migrationBuilder.DropColumn(
                name: "PalletId",
                table: "Container");

            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "Pallet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pallet_ContainerId",
                table: "Pallet",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pallet_Container_ContainerId",
                table: "Pallet",
                column: "ContainerId",
                principalTable: "Container",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
