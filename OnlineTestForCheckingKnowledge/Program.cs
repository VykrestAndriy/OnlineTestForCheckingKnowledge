using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Business.Mapping;
using OnlineTestForCheckingKnowledge.Business.Services;
using OnlineTestForCheckingKnowledge.Data;

var builder = WebApplication.CreateBuilder(args);

// Подключаем БД
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Добавляем сервисы
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddLogging(); // Добавляем логирование

// Добавляем контроллеры + Swagger
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Включаем Swagger в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "api",
        pattern: "api/{controller}/{action}/{id?}");
});

app.Run();
