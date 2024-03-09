using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedSocial.infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class FixImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "Identity",
                table: "Users");
        }
    }
}
