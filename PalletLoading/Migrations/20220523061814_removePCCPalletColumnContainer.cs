using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class removePCCPalletColumnContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Container_no",
                table: "PartCenterPallets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Container_no",
                table: "PartCenterPallets",
                type: "nvarchar(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");
        }
    }
}
