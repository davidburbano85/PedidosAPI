using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pedidos_API.Migrations
{
    /// <inheritdoc />
    public partial class NuevaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechadeLlegada",
                value: new DateTime(2024, 3, 11, 23, 4, 0, 236, DateTimeKind.Local).AddTicks(5751));

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechadeLlegada",
                value: new DateTime(2024, 3, 11, 23, 4, 0, 236, DateTimeKind.Local).AddTicks(5774));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechadeLlegada",
                value: new DateTime(2025, 3, 11, 22, 30, 1, 206, DateTimeKind.Local).AddTicks(4609));

            migrationBuilder.UpdateData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechadeLlegada",
                value: new DateTime(2025, 3, 11, 22, 30, 1, 206, DateTimeKind.Local).AddTicks(4631));
        }
    }
}
