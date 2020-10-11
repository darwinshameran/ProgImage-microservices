using System;

namespace ProgImage.Gateway.API
{
    public static class EnvVariables
    {
        public static string WebserverPort { get; } = Environment.GetEnvironmentVariable("WEBSERVER_PORT");
    }
}