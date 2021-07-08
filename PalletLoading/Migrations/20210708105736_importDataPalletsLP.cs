using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class importDataPalletsLP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportDataPalletsLP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode250P = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CustomerCode180P = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    PICKED250 = table.Column<int>(type: "int", nullable: false),
                    PICKED180 = table.Column<int>(type: "int", nullable: false),
                    LOADED250 = table.Column<int>(type: "int", nullable: false),
                    LOADED180 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportDataPalletsLP", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportDataPalletsLP");
        }
    }
}
