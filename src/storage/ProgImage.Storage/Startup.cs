using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgImage.Storage.Domain.Persistence;
using ProgImage.Storage.Domain.Persistence.Database;
using ProgImage.Storage.Domain.Repositories;
using ProgImage.Storage.Domain.Services;
using ProgImage.Storage.Helpers;
using ProgImage.Storage.Services;
using Serilog;

namespace ProgImage.Storage
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    $"Host={EnvVariables.DatabaseHostname}" +
                    $";Database={EnvVariables.DatabaseName}" +
                    $";Username={EnvVariables.DatabaseUsername}" +
                    $";Password={EnvVariables.DatabasePassword}")
            );
            
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IStorageService, StorageService>();

            services.AddAutoMapper(GetType().Assembly);
            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            using IServiceScope serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            AppDbContext context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            
            try
            {
                context.Database.Migrate();
            }
            catch (Exception)
            {
                Log.Information("[Database] No migrations to run.");
            }
        }
    }
}