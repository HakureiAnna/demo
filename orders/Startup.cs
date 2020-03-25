using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using infrastructure;
using infrastructure.Extensions;
using infrastructure.Messages;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using orders.Consumers;
using orders.Models;
using orders.Models.Repositories;

namespace orders
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
            services.AddCorsCustom();

            services.AddDbContext<OrdersDbContext>(options =>
            {
                options.UseSqlServer(Constants.DB_CONN_STR);
            });

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerCustom("Orders API");

            services.AddMassTransit(config =>
            {
                config.AddConsumer<InventoryCheckResponseConsumer>();

                config.AddBus(provider =>
                {
                    var azureServiceBus = Bus.Factory.CreateUsingAzureServiceBus(busFactoryConfig =>
                    {
                        //busFactoryConfig.Message<FlightOrder>(configTopology =>
                        //{
                        //    configTopology.SetEntityName(Constants.SB_TOPIC_FO);
                        //});

                        var host = busFactoryConfig.Host(Constants.SB_CONN_STR, hostConfig =>
                        {
                            hostConfig.TransportType = TransportType.AmqpWebSockets;
                        });

                        busFactoryConfig.ReceiveEndpoint(Constants.SB_QUEUE_IC_RES, configurator =>
                        {
                            configurator.Consumer<InventoryCheckResponseConsumer>(provider);
                        });
                    });

                    //services.AddSingleton<IPublishEndpoint>(azureServiceBus);
                    services.AddSingleton<ISendEndpointProvider>(azureServiceBus);
                    services.AddSingleton<IBus>(azureServiceBus);

                    return azureServiceBus;
                });

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            IHostApplicationLifetime lifetime,
            OrdersDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("Cors Policy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerCustom("Orders API V1");

            app.UseMassTransitCustom(lifetime);
        }
    }
}
