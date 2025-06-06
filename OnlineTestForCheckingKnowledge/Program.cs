using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineTestForCheckingKnowledge.Business.Services;
using OnlineTestForCheckingKnowledge.Data;
using OnlineTestForCheckingKnowledge.Data.Entities;
using OnlineTestForCheckingKnowledge.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using OnlineTestForCheckingKnowledge.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("OnlineTestForCheckingKnowledge.Infrastructure")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddRoles<IdentityRole>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();

builder.Services.AddScoped<IRepository<Test>, TestRepository>();
builder.Services.AddScoped<IRepository<Question>, QuestionRepository>();
builder.Services.AddScoped<IRepository<Answer>, AnswerRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddLogging();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("uk-UA"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("uk-UA");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    options.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider());
    options.RequestCultureProviders.Insert(2, new AcceptLanguageHeaderRequestCultureProvider());
});

builder.Services.AddRazorPages()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.AddControllersWithViews();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await CreateRolesAndAdminUser(scope.ServiceProvider);
}

var localizationOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// ����� ��� API ����������
app.MapControllers();

// ��������� ������� ��� MVC ���������� - ������ �� ������� �����
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// ��������� ������� ��� StartTest
app.MapControllerRoute(
    name: "startTest",
    pattern: "Test/StartTest/{testId:int}",
    defaults: new { controller = "Test", action = "StartTest" });

app.Run();

async Task CreateRolesAndAdminUser(IServiceProvider serviceProvider)
{
    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();

    // ��������� ��� "Admin", ���� �� ����
    var adminRoleExists = await RoleManager.RoleExistsAsync("Admin");
    if (!adminRoleExists)
    {
        await RoleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // ������ ����������� �� email
    var adminUser = await UserManager.FindByEmailAsync("newadmin@example.com");

    if (adminUser != null)
    {
        // ���������, �� ���������� ��� � ��� "Admin"
        if (!await UserManager.IsInRoleAsync(adminUser, "Admin"))
        {
            // ������ ����������� �� ��� "Admin"
            var addToRoleResult = await UserManager.AddToRoleAsync(adminUser, "Admin");
            if (addToRoleResult.Succeeded)
            {
                Console.WriteLine("����������� newadmin@example.com ������ �� ��� Admin.");
            }
            else
            {
                foreach (var error in addToRoleResult.Errors)
                {
                    Console.WriteLine($"������� ��������� �� ���: {error.Description}");
                }
            }
        }
        else
        {
            Console.WriteLine("���������� newadmin@example.com ��� �� ���� Admin.");
        }

        // **��������� ������ ������ (���� ��� ����������!)**
        var resetToken = await UserManager.GeneratePasswordResetTokenAsync(adminUser);
        var changePasswordResult = await UserManager.ResetPasswordAsync(adminUser, resetToken, "YourNewStrongPassword!"); // ������� �� ��� ������
        if (changePasswordResult.Succeeded)
        {
            Console.WriteLine("������ ��� newadmin@example.com ��������� ������ �� YourNewStrongPassword!");
        }
        else
        {
            foreach (var error in changePasswordResult.Errors)
            {
                Console.WriteLine($"������� ���� ������: {error.Description}");
            }
        }
    }
    else
    {
        Console.WriteLine("����������� newadmin@example.com �� ��������.");
    }
}