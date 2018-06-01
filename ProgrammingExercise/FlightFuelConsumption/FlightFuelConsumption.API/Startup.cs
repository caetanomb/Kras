using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlightFuelConsumption.API.Queries;
using FlightFuelConsumption.ApplicationServices.CommandHandlers;
using FlightFuelConsumption.Core.Interfaces;
using FlightFuelConsumption.Infrastructure.Data;
using FlightFuelConsumption.Infrastructure.Data.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace FlightFuelConsumption.API
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
            //Configure Entity Context
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("AppDbContext"));
            });

            //Configure MediatR
            services.AddMediatR(typeof(EnterFlightCommandHandler).Assembly);

            services.AddTransient<IFlightRepository, FlightRepository>();
            services.AddTransient<IAirportRepository, AirportRepository>();
            services.AddTransient<IFlightQueries, FlightQueries>();
            services.AddTransient<IAirportQueries, AirportQueries>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            services.AddMvc()
              .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
      //Redirect non api calls to angular app that will handle routing of the app.    
      app.Use(async (context, next) =>
      {
        await next();
        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });

      if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("MyPolicy");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
