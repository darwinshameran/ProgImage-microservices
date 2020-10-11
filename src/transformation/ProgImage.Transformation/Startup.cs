using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgImage.Transformation.Domain.Persistence;
using ProgImage.Transformation.Domain.Persistence.Database;
using ProgImage.Transformation.Domain.Repositories;
using ProgImage.Transformation.Domain.Services;
using ProgImage.Transformation.Helpers;
using ProgImage.Transformation.RabbitMQ.Connection;
using ProgImage.Transformation.RabbitMQ.Services;
using ProgImage.Transformation.Services;
using Serilog;

namespace ProgImage.Transformation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IWebHostEnvironment env)
        {

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    $"Host={EnvVariables.DatabaseHostname}" +
                    $";Port={EnvVariables.DatabasePort}" +
                    $";Database={EnvVariables.DatabaseName}" +
                    $";Username={EnvVariables.DatabaseUsername}" +
                    $";Password={EnvVariables.DatabasePassword}")
            );
            
            services.AddControllers();

            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IThumbnailService, ThumbnailService>();
            services.AddScoped<IBlurService, BlurService>();
            services.AddScoped<ICompressService, CompressService>();
            services.AddScoped<IRotateService, RotateService>();
            services.AddScoped<IMaskService, MaskService>();

            services.AddScoped<IStatusRepository, StatusRepository>();

            services.AddSingleton<IRabbitMqConnection, RabbitMqConnection>();
            services.AddSingleton<IConsumer, Consumer>();
            services.AddSingleton<IProducer, Producer>();
            
            services.AddAutoMapper(GetType().Assembly);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseRabbitListener();

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