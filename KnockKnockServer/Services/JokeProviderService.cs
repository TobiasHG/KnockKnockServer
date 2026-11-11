using KnockKnockServer.Interfaces;
using KnockKnockServer.Settings;
using KnockKnockServer.Models;
using Microsoft.Extensions.Options;

namespace KnockKnockServer.Services
{
    public class JokeProviderService
    {
        private readonly IJokeProvider _provider;

        public JokeProviderService(
            IOptions<JokeSelectionSettings> selection,
            IEnumerable<IJokeProvider> providers)
        {
            var strategy = selection.Value.Strategy;

            _provider = providers.FirstOrDefault(p =>
                    p.StrategyName.Equals(strategy, StringComparison.OrdinalIgnoreCase))
                ?? throw new InvalidOperationException($"Ukendt strategi: {strategy}");
        }

        public Joke GetJoke() => _provider.GetJoke();
    }
}
