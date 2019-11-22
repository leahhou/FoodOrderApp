using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace FoodOrderApi
{
    public static class Configuration
    {
        public static IConfigurationBuilder Config()
        {
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "local";

            Console.WriteLine($"GetCommandLineArgs: {environmentName}");
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
//                .AddJsonFile($"appsettings.{environmentName}.json")
                .AddJsonFile($"appsettings.{environmentName}.json", false, true)
                .AddEnvironmentVariables();
            return builder;
        }
    }
}