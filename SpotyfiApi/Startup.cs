using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
//
using SpotyfiLib.SpotyfiLib.Infrastructure.Cross_Cutting.LoggingFac;
using SpotyfiLib.Infrastructure.Repositories;
using SpotyfiLib.Infrastructure.UnitOfWork;
using SpotyfiLib.Domain.ArtistAgg;
using SpotyfiLib.Domain.RecordAgg;
using SpotyfiLib.Domain.TrackAgg;
using SpotyfiLib.Domain.AlbumAgg;
using SpotyfiLib.Application;
//
using PostgresSink;
using Serilog;
using AutoMapper;
using Npgsql;
//Authentication Purposes
using AspNet.Security.OpenIdConnect.Primitives;
using OpenIddict.Core;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace SpotyfiApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            // Log.Logger = new LoggerConfiguration()
            //     .Enrich.FromLogContext()
            //     .WriteTo.PostgreSqlServer(Configuration.GetConnectionString("fed"), "logs")
            //     .WriteTo.LiterateConsole()
            //     .CreateLogger();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Connect to database
            var connection = new NpgsqlConnection(Configuration.GetConnectionString("fed")); //What does this do? //dapat "spotify"

            services.AddDbContext<FirstGenUnitOfWork>(option => option.UseNpgsql(connection));
            services.AddDbContext<SecondGenUnitOfWork>(option => option.UseNpgsql(connection));

            //Dependency Injection:
            services.AddScoped<MainUnitOfWork, MainUnitOfWork>();

            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IRecordRepository, RecordRepository>();

            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IArtistRepository, ArtistRepository>();

            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<ITrackRepository, TrackRepository>();

            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();

            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            // Add framework services.
            services.AddMvc();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseMvc();

            //Manual
            FedLogger.LoggerFactory = loggerFactory.AddSerilog();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://192.168.143.180:4200", "http://localhost:4200");
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        }
    }
}
