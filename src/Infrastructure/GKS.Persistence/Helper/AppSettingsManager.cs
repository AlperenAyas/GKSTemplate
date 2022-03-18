
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GKS.Persistence.Helper
{
    internal static class AppSettingsManager
    {
        public static ConfigurationManager GetConfigurationManager()
        {
            ConfigurationManager configuration = new();

            configuration.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/GKS.WebApi"));

            configuration.AddJsonFile("appsettings.json");

            return configuration;
        }
    }
}
