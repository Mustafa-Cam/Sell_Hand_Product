using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sellhandproduct.Migrations
{
    public partial class updatedatacontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Baskets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Baskets",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "ProductPrice",
                table: "Baskets",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
