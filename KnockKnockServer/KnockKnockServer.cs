using KnockKnockServer.Services;
using KnockKnockServer.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace KnockKnockServer
{
    public class KnockKnockServer : BackgroundService
    {
        private readonly ServerSettings settings;
        private readonly JokeProviderService _jokeProviderService;

        public KnockKnockServer(IOptions<ServerSettings> serverSettings, JokeProviderService jokeProviderService)
        {
            settings = serverSettings.Value;
            _jokeProviderService = jokeProviderService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TcpListener server = new TcpListener(IPAddress.Any, settings.Port);
            server.Start();

            Console.WriteLine("Tobias's Knock Knock Server :D");
            Console.WriteLine($"Knock knock server startede.. lytter på port {settings.Port}");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {

                    TcpClient client = await server.AcceptTcpClientAsync();

                    // TODO: jokeproviderservice burde blive DI højere oppe i programmet?
                    ClientHandler handler = new ClientHandler(_jokeProviderService);

                    _ = Task.Run(() => handler.NewClient(client));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
