using Workbench.Components;

namespace Workbench
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            // Additional configurations for the Blazor circuit 
            // and hub.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents(config =>
                {
                    config.DetailedErrors = true;
                    config.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(15);
                })
                .AddHubOptions(config =>
                {
                    config.ClientTimeoutInterval = TimeSpan.FromSeconds(10);
                    config.KeepAliveInterval = TimeSpan.FromSeconds(4);
                });

            // Configuration for logging.
            // Add a logger to component by injecting the ILogger<?> interface
            // with the component class as strongly-typed argument.
            builder.Logging.AddConsole()
                .SetMinimumLevel(LogLevel.Trace);

            var app = builder.Build();

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
