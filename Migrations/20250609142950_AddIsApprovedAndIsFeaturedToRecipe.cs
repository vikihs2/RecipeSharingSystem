using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSharingSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddIsApprovedAndIsFeaturedToRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Recipes");
        }
    }
}
