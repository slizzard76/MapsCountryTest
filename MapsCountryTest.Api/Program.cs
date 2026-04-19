using AutoMapper;
using DataAccess.Context.Models;

using MapsCountryTest.Api.Repository.Abstraction;
using MapsCountryTest.Api.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MediatR;
using MapsCountryTest.Api.MapProfiles;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var postgresConnectionString = builder.Configuration.GetConnectionString("PostgresConnection");
        builder.Services.AddDbContext<MapsCountryTestDbContext>(options =>
             options.UseNpgsql(postgresConnectionString, o => o.MapEnum<OfficeType>(nameof(OfficeType))));

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        AddApplicationMappings(builder.Services, loggerFactory);

        builder.Services.AddMediatR(typeof(Program));
        
        builder.Services.AddScoped<IOfficeRepository, OfficeRepository>();

        builder.Services.AddEndpointsApiExplorer(); 

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MapCountry API",
                Version = "v1",
                Description = "API для теста."
            });
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
        
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MapCountry API");
        
            c.RoutePrefix = string.Empty; 
        });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void AddApplicationMappings(IServiceCollection services, ILoggerFactory loggerFactory)
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.AddMaps(typeof(MappingProfile));
        }, loggerFactory);
        mapperConfig.AssertConfigurationIsValid();

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}