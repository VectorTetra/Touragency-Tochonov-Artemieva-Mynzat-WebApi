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
//builder.Services.AddSession(opt =>
//{
//    opt.IdleTimeout = TimeSpan.FromMinutes(30);
//    opt.Cookie.Name = "Session";
//});
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSwaggerGen();
builder.Services.AddTouragencyContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddScoped<IBedConfigurationService, BedConfigurationService>();
builder.Services.AddScoped<IBookingChildrenService, BookingChildrenService>();
builder.Services.AddScoped<IBookingDataService, BookingDataService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IContinentService, ContinentService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IHotelConfigurationService, HotelConfigurationService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IHotelImageService, HotelImageService>();
builder.Services.AddScoped<IHotelServiceService, HotelServiceService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IHotelServiceTypeService, HotelServiceTypeService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPhoneService, PhoneService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IReviewImageService, ReviewImageService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ISettlementService, SettlementService>();
builder.Services.AddScoped<ITourStateService, TourStateService>();
builder.Services.AddScoped<ITourNameService, TourNameService>();
builder.Services.AddScoped<ITourImageService, TourImageService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITransportTypeService, TransportTypeService>();
builder.Services.AddScoped<ITouragencyAccountRoleService, TouragencyAccountRoleService>();
builder.Services.AddScoped<ITouragencyEmployeeService, TouragencyEmployeeService>();
builder.Services.AddScoped<ITouragencyAccountService, TouragencyAccountService>();



var app = builder.Build();
// настраиваем CORS
//app.UseSession();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
//app.UseCors(builder => builder.WithOrigins("https://localhost:7110")
//                            .AllowAnyHeader()
//                            .AllowAnyMethod());

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
