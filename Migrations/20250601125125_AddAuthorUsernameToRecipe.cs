using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeSharingSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorUsernameToRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorUsername",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorUsername",
                table: "Recipes");
        }
    }
}
