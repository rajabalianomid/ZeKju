using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeKju.Data;
using ZeKju.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ZeKju.Data.Repository;
using ZeKju.Service;
using ZeKju.App.Commands;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ZeKju.App.Constants;
using Microsoft.Extensions.Logging;

namespace ZeKju.App.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceProvider ManageContext(this IServiceScope scope)
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ZeKjuContext>();
                bool isExist = context.GetService<IDatabaseCreator>().CanConnect();
                if (!isExist)
                {
                    context.Database.Migrate();
                }
                if (!context.Routes.Any())
                {
                    var seeder = services.GetRequiredService<IDatabaseSeeder>();
                    Console.Write(AppConstants.Message_ImportDirectory);
                    var path = Console.ReadLine();
                    seeder.Initialize(path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return services;
        }
        internal static IHostBuilder Config(this IHostBuilder builder, IConfigurationRoot configuration)
        {
            return builder.ConfigureServices((context, services) =>
             {
                 services.AddDatabase(configuration);
                 services.AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>));
                 services.AddSingleton<IFlightService, FlightService>();
                 services.AddSingleton<IExitCommand, ExitCommand>();
                 services.AddSingleton<IExportCommand, ExportCommand>();
                 services.AddSingleton<IHelpCommand, HelpCommand>();
                 services.AddSingleton<INoneCommand, NoneCommand>();
             });
        }
        internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<ZeKjuContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")).UseLoggerFactory(LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.None).AddConsole();
        }))).AddTransient<IDatabaseSeeder, DatabaseSeeder>();
    }
}
