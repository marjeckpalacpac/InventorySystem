using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CorrectSpellingProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pproducts_ProductCategories_ProductCategoryId",
                table: "Pproducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Pproducts_Suppliers_SupplierId",
                table: "Pproducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pproducts",
                table: "Pproducts");

            migrationBuilder.RenameTable(
                name: "Pproducts",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_Pproducts_SupplierId",
                table: "Products",
                newName: "IX_Products_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Pproducts_ProductCategoryId",
                table: "Products",
                newName: "IX_Products_ProductCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 7, 14, 15, 19, 14, 386, DateTimeKind.Utc).AddTicks(260), new DateTime(2024, 7, 14, 15, 19, 14, 386, DateTimeKind.Utc).AddTicks(264) });

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategories_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Suppliers_SupplierId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Pproducts");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplierId",
                table: "Pproducts",
                newName: "IX_Pproducts_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Pproducts",
                newName: "IX_Pproducts_ProductCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pproducts",
                table: "Pproducts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTime(2024, 7, 14, 15, 6, 32, 209, DateTimeKind.Utc).AddTicks(4277), new DateTime(2024, 7, 14, 15, 6, 32, 209, DateTimeKind.Utc).AddTicks(4279) });

            migrationBuilder.AddForeignKey(
                name: "FK_Pproducts_ProductCategories_ProductCategoryId",
                table: "Pproducts",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pproducts_Suppliers_SupplierId",
                table: "Pproducts",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }
    }
}
