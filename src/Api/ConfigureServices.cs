
using Application.Contract.Common.Interfaces;
using EShop.Application.Services;

namespace Api;

public static class ConfigureServices
{ 
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        //services.AddHealthChecks()
        //    .AddDbContextCheck<ApplicationDbContext>();

        //services.AddControllersWithViews(options =>
        //    options.Filters.Add<ApiExceptionFilterAttribute>())
        //        .AddFluentValidation(x => x.AutomaticValidationEnabled = false);
        
        // Customise default API behaviour
        //services.Configure<ApiBehaviorOptions>(options =>
        //    options.SuppressModelStateInvalidFilter = true);

        //services.AddOpenApiDocument(configure =>
        //{
        //    configure.Title = "s API";
        //});

        return services;
    }
}
