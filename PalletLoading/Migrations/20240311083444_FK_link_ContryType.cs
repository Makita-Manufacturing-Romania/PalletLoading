using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PalletLoading.Migrations
{
    /// <inheritdoc />
    public partial class FK_link_ContryType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Countries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_TypeId",
                table: "Countries",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_ClientTypes_TypeId",
                table: "Countries",
                column: "TypeId",
                principalTable: "ClientTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_ClientTypes_TypeId",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_TypeId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Countries");
        }
    }
}
