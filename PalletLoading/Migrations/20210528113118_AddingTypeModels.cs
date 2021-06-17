using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class AddingTypeModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Container_Pallet_PalletId",
                table: "Container");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pallet",
                table: "Pallet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Container",
                table: "Container");

            migrationBuilder.RenameTable(
                name: "Pallet",
                newName: "Pallets");

            migrationBuilder.RenameTable(
                name: "Container",
                newName: "Containers");

            migrationBuilder.RenameIndex(
                name: "IX_Container_PalletId",
                table: "Containers",
                newName: "IX_Containers_PalletId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pallets",
                table: "Pallets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Containers",
                table: "Containers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContainerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PalletTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalletTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Pallets_PalletId",
                table: "Containers",
                column: "PalletId",
                principalTable: "Pallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Pallets_PalletId",
                table: "Containers");

            migrationBuilder.DropTable(
                name: "ContainerTypes");

            migrationBuilder.DropTable(
                name: "PalletTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pallets",
                table: "Pallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Containers",
                table: "Containers");

            migrationBuilder.RenameTable(
                name: "Pallets",
                newName: "Pallet");

            migrationBuilder.RenameTable(
                name: "Containers",
                newName: "Container");

            migrationBuilder.RenameIndex(
                name: "IX_Containers_PalletId",
                table: "Container",
                newName: "IX_Container_PalletId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pallet",
                table: "Pallet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Container",
                table: "Container",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Container_Pallet_PalletId",
                table: "Container",
                column: "PalletId",
                principalTable: "Pallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
