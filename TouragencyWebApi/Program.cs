using Microsoft.EntityFrameworkCore;
using TouragencyWebApi.BLL.Infrastructure;
using TouragencyWebApi.BLL.Interfaces;
using TouragencyWebApi.BLL.Services;
//using TouragencyWebApi.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(); // äîáàâëÿåì ñåðâèñû CORS


// Ïîëó÷àåì ñòðîêó ïîäêëþ÷åíèÿ èç ôàéëà êîíôèãóðàöèè
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// äîáàâëÿåì êîíòåêñò ApplicationContext â êà÷åñòâå ñåðâèñà â ïðèëîæåíèå
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSwaggerGen();
builder.Services.AddTouragencyContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddScoped<IBedConfigurationService, BedConfigurationService>();
builder.Services.AddScoped<IBookingChildrenService, BookingChildrenService>();
builder.Services.AddScoped<IBookingDataService, BookingDataService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IHotelConfigurationService, HotelConfigurationService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IHotelServiceService, HotelServiceService>();
builder.Services.AddScoped<IHotelServiceTypeService, HotelServiceTypeService>();
builder.Services.AddScoped<IPhoneService, PhoneService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IReviewImageService, ReviewImageService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ISettlementService, SettlementService>();
builder.Services.AddScoped<ITourStateService, TourStateService>();
builder.Services.AddScoped<ITouragencyAccountRoleService, TouragencyAccountRoleService>();
builder.Services.AddScoped<ITouragencyEmployeeService, TouragencyEmployeeService>();
builder.Services.AddScoped<ITouragencyAccountService, TouragencyAccountService>();
builder.Services.AddScoped<ITourNameService, TourNameService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITransportTypeService, TransportTypeService>();




var app = builder.Build();
// íàñòðàèâàåì CORS
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
