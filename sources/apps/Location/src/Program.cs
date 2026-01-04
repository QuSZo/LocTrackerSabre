using Location.Cloud.DependencyInjection;
using Location.DeviceService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCloud();
builder.Services.AddSingleton<LocationPersistence>();
builder.Services.AddHostedService<LocationService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "Location WebAPI");
app.MapControllers();

app.Run("http://0.0.0.0:8080");