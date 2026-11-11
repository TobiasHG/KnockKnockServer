using System;
using System.Collections.Generic;
using System.Text;
using KnockKnockServer.Models;

namespace KnockKnockServer.Interfaces
{
    public interface IJokeProvider
    {
        string StrategyName { get; set; }
        Joke GetJoke();
    }
}
