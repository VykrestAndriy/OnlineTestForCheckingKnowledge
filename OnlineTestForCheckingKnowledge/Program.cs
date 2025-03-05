using Microsoft.EntityFrameworkCore;
using OnlineTestForCheckingKnowledge.Data;
using OnlineTestForCheckingKnowledge.Business.Services;
using AutoMapper;
using OnlineTestForCheckingKnowledge.Business.Mapping;


var builder = WebApplication.CreateBuilder(args);

// ���������� ��
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ��������� �������
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddLogging(); // ��������� �����������

// ��������� ����������� + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// �������� Swagger � ������ ����������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();