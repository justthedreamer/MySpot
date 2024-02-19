using MySpot.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ReservationsService>();

var app = builder.Build();
app.MapControllers();
app.Run();