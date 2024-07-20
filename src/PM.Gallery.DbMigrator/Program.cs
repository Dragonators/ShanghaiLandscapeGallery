using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PM.Gallery.Application.SeedData;
using PM.Gallery.Infrastructure;
using PM.Gallery.Infrastructure.EntityFrameworkCore;

var host = CreateHostBuilder(args).Build();
using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

        var seedDataContributor = services.GetRequiredService<SeedDataContributor>();
        await seedDataContributor.SeedAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            Console.WriteLine(context.HostingEnvironment.ContentRootPath);
            config.SetBasePath(context.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((context, services) =>
        {
            //注册 持久化工具
            services.AddInfrastructureServices(context.Configuration);

            // 注册 SeedDataContributor
            services.AddScoped<SeedDataContributor>();
        });