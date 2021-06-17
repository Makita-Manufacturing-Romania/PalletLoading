using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class AddingPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Column",
                table: "Pallets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Row",
                table: "Pallets",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Column",
                table: "Pallets");

            migrationBuilder.DropColumn(
                name: "Row",
                table: "Pallets");
        }
    }
}
