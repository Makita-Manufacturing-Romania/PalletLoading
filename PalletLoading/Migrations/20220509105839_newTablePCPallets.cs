using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class newTablePCPallets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartCenterPallets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Container_no = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Pallet_number = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: true),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mass = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Shift = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InputTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImportDataId = table.Column<int>(type: "int", nullable: false),
                    ImportDataHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartCenterPallets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartCenterPallets_ImportData_ImportDataHistoryId",
                        column: x => x.ImportDataHistoryId,
                        principalTable: "ImportData",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PartCenterPallets_ImportData_ImportDataId",
                        column: x => x.ImportDataId,
                        principalTable: "ImportData",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartCenterPallets_ImportDataHistoryId",
                table: "PartCenterPallets",
                column: "ImportDataHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PartCenterPallets_ImportDataId",
                table: "PartCenterPallets",
                column: "ImportDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartCenterPallets");
        }
    }
}
