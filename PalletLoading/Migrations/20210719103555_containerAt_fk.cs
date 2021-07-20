using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class containerAt_fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ContainerATs_ContainerId",
                table: "ContainerATs",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContainerATs_Containers_ContainerId",
                table: "ContainerATs",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContainerATs_Containers_ContainerId",
                table: "ContainerATs");

            migrationBuilder.DropIndex(
                name: "IX_ContainerATs_ContainerId",
                table: "ContainerATs");
        }
    }
}
