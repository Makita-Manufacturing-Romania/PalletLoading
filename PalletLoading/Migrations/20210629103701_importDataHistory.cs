using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class importDataHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportDataHistory",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealId = table.Column<int>(type: "int", nullable: false),
                    container_no = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    consignee_code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    salse_part = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    serial_from = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    serial_to = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    picking_qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    pallet_no = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    loading_date = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    loading_time = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IPRVCC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IPRVYY = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IPRVMM = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IPRVDD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IPSEQ = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportDataHistory", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportDataHistory");
        }
    }
}
