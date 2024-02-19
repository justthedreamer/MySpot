using MySpot.Api.DTO;
using MySpot.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ReservationsService>();
builder.Services.AddSingleton<IClock, Clock>();

var app = builder.Build();
app.MapControllers();
app.Run();