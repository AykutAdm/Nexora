using Nexora.WebUI.Services.CryptoPriceServices;
using Nexora.WebUI.Services.DestinationServices;
using Nexora.WebUI.Services.GasPriceServices;
using Nexora.WebUI.Services.ExchangeRateService;
using Nexora.WebUI.Services.HotelServices;
using Nexora.WebUI.Services.WeatherForecastServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IDestinationService, DestinationService>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddScoped<IHotelSearchService, HotelSearchService>();
builder.Services.AddScoped<IHotelDetailService, HotelDetailService>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddScoped<ICryptoPriceService, CryptoPriceService>();
builder.Services.AddScoped<IGasPriceService, GasPriceService>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
