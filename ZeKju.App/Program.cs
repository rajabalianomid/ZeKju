using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ZeKju.App;
using ZeKju.App.Constants;
using ZeKju.App.Extensions;
using ZeKju.Data.Helper;

public class Program
{
    public async static Task Main()
    {
        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Critical + 1).AddConsole();
        });

        var logger = loggerFactory.CreateLogger("MyApp");
        if (!File.Exists(AppConstants.App_Data))
        {
            Console.WriteLine(AppConstants.Message_Installing);
            Console.Write(AppConstants.Message_ConfigConnectionString);
            var connectionString = Console.ReadLine();
            FileHelper.CreateAppSettings(connectionString);
        }
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(AppConstants.App_Data, optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        var host = CreateHostBuilder().Config(configuration).Build();
        using var scope = host.Services.CreateScope();
        var services = scope.ManageContext();
        await new Startup(services).RunAsync();
    }

    public static IHostBuilder CreateHostBuilder() => Host.CreateDefaultBuilder();
}
