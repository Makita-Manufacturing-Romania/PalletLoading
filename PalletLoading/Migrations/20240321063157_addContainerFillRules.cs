using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PalletLoading.Migrations
{
    /// <inheritdoc />
    public partial class addContainerFillRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContainerFillRule",
                table: "Containers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContainerFillRules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerFillRules", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerFillRules");

            migrationBuilder.DropColumn(
                name: "ContainerFillRule",
                table: "Containers");
        }
    }
}
