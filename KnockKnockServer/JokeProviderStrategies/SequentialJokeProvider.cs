using KnockKnockServer.Interfaces;
using KnockKnockServer.Models;
using KnockKnockServer.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnockKnockServer.JokeProviderStrategies
{
    public class SequentialJokeProvider : IJokeProvider
    {
        public string StrategyName { get; set; } = "Sequential";
        private readonly List<Joke> jokes;
        private int _next = -1;
        public SequentialJokeProvider(IOptions<JokeSettings> jokeSettings)
        {
            jokes = jokeSettings.Value.Jokes;
        }
        public Joke GetJoke()
        {

            // indfør egen Løsning
            int index = (int)((uint)Interlocked.Increment(ref _next) % (uint)jokes.Count);
            return jokes[index];
        }

    }
}
