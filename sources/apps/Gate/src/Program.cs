using Gate.Cloud;
using Gate.Cloud.DependencyInjection;
using Gate.Simulators;
using Google.Cloud.PubSub.V1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCloud();
builder.Services.AddSingleton<DeviceSimulator>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "Gate WebAPI");
app.MapControllers();

app.Run("http://0.0.0.0:8080");