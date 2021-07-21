using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class ChangeContainerNameInCmr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "CmrDatas");

            migrationBuilder.AddColumn<string>(
                name: "ContainerName",
                table: "CmrDatas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContainerName",
                table: "CmrDatas");

            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "CmrDatas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
