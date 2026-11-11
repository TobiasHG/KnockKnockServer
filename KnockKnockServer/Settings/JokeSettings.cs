using KnockKnockServer.Models;
using System.ComponentModel.DataAnnotations;

namespace KnockKnockServer.Settings
{
    public class JokeSettings
    {
        public const string SectionName = "Jokes";

        [Required]
        [MinLength(1, ErrorMessage = "Der skal være mindst én joke.")]
        public List<Joke> Jokes { get; set; }
    }
}