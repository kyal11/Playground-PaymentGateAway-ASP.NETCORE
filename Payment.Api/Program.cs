using Microsoft.EntityFrameworkCore;
using Payment.Application;
using Payment.Infrastructure;
using Payment.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        if (context.Database.CanConnect() && app.Environment.IsDevelopment())
        {
            logger.LogInformation("Database terhubung dengan sukses");
            //context.Database.Migrate();
            //logger.LogInformation("Migrate Berjalan");
        }
        else
        {
            logger.LogWarning("Database tidak bisa dihubungi");
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Gagal koneksi database");
    }
}

app.Run();
