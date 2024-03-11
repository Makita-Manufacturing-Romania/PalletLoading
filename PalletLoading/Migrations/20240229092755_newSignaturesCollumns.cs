using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class newSignaturesCollumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "approval_Name",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "checkerSV_Name",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "checkerTL_Name",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "issuer_Name",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approval_Name",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "checkerSV_Name",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "checkerTL_Name",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "issuer_Name",
                table: "Containers");
        }
    }
}
