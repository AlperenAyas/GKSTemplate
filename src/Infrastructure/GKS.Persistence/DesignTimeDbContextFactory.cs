using GKS.Persistence.Contexts;
using GKS.Persistence.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GKSContext>
    {
        public GKSContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<GKSContext> builder = new();

            builder.UseNpgsql(AppSettingsManager.GetConfigurationManager().GetConnectionString("Gks"));

            return new GKSContext(builder.Options);
        }
    }
}
