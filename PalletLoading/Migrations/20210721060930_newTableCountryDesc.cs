using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class newTableCountryDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryDescriptionImportData",
                columns: table => new
                {
                    CCODE = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CNAME = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CADDR1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CADDR2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CADDR3 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CPSTCD = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CREPES = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CTEL = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CFAX = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CPAYT = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    CLEAD = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CVATNO = table.Column<string>(type: "nvarchar(17)", maxLength: 17, nullable: true),
                    CPAYUS = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    CDUTFR = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    CNAME2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CADR12 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CADR22 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CADR32 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CDLVN = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CDAD1 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CDAD2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CDAD3 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CDLVN2 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CDAD12 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CDAD22 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CDAD32 = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CBNKCD = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    CCOOL = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    CEUREG = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    CCNAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CAWGT = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    CPRLI = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    CCURC = table.Column<int>(type: "int", nullable: false),
                    CTOD = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDescriptionImportData", x => x.CCODE);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryDescriptionImportData");
        }
    }
}
