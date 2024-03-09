using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniRedSocial.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RipUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Users_UserId",
                table: "Friendships");

            migrationBuilder.DropForeignKey(
                name: "FK_Hilos_Users_UserId",
                table: "Hilos");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Publication_PublicationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Publication_Users_UserId",
                table: "Publication");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Hilos_UserId",
                table: "Hilos");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_UserId",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publication",
                table: "Publication");

            migrationBuilder.DropIndex(
                name: "IX_Publication_UserId",
                table: "Publication");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Hilos");

            migrationBuilder.RenameTable(
                name: "Publication",
                newName: "Publications");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publications",
                table: "Publications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Publications_PublicationId",
                table: "Messages",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Publications_PublicationId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publications",
                table: "Publications");

            migrationBuilder.RenameTable(
                name: "Publications",
                newName: "Publication");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Hilos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publication",
                table: "Publication",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Validate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Hilos_UserId",
                table: "Hilos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserId",
                table: "Friendships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Publication_UserId",
                table: "Publication",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Users_UserId",
                table: "Friendships",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hilos_Users_UserId",
                table: "Hilos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Publication_PublicationId",
                table: "Messages",
                column: "PublicationId",
                principalTable: "Publication",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Publication_Users_UserId",
                table: "Publication",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
