using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options =>
                {
                options.AddDefaultPolicy
                    (builder => 
                        {
                        builder
                        .AllowAnyHeader()
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
                    });
                });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.Use(function(req, res, next) {
            //    res.header("Access-Control-Allow-Origin", "*");
            //    res.header("Access-Control-Allow-Methods", "GET, PUT, POST");
            //    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
            //    next();
            //});

            //app.UseCors();
            //app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseCors(builder =>
            builder.WithOrigins(
            "http://localhost:8081",
            "https://localhost:8081"));
            app.UseMvc();
        }
    }
}
