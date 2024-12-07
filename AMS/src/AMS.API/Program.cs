using AMS.API.Infrastructure.AutofacHandler;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Serilog;
using AMS.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AMS.API.Infrastructure.Hosting;
using AutoMapper;
using AMS.Infrastructure.AutoMapper;

var configuration = GetConfiguration();
Log.Logger = CreateSerilogLogger(configuration);

var builder = WebApplication.CreateBuilder(args);

#region Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AMSProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AMS",
        Version = "v1",
        Description = "Appointment Management API"
  
    });
});

#region DbContext & Settings
AddCustomDbContext(builder.Services);
#endregion

#region Auto fac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MediatorModule()));
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ApplicationModule()));
#endregion


var app = builder.Build();


#region Migration
Log.Information("Applying migrations ({ApplicationContext})...");
app.MigrateDbContext<AMSContext>((context, services) =>
{
    context.Database.Migrate();
    
});

Log.Information("Starting web host ({ApplicationContext})...");

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


IServiceCollection AddCustomDbContext(IServiceCollection services)
{
    string connString = configuration["ConnectionString"];
    string envConnString = Environment.GetEnvironmentVariable("ConnectionStrings__DBConString");
    if (!string.IsNullOrWhiteSpace(envConnString))
    {
        connString = envConnString;
    }

    services.AddDbContext<AMSContext>(options =>
    {
        options.UseSqlServer(connString,
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });
    },
    ServiceLifetime.Scoped  
    );


    return services;
}


IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    var seqServerUrl = configuration["Serilog:SeqServerUrl"];
    var logstashUrl = configuration["Serilog:LogstashgUrl"];
    var logPath = Path.Combine(configuration["Serilog:LogPath"], ".log");
    int.TryParse(configuration["Serilog:FileLogLevel"], out int fileLogLevel);
    int.TryParse(configuration["Serilog:RollingInterval"], out int rollingInterval);

    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", "AMS.APi")
        .Enrich.FromLogContext()

        .CreateLogger();
}


