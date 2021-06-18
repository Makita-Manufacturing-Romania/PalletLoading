using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class changetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "serial_to",
                table: "ImportData",
                type: "decimal(9,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "serial_from",
                table: "ImportData",
                type: "decimal(9,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "picking_qty",
                table: "ImportData",
                type: "decimal(9,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "pallet_no",
                table: "ImportData",
                type: "decimal(4,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "loading_time",
                table: "ImportData",
                type: "decimal(6,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "loading_date",
                table: "ImportData",
                type: "decimal(8,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "serial_to",
                table: "ImportData",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,0)");

            migrationBuilder.AlterColumn<int>(
                name: "serial_from",
                table: "ImportData",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,0)");

            migrationBuilder.AlterColumn<int>(
                name: "picking_qty",
                table: "ImportData",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,0)");

            migrationBuilder.AlterColumn<int>(
                name: "pallet_no",
                table: "ImportData",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4,0)");

            migrationBuilder.AlterColumn<int>(
                name: "loading_time",
                table: "ImportData",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,0)");

            migrationBuilder.AlterColumn<int>(
                name: "loading_date",
                table: "ImportData",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(8,0)");
        }
    }
}
