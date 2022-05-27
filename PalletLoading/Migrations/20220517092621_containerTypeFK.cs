using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class containerTypeFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_ContainerTypes_ContainerTypeId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_ContainerTypeId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ContainerTypeId",
                table: "Containers");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_TypeId",
                table: "Containers",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_ContainerTypes_TypeId",
                table: "Containers",
                column: "TypeId",
                principalTable: "ContainerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_ContainerTypes_TypeId",
                table: "Containers");

            migrationBuilder.DropIndex(
                name: "IX_Containers_TypeId",
                table: "Containers");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Containers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ContainerTypeId",
                table: "Containers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ContainerTypeId",
                table: "Containers",
                column: "ContainerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_ContainerTypes_ContainerTypeId",
                table: "Containers",
                column: "ContainerTypeId",
                principalTable: "ContainerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
