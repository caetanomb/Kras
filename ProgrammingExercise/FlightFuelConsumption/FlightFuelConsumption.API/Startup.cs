using System;
using System.Collections.Generic;
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
            services.AddDbContext<AppDbContext>(opt => {
                    opt.UseSqlServer(Configuration.GetConnectionString("AppDbContext"));
                });

            //Configure MediatR
            services.AddMediatR(typeof(EnterFlightCommandHandler).Assembly);

            services.AddTransient<IFlightRepository, FlightRepository>();
            services.AddTransient<IAirportRepository, AirportRepository>();
            services.AddTransient<IFlightQueries, FlightQueries>();
            services.AddTransient<IAirportQueries, AirportQueries>();            

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
