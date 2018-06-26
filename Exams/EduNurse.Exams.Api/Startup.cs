using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using EduNurse.Exams.Api.Repositories;
using EduNurse.Exams.Shared.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace EduNurse.Exams.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<ExamsContext>(options =>
            {
                options.UseInMemoryDatabase("Testing");
            });
            services.AddScoped<IExamsContext>(provider => provider.GetService<ExamsContext>());

            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<IExamsRepository, ExamsRepository>();

            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "EduNurse - Exams", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EduNurse - Exams");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
