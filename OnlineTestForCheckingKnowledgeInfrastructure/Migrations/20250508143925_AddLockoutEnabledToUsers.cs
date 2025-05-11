using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTestForCheckingKnowledge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLockoutEnabledToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "AspNetUsers");
        }
    }
}