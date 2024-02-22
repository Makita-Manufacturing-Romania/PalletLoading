using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class addedConfirmationsForContainerData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "formDefinitionConfirm",
                table: "Containers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "loadingTypeConfirm",
                table: "Containers",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "securingLoadConfirm",
                table: "Containers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "formDefinitionConfirm",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "loadingTypeConfirm",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "securingLoadConfirm",
                table: "Containers");
        }
    }
}
