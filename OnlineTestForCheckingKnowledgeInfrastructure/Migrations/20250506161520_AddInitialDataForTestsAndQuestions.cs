using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineTestForCheckingKnowledge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialDataForTestsAndQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tests",
                columns: new[] { "Id", "Name", "Title" },
                values: new object[] { 49, "Тест 1", "Тест 1" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "TestId", "Text" },
                values: new object[] { 1, 49, "Що таке ASP.NET MVC?" });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "IsCorrect", "Text" },
                values: new object[] { 1, 1, true, "Фреймворк для побудови веб-застосунків на основі моделі Model-View-Controller." });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "IsCorrect", "Text" },
                values: new object[] { 2, 1, false, "Бібліотека класів для роботи з базами даних." });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "IsCorrect", "Text" },
                values: new object[] { 3, 1, false, "Мова програмування для створення клієнтських сценаріїв." });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tests",
                keyColumn: "Id",
                keyValue: 49);
        }
    }
}