﻿using System.Reflection;

using EShop.Domain.Common;
using EShop.Infrastructure.Common;
using EShop.Infrastructure.Persistence;

using EShop;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EShop.Domain;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Contract.Common.Models;
using EShop.Infrastructure.Persistence.Interceptors;

namespace EShop.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

  


        services.AddIdentity<User, Role>()
        .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddCookie()
               .AddJwtBearer(jwtBearerOptions =>
               {
                   jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateActor = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = configuration["Issuer"],
                       ValidAudience = configuration["Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                           (configuration["SigningKey"]))
                   };
               });


        services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 0;
            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 1000;
            options.Lockout.AllowedForNewUsers = true;
            // User settings
            options.User.RequireUniqueEmail = false;
            options.SignIn.RequireConfirmedEmail = false;
        });





        services.Configure<AppSettings>(configuration);



        ResolveAllTypes(services, ServiceLifetime.Scoped, typeof(BaseRepository<>), "Repository");
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        
        return services;
    }

    public static void ResolveAllTypes(IServiceCollection services, ServiceLifetime serviceLifetime, Type refType, string suffix)
    {

        var assemblyCurrent = refType.GetTypeInfo().Assembly;
        var allServices = assemblyCurrent.GetTypes().Where(t =>
            t.GetTypeInfo().IsClass &&
            !t.GetTypeInfo().IsAbstract &&
            !t.GetType().IsInterface &&
            t.Name.EndsWith(suffix)
        );
       

        foreach (var type in allServices)
        {
            var allInterfaces = type.GetInterfaces();
            var mainInterfaces = allInterfaces.Except
                (allInterfaces.SelectMany(t => t.GetInterfaces()));
            foreach (var itype in mainInterfaces)
            {
                if (allServices.Any(x => !x.Equals(type) && itype.IsAssignableFrom(x)))
                {
                    throw new Exception("The " + itype.Name +
                                        " type has more than one implementations, please change your filter");
                }
                services.Add(new ServiceDescriptor(itype, type, serviceLifetime));
            }
        }
    }
}
