using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreLoggerDemo
{
    public class Program
    {
        // Reference: http://jakeydocs.readthedocs.io/en/latest/fundamentals/logging.html
        // Reference: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging?tabs=aspnetcore2x#built-in-logging-providers
        // Reference: https://stackify.com/net-core-loggerfactory-use-correctly/
        // Reference: https://stackify.com/asp-net-core-logging-what-changed/
        // Reference: https://joonasw.net/view/aspnet-core-2-configuration-changes
        // Reference: https://github.com/aspnet/MetaPackages/blob/dev/src/Microsoft.AspNetCore/WebHost.cs
        // Reference: https://github.com/aspnet/Announcements/issues/238
        // Reference: https://github.com/aspnet/Logging/issues/615
        // Reference: https://github.com/aspnet/Announcements/issues/255

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();
    }
}
