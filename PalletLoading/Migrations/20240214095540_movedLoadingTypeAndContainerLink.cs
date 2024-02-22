using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class movedLoadingTypeAndContainerLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "LoadingTypes");

            migrationBuilder.AddColumn<int>(
                name: "LoadingTypeId",
                table: "Containers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoadingTypeId",
                table: "Containers");

            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "LoadingTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
