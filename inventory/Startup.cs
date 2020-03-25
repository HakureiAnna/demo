using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using infrastructure;
using MassTransit;
using MassTransit.Azure.ServiceBus.Core;
using infrastructure.Messages;
using Microsoft.Azure.ServiceBus;
using inventory.Consumers;
using MassTransit.Util;
using inventory.Models;
using Microsoft.EntityFrameworkCore;
using inventory.Models.Repositories;
using infrastructure.Extensions;

namespace inventory
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

            services.AddDbContext<InventoryDbContext>(options => {
                options.UseSqlServer(Constants.DB_CONN_STR);
            });
            
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddControllers();

            services.AddSwaggerCustom("Inventory API");

            services.AddMassTransit(config =>
            {
                //config.AddConsumer<FlightPurchasedConsumer>();
                //config.AddConsumer<FlightCancellationConsumer>();
                config.AddConsumer<InventoryCheckRequestConsumer>();

                config.AddBus(provider =>
                {
                    var azureServiceBus = Bus.Factory.CreateUsingAzureServiceBus(busFactoryConfig =>
                    {
                        //busFactoryConfig.Message<FlightOrder>(m =>
                        //{
                        //    m.SetEntityName(Constants.SB_TOPIC_FO);
                        //});

                        var host = busFactoryConfig.Host(Constants.SB_CONN_STR, hostConfig =>
                        {
                            hostConfig.TransportType = TransportType.AmqpWebSockets;
                        });

                        busFactoryConfig.ReceiveEndpoint(Constants.SB_QUEUE_IC_REQ, configurator =>
                        {
                            configurator.Consumer<InventoryCheckRequestConsumer>(provider);
                        });

                        //busFactoryConfig.SubscriptionEndpoint<FlightOrder>(Constants.SB_SUBSCRIPTION_FS, configurator =>
                        //{
                        //    configurator.Consumer<FlightPurchasedConsumer>(provider);
                        //});

                        //busFactoryConfig.ReceiveEndpoint(Constants.SB_QUEUE_FC, configurator =>
                        //{
                        //    configurator.Consumer<FlightCancellationConsumer>(provider);
                        //});
                    });
                    
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
            InventoryDbContext dbContext)
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

            app.UseSwaggerCustom("Inventory API V1");

            app.UseMassTransitCustom(lifetime);
        }
    }
}
