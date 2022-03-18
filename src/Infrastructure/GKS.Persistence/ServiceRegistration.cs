using GKS.Application.Abstrations.Repositories;
using GKS.Application.Abstrations.UnitOfWork;
using GKS.Persistence.Contexts;
using GKS.Persistence.Repositories;
using GKS.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GKSContext>(
                opt =>
                {
                    opt.UseNpgsql(configuration.GetConnectionString("Gks"));
                }
                , ServiceLifetime.Scoped);

            services.AddScoped(typeof(ICommandRepository<>),typeof(CommandRepository<>));

            services.AddScoped(typeof(IQueryRepository<>),typeof(QueryRepository<>));

            services.AddScoped<IUow,Uow>();
        }
    }
}
