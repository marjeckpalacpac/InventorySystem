using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Inventory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddLookupListingAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LookupListings",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(110)", maxLength: 110, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SortId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LookupListings", x => new { x.Code, x.Name });
                });

            migrationBuilder.InsertData(
                table: "LookupListings",
                columns: new[] { "Code", "Name", "Id", "IsActive", "SortId", "Value" },
                values: new object[,]
                {
                    { "SupplyChainPartner", "Customer", 2, true, 2, "Customer" },
                    { "SupplyChainPartner", "Supplier", 1, true, 1, "Supplier" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LookupListings");
        }
    }
}
