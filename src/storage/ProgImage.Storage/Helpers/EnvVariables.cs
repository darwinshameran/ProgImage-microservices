using System;

namespace ProgImage.Storage.Helpers
{
    public static class EnvVariables
    {
        public static string DatabaseHostname { get; } = Environment.GetEnvironmentVariable("DATABASE_HOSTNAME");
        public static string DatabaseName { get; } = Environment.GetEnvironmentVariable("DATABASE_NAME");
        public static string DatabaseUsername { get; } = Environment.GetEnvironmentVariable("DATABASE_USERNAME");
        public static string DatabasePassword { get; } = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");
        public static string WebserverPort { get; } = Environment.GetEnvironmentVariable("WEBSERVER_PORT");
        
        
    }
}