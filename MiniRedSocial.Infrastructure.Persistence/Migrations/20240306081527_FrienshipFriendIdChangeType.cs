using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedSocial.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FrienshipFriendIdChangeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FriendId",
                table: "Friendships",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FriendId",
                table: "Friendships",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
