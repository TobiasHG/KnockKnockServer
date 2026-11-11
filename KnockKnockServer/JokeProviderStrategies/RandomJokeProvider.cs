using KnockKnockServer.Interfaces;
using KnockKnockServer.Models;
using KnockKnockServer.Settings;
using Microsoft.Extensions.Options;

namespace KnockKnockServer.JokeProviderStrategies
{
    public class RandomJokeProvider : IJokeProvider
    {
        public string StrategyName { get; set; } = "Random";
        private readonly List<Joke> jokes;
        public RandomJokeProvider(IOptions<JokeSettings> jokeSettings)
        {
            jokes = jokeSettings.Value.Jokes;
        }
        public Joke GetJoke()
        {
            int index = Random.Shared.Next(jokes.Count);
            return jokes[index];
        }

    }
}
