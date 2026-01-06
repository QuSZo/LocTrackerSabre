using Location.Cloud.DependencyInjection;
using Location.Database.DependencyInjection;
using Location.DeviceService;
using Location.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCloud();
builder.Services.AddDatabase();
builder.Services.AddSingleton<LocationPersistence>();
builder.Services.AddHostedService<LocationService>();
builder.Services.AddControllers();

builder.Services.AddCorsPolicy();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();
app.UseCors();

app.MapGet("/", () => "Location WebAPI");
app.MapControllers();

app.Run("http://0.0.0.0:8080");