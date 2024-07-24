using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceUnitToUoMId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasurementId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UnitOfMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PackagingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurement", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 7, 22, 12, 29, 46, 664, DateTimeKind.Utc).AddTicks(7454), new DateTime(2024, 7, 22, 12, 29, 46, 664, DateTimeKind.Utc).AddTicks(7455) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitOfMeasurementId",
                table: "Products",
                column: "UnitOfMeasurementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Products",
                column: "UnitOfMeasurementId",
                principalTable: "UnitOfMeasurement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_UnitOfMeasurement_UnitOfMeasurementId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurement");

            migrationBuilder.DropIndex(
                name: "IX_Products_UnitOfMeasurementId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurementId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 7, 14, 15, 19, 14, 386, DateTimeKind.Utc).AddTicks(260), new DateTime(2024, 7, 14, 15, 19, 14, 386, DateTimeKind.Utc).AddTicks(264) });
        }
    }
}
