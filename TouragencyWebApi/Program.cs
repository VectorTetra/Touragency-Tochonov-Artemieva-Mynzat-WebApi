using Microsoft.EntityFrameworkCore;
using TouragencyWebApi.Models;
using TouragencyWebApi.EFContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(); // добавляем сервисы CORS


// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<TouragencyContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();
// настраиваем CORS
//app.UseCors(builder => builder.AllowAnyOrigin());

app.UseCors(builder => builder.WithOrigins("https://localhost:7110")
                            .AllowAnyHeader()
                            .AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
