using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class fk_palletContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Pallets_PalletId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_PalletId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "PalletId",
                table: "Containers");

            migrationBuilder.CreateIndex(
                name: "IX_Pallets_Container2Id",
                table: "Pallets",
                column: "Container2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pallets_Containers_Container2Id",
                table: "Pallets",
                column: "Container2Id",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pallets_Containers_Container2Id",
                table: "Pallets");

            migrationBuilder.DropIndex(
                name: "IX_Pallets_Container2Id",
                table: "Pallets");

            migrationBuilder.AddColumn<int>(
                name: "PalletId",
                table: "Containers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_PalletId",
                table: "Containers",
                column: "PalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Pallets_PalletId",
                table: "Containers",
                column: "PalletId",
                principalTable: "Pallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
