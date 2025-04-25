using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTestForCheckingKnowledge.Infrastructure.Migrations
{
    public partial class AddCorrectAnswerIdToQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerId",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswerId",
                table: "Questions");
        }
    }
}
