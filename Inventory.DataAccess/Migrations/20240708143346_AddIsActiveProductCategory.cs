using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductCategories");
        }
    }
}
