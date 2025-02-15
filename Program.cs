using Microsoft.AspNetCore;
using Serilog;

namespace SportsStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            Log.Logger = new LoggerConfiguration()
    .WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

            Log.Information("Hello, {Name}!", Environment.UserName);

            Console.ReadKey(true);
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseDefaultServiceProvider(options =>
            options.ValidateScopes = false)
            .Build();
        
      }
}
