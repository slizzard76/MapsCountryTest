using DataAccess.Context.Models;
using MapsCountryTest.Service;
using Microsoft.EntityFrameworkCore;

var app = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("connectionstring.json",
            optional: true,
            reloadOnChange: true);
    })
    .ConfigureServices((hostContext,  services) =>
    {

        var postgresConnectionString = hostContext.Configuration.GetConnectionString("PostgresConnection");
        services.AddDbContext<MapsCountryTestDbContext>(options =>
        //Добавляем ENUM как тип в базу.
        options.UseNpgsql(postgresConnectionString, o => o.MapEnum<OfficeType>(nameof(OfficeType))));
        services.AddScoped<TerminalSyncWorker>();
        services.AddHostedService<TerminalSyncBackgroundService>();
        //Для graceful shutdown, ибо по условию время загрузки данных < 5 мин.
        services.Configure<HostOptions>(opts => opts.ShutdownTimeout = TimeSpan.FromMinutes(5));
    }).Build();

// Убедимся, что база данных готова (Миграции и создание таблиц)
// Вообще по хорошему это надо выносить в отдельный проект миграции, но в рамках тестового задания я не стал этого делать.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MapsCountryTestDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

await app.RunAsync();
