using Buddy.Configuration;
using Buddy.EntityFrameworkCore;
using Buddy.Localization;
using Buddy.Logging;
using Buddy.Modularity;
using Buddy.MultiTenancy;
using Buddy.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Buddy.Web;

[DependsOn(
    typeof(BuddyWebMvcCoreModule),
    typeof(BuddyConfigurationModule),
    typeof(BuddyLocalizationModule),
    typeof(BuddyLoggingModule),
    typeof(BuddyMultiTenancyModule),
    typeof(BuddyUsersModule)
)]
public class BuddyWebMvcModule : BuddyModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();

        var env = serviceProvider.GetService<IWebHostEnvironment>();
        var configuration = serviceProvider.GetService<IConfiguration>();

        services.AddDbContext<BuddyDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddMvc()
            .AddRazorRuntimeCompilation();

        services.ConfigureBuddyModuleRazorRuntimeCompilation(env);

        // services.Configure<RazorViewEngineOptions>(options =>
        // {
        //     options.ViewLocationExpanders.Add(new BuddyViewLocationExpander());
        // });

        //services.Configure<RazorPagesOptions>(options => { options.RootDirectory = "/Web/Pages"; });
    }

    public override void Configure(IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetService<IWebHostEnvironment>();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapRazorPages();
        });
    }
}