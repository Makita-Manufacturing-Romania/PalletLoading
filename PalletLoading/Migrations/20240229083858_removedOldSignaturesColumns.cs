using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class removedOldSignaturesColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "forklifterName",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "operatorName",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "svName",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "tlName",
                table: "Containers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "forklifterName",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "operatorName",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "svName",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tlName",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
