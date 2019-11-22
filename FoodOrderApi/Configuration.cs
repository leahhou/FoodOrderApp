using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace FoodOrderApi
{
    public static class Configuration
    {
        public static IConfigurationBuilder Config()
        {
            var appLocation = Environment.GetEnvironmentVariable("APPLOCATION") ?? "local";

            Console.WriteLine($"app location: {appLocation}");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{appLocation}.json", false, true)
                .AddEnvironmentVariables();
                //Environment variable is used to get the desired appsettings.{appLocation.json file;
                return builder;
        }
    }
}