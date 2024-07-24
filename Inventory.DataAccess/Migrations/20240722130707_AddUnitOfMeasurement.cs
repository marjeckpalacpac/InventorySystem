using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitOfMeasurement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitOfMeasurement",
                table: "UnitOfMeasurement");

            migrationBuilder.RenameTable(
                name: "UnitOfMeasurement",
                newName: "UnitOfMeasurements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitOfMeasurements",
                table: "UnitOfMeasurements",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 13, 7, 7, 341, DateTimeKind.Utc).AddTicks(4771), new DateTime(2024, 7, 22, 13, 7, 7, 341, DateTimeKind.Utc).AddTicks(4774) });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UnitOfMeasurements_UnitOfMeasurementId",
                table: "Products",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_UnitOfMeasurements_UnitOfMeasurementId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitOfMeasurements",
                table: "UnitOfMeasurements");

            migrationBuilder.RenameTable(
                name: "UnitOfMeasurements",
                newName: "UnitOfMeasurement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitOfMeasurement",
                table: "UnitOfMeasurement",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 12, 29, 46, 664, DateTimeKind.Utc).AddTicks(7454), new DateTime(2024, 7, 22, 12, 29, 46, 664, DateTimeKind.Utc).AddTicks(7455) });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Products",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
