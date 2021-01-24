using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tax.Services.Mapper;
using Tax.Services.Services;
using Tax.Services.Services.Interfaces;

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
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<TaxCalculatorToBeImplemented>();
            services.AddTransient<TaxCalculatorTaxJarApi>();

            services.AddTransient<Func<string, ITaxCalculator>>(serviceProvider => clientName =>
           {
               // check if services is hosted for multiple clients or self hosted for client
               bool isServiceMultipleClients = Configuration.GetValue<bool>("MultipleClients");

               // if hosted for multiple clients, gets the client tax calculator from appSettings.json else retuns null
               string taxCalculatorForClient = isServiceMultipleClients && !string.IsNullOrEmpty(clientName) ? Configuration.GetSection("ClientTaxCalculators").GetChildren().ToList().Select(c => new
               {
                   Name = c.GetValue<string>("Name"),
                   TaxCalculator = c.GetValue<string>("TaxCalculator")
               }).Where(c => c.Name == clientName)
               .FirstOrDefault()?.TaxCalculator : null;

               // gets the defaul tax calculator for client if self hosted
               var defaultTaxCalculator = Configuration.GetSection("Client").GetSection("TaxCalculator").Value;

               //gets the tax calculator depending on variable above to instantiate the service
               string taxCalculatorImplementation = isServiceMultipleClients && !string.IsNullOrEmpty(taxCalculatorForClient) ? taxCalculatorForClient : defaultTaxCalculator;

               // resolves the tax calculator, we can add as many as we want
               if (taxCalculatorImplementation == "TBI")
               {
                   // this service is to show, multiple tax calculator can be handle
                   return serviceProvider.GetService<TaxCalculatorToBeImplemented>();
               }
               else if (taxCalculatorImplementation == "TaxJarApi")
               {
                   // resolve using tax jar api tax calculator service
                   return serviceProvider.GetService<TaxCalculatorTaxJarApi>();
               }
               else
               {
                   // we could also resolve with a default service if we would like
                   throw new NotImplementedException();
               }
           });

            services.AddControllers();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
