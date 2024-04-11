using Microsoft.EntityFrameworkCore;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.Services;
//using TouragencyWebApi.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(); // добавляем сервисы CORS


// Получаем строку подключения из файла конфигурации
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSwaggerGen();
builder.Services.AddTouragencyContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddScoped<IBedConfigurationService, BedConfigurationService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPhoneService, PhoneService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IReviewImageService, ReviewImageService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ISettlementService, SettlementService>();
builder.Services.AddScoped<ITourStateService, TourStateService>();
builder.Services.AddScoped<ITourNameService, TourNameService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITransportTypeService, TransportTypeService>();



var app = builder.Build();
// настраиваем CORS
app.UseCors(builder => builder.AllowAnyOrigin());
//app.UseCors(builder => builder.WithOrigins("https://localhost:7110")
//                            .AllowAnyHeader()
//                            .AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
