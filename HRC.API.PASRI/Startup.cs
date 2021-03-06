﻿using AutoMapper;
using HRC.DB.Master.Core;
using HRC.DB.Master.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;

namespace HRC.API.PASRI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add a database context to the collection of services
            // May be overridden by derived classes (e.g. test startups)
            AddDatabaseServices(services);

            // Add the automapper service for transforming DTOs to DMOs and vice versa 
            services.AddAutoMapper();

            // Add the API documentation services (e.g. Swagger)
            AddApiDocumentationServices(services);

            // Add the MVC service to the ASP.NET Core 2.1 version
            services.AddMvc(options =>
                {
                    options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                    options.RespectBrowserAcceptHeader = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });
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
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["AppSettings:VirtualDirectory"] + "swagger/v1/swagger.json", 
                    "PASR-Identity API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        [ExcludeFromCodeCoverage]
        protected virtual void AddDatabaseServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlServer();

            // Create an EntityFramework DbContext from the configured connection string
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("PasriDbContext");
            services.AddDbContext<MasterDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    x => x.MigrationsAssembly("HRC.DB.Master"));
            });

            // Inject the dependency of the IUnitOfWork to UnitOfWork to the scoped service lifetime (once per request)
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        [ExcludeFromCodeCoverage]
        protected virtual void AddApiDocumentationServices(IServiceCollection services)
        {
            // Add the Open API (Swagger) documentation service
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "PASR-Identity API",
                    Version = "v1",
                    Description = "API for the Personnel Accountability and Strength Reporting - Identity (PASR-I)",
                    Contact = new Contact
                    {
                        Name = "Darryl Schott",
                        Email = "darryl.t.schott.ctr@mail.mil"
                    }
                });

                options.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));

                options.TagActionsBy(api => api.GroupName);

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
