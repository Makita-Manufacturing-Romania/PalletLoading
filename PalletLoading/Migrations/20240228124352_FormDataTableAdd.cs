using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PalletLoading.Migrations
{
    public partial class FormDataTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormDatas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rampaNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oraIntrare = table.Column<DateTime>(type: "datetime2", nullable: false),
                    start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    stop = table.Column<DateTime>(type: "datetime2", nullable: false),
                    oraIesire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nrCamion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nrContainer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nrPaletiPickPeTemp = table.Column<int>(type: "int", nullable: false),
                    nrPalScanner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nrAviz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nrSigiliu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numeStivuitorist = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDatas", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormDatas");
        }
    }
}
