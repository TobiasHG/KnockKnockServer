using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace KnockKnockServer.Settings
{
    public class ServerSettings
    {
        public const string SectionName = "Server";

        [Range(1, 65535, ErrorMessage = "Server:Port skal være mellem 1 og 65535.")]
        public int Port { get; set; }

        [Range(1, 10000, ErrorMessage = "Server:MaxConcurrentClients skal være mellem 1 og 10000.")]
        public int MaxConcurrentClients { get; set; }

    }
}