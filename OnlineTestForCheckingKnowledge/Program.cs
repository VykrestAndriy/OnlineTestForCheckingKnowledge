using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Business.Mapping;
using OnlineTestForCheckingKnowledge.Business.Services;
using OnlineTestForCheckingKnowledge.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ���������� ��
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� �������
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddLogging(); 

// ��������� �����������
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// ��������� ����������� + Swagger
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var supportedCultures = new[] {
    new CultureInfo("en-US"),
    new CultureInfo("uk-UA")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("uk-UA"), // �������� �� �������������
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>
{
    new QueryStringRequestCultureProvider(),
    new CookieRequestCultureProvider(),
    new AcceptLanguageHeaderRequestCultureProvider()
};

app.UseRequestLocalization(localizationOptions);

// �������� Swagger � ������ ����������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

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

