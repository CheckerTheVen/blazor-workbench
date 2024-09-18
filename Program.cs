using Workbench.Components;

namespace Workbench
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

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
