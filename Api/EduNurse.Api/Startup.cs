﻿using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using EduNurse.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduNurse.Api
{
    public class Startup
    {
        public IContainer Container { get; private set; }
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ConfigureAuthentication(Configuration);

            if (!HostingEnvironment.IsEnvironment("Testing"))
            {
                services.ConfigureSwagger();
            }

            Container = services.ConfigureContainer(Configuration);
            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime appLifetime)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            if (!HostingEnvironment.IsEnvironment("Testing"))
            {
                app.ConfigureSwagger();
            }

            app.UseCors(cors =>
            {
                cors.AllowAnyOrigin();
                cors.AllowAnyMethod();
                cors.AllowAnyHeader();
            });

            app.ConfigureMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
            appLifetime.ConfigureContainer(Container);
        }
    }
}
