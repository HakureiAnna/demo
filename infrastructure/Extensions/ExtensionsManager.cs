using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Extensions
{
    public static class ExtensionsManager
    {
        public static IServiceCollection AddSwaggerCustom(
            this IServiceCollection self,
            string title = "API",
            string version = "v1",
            string docName = "v1")
        {
            self.AddSwaggerGen(config =>
            {
                config.SwaggerDoc(docName, new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = title,
                    Version = version
                });
            });

            return self;
        }

        public static IServiceCollection AddCorsCustom(
            this IServiceCollection self,
            string pName = "Cors Policy")
        {
            self.AddCors(options =>
            {
                options.AddPolicy(pName, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return self;
        }

        public static IApplicationBuilder UseSwaggerCustom(
            this IApplicationBuilder self,
            string epName = "API V1",
            string epURL = "/swagger/v1/swagger.json")
        {
            self.UseSwagger();
            self.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint(epURL, epName);
            });

            return self;
        }

        public static IApplicationBuilder UseMassTransitCustom(
            this IApplicationBuilder self,
            IHostApplicationLifetime lifetime
            )
        {
            var bus = self.ApplicationServices.GetService<IBusControl>();
            var busHandle = TaskUtil.Await(() =>
            {
                return bus.StartAsync();
            });
            lifetime.ApplicationStopping.Register(() =>
            {
                busHandle.Stop();
            });

            return self;
        }
    }
}
