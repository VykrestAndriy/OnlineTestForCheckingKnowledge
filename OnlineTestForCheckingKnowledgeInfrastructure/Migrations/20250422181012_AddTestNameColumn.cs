using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTestForCheckingKnowledge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTestNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tests");
        }
    }
}
