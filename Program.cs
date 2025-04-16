using Concierge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Concierge;

/// <summary>
/// The Main Program.
/// </summary>
public static class Program
{
    /// <summary>
    /// The Main.
    /// </summary>
    private static async Task Main(string[] args)
    {
        // the logger
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            // .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Log.Information("Starting up ..");

        // the builder of the host
        Log.Debug("Creating the host builder ..");
        var builder = Host.CreateDefaultBuilder(args);
        
        // configure the builder
        builder.ConfigureHostConfiguration(config =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory());
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddEnvironmentVariables();
        });
        
        // configure the services
        builder.ConfigureServices((context, services) =>
        {
            // configure the app db context
            services.AddDbContext<AppDbContext>(options =>
            {
                var conn = context.Configuration.GetConnectionString("DefaultConnection");
                if (string.IsNullOrEmpty(conn))
                {
                    throw new Exception("Connection string 'DefaultConnection' not found.");
                }
                
                var path = Path.GetDirectoryName(conn.Replace("Data Source=", string.Empty));
                if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                options.UseSqlite(conn);
                
                if (context.HostingEnvironment.IsDevelopment())
                {
                    options.EnableSensitiveDataLogging();
                }
                
            });
        });
        
        // configure the logging
        builder.ConfigureLogging((context, logging) =>
        {
            logging.ClearProviders();
            logging.AddSerilog();
            logging.AddConfiguration(context.Configuration.GetSection("Logging"));
        });
        
        // build the host
        Log.Debug("Building the host ..");
        var host = builder.Build();
        
        // run the app
        Log.Debug("Running the host ..");
        await host.RunAsync();

        Log.Information("Done.");
    }
}