using System;
using System.Collections.Generic;
using System.IO;
using JavaScriptViewEngine;
using JavaScriptViewEngine.Pool;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AirDoc.Models;
using AirDoc.Services;

namespace AirDoc
{
    public class Startup
    {
        IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            // Set up configuration sources.
            var builder = JsonConfigurationExtensions.AddJsonFile(JsonConfigurationExtensions.AddJsonFile(FileConfigurationExtensions.SetBasePath(new ConfigurationBuilder(), env.ContentRootPath), "appsettings.json"), $"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            EnvironmentVariablesExtensions.AddEnvironmentVariables(builder);
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddJsEngine();
            services.Configure<RenderPoolOptions>(options =>
            {
                options.WatchPath = _env.WebRootPath;
                options.WatchFiles = new List<string>
                {
                     Path.Combine(_env.WebRootPath, "pack", "server.generated.js")
                };
                options.WatchDebounceTimeout = (int)TimeSpan.FromSeconds(2).TotalMilliseconds;
            });
            services.Configure<NodeRenderEngineOptions>(options =>
            {
                options.ProjectDirectory = Path.Combine(_env.WebRootPath, "pack");
                options.GetArea = (area) =>
                {
                    switch (area)
                    {
                        case "default":
                            return "server.generated";
                        default:
                            return area;
                    }
                };
            });

            // Add framework services.
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<ISmsSender, SmsSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStatusCodePagesWithReExecute("/Status/Status/{0}");

            app.UseStaticFiles();

            app.UseIdentity();

            var googleClientId = Configuration["Authentication:Google:ClientId"];
            var googleClientSecret = Configuration["Authentication:Google:ClientSecret"];
            if (!string.IsNullOrEmpty(googleClientId) && !string.IsNullOrEmpty(googleClientSecret))
            {
                var options = new GoogleOptions();
                options.ClientId = googleClientId;
                options.ClientSecret = googleClientSecret;
                options.Scope.Add("email");
                options.Scope.Add("profile");
            }

            var facebookAppId = Configuration["Authentication:Facebook:AppId"];
            var facebookAppSecret = Configuration["Authentication:Facebook:AppSecret"];
            if (!string.IsNullOrEmpty(facebookAppId) && !string.IsNullOrEmpty(facebookAppSecret))
            {
                app.UseFacebookAuthentication(new FacebookOptions
                {
                    AppId = facebookAppId,
                    AppSecret = facebookAppSecret
                });
            }

            app.UseJsEngine();

            app.UseMvc(routes =>
            {
                MapRouteRouteBuilderExtensions.MapRoute(routes, name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Use((context, next) => {
                context.Response.StatusCode = 404;
                return next();
            });
        }
    }
}
