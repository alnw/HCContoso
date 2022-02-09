using GQL.Extensions;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var azure_root = $"{Environment.GetEnvironmentVariable("HOME")}/site/wwwroot";
            var visual_studio_root = Environment.CurrentDirectory;

            var actual_root =  azure_root;

            if (!Directory.Exists(actual_root))
            {
                actual_root = visual_studio_root;
            }

            Configuration = GetConfiguration(actual_root);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddCors(o =>
                    o.AddDefaultPolicy(b =>
                        b.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin()));

            services.AddDemoGraphQLServer(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors();

            app.UseWebSockets();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                // We will be using the new routing API to host our GraphQL middleware.
                endpoints.MapGraphQL();
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/graphql", true);
                    return Task.CompletedTask;
                });
            });
        }
        private static IConfiguration GetConfiguration(string appDirectory)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(appDirectory)
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return config;
        }
    }
}
