using GKS.Application.Abstrations.Services;
using GKS.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<ICacheService,RedisCacheService>();
            services.AddSingleton<IConnectionMultiplexer>(
                opt=>
                    ConnectionMultiplexer.Connect(configuration.GetSection("Redis")["Host"]+":"+configuration.GetSection("Redis")["Port"])
            );

            services.AddSingleton<IJwtService, JwtService>();
        }
    }
}
