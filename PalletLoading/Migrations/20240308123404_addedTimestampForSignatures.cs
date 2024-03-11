using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PalletLoading.Migrations
{
    /// <inheritdoc />
    public partial class addedTimestampForSignatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "approval_Signature_Timestamp",
                table: "Containers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "checkerSV_Signature_Timestamp",
                table: "Containers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "checkerTL_Signature_Timestamp",
                table: "Containers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "issuer_Signature_Timestamp",
                table: "Containers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "approval_Signature_Timestamp",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "checkerSV_Signature_Timestamp",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "checkerTL_Signature_Timestamp",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "issuer_Signature_Timestamp",
                table: "Containers");
        }
    }
}
