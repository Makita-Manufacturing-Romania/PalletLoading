using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class newTable_ImportData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportData",
                columns: table => new
                {
                    container_no = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    consignee_code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    salse_part = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    serial_from = table.Column<int>(type: "int", nullable: false),
                    serial_to = table.Column<int>(type: "int", nullable: false),
                    picking_qty = table.Column<int>(type: "int", nullable: false),
                    pallet_no = table.Column<int>(type: "int", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    loading_date = table.Column<int>(type: "int", nullable: false),
                    loading_time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportData");
        }
    }
}
