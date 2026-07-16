using ChinookDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

try
{
    var builder = Host.CreateApplicationBuilder(args);

    // Configure Serilog from host configuration
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(dispose: true);

    Log.Information("Starting application");

    // Resolve database path relative to the project directory
    var projectDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
    var dbPath = Path.Combine(projectDir, "data", "chinookDb.db");
    Log.Information("Project directory: {ProjectDir}", projectDir);
    Log.Information("Database path: {DbPath}", dbPath);
    Log.Information("File exists: {Exists}", File.Exists(dbPath));

    // Register DbContext with DI
    builder.Services.AddDbContext<ChinookDbContext>(options =>
        options.UseSqlite($"Data Source={dbPath}"));

    // Register the main worker as a hosted service
    builder.Services.AddHostedService<ChinookWorker>();

    var host = builder.Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public class ChinookWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<ChinookWorker> _logger;

    public ChinookWorker(IServiceScopeFactory scopeFactory, ILogger<ChinookWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ChinookDbContext>();

        _logger.LogInformation("=== Artists ===");
        var artists = await context.Artists.Take(10).ToListAsync(stoppingToken);
        foreach (var artist in artists)
        {
            _logger.LogInformation("{ArtistId}: {Name}", artist.ArtistId, artist.Name);
        }

        _logger.LogInformation("Total artists: {Count}", await context.Artists.CountAsync(stoppingToken));
        _logger.LogInformation("Total albums: {Count}", await context.Albums.CountAsync(stoppingToken));
        _logger.LogInformation("Total tracks: {Count}", await context.Tracks.CountAsync(stoppingToken));
        _logger.LogInformation("Total customers: {Count}", await context.Customers.CountAsync(stoppingToken));
        _logger.LogInformation("Total invoices: {Count}", await context.Invoices.CountAsync(stoppingToken));
    }
}