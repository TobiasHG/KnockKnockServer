using KnockKnockServer.Interfaces;
using KnockKnockServer.JokeProviderStrategies;
using KnockKnockServer.Services;
using KnockKnockServer.Settings;
using KnockKnockServer.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KnockKnockServer
{
    enum ConversationState
    {
        WaitingForWhosThere,
        WaitingForWho,
        Finished
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {

            var builder = Host.CreateApplicationBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            builder.Services
                .AddOptions<ServerSettings>()
                .Bind(builder.Configuration.GetSection(ServerSettings.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services
                .AddOptions<JokeSettings>()
                .Configure(o => o.Jokes = builder.Configuration
                    .GetSection(JokeSettings.SectionName)
                    .Get<List<Joke>>() ?? new())
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services
                .AddOptions<JokeSelectionSettings>()
                .Bind(builder.Configuration.GetSection(JokeSelectionSettings.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddSingleton<IJokeProvider, RandomJokeProvider>();
            builder.Services.AddSingleton<IJokeProvider, SequentialJokeProvider>();
            builder.Services.AddSingleton<JokeProviderService>();

            builder.Services.AddHostedService<KnockKnockServer>();

            try
            {
                using var host = builder.Build();
                await host.RunAsync();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
                return;
            }
        }


    }
}
