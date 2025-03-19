using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace innobyte_e_commerce.Migrations
{
    /// <inheritdoc />
    public partial class addressadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "MasterOrders",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "MasterOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalItems",
                table: "MasterOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "MasterOrders");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "MasterOrders");

            migrationBuilder.DropColumn(
                name: "TotalItems",
                table: "MasterOrders");
        }
    }
}
