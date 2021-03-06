﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CreditCards.Infrastructure;
using Microsoft.EntityFrameworkCore;
using CreditCards.Core.Interfaces;

namespace CreditCards
{
    public class Startup
    {
        private IConfiguration _configuration;
        private IHostingEnvironment _environnment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environnment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            if (_environnment.IsDevelopment())
            {
                services.AddDbContext<AppDbContext>(
                    options => options.UseInMemoryDatabase("TestingDatabase"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(
                    options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddScoped<ICreditCardApplicationRepository, EntityFrameworkCreditCardApplicationRepository>();

            services.AddMvc();

            // Build the intermediate service provider then return it
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            if (_environnment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();

                var dbContext = serviceProvider.GetService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
