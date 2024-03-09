using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedSocial.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixFromHilo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Hilos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hilos");
        }
    }
}
