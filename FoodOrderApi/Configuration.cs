using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace FoodOrderApi
{
    public static class Configuration
    {
        public static IConfigurationBuilder Config()
        {
            // Environment.SetEnvironmentVariable("ENVIRONMENT","local");
            var environmentName = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "local";

//            if (string.IsNullOrEmpty(environmentName)) environmentName = "local";
            
            Console.WriteLine("GetCommandLineArgs: {0}", String.Join("--", environmentName));
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true,true)
//                .AddJsonFile($"appsettings.{environmentName}.json")
                .AddJsonFile($"appsettings.{environmentName}.json", false, true)
                .AddEnvironmentVariables();
            return builder;
        }
    }
}