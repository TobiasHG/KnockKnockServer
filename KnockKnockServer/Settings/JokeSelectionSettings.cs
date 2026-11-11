using KnockKnockServer.Models;
using System.ComponentModel.DataAnnotations;

namespace KnockKnockServer.Settings
{
    public class JokeSelectionSettings
    {
        public const string SectionName = "JokeSelection";

        [Required(ErrorMessage = "Strategy is required.")]
        public string Strategy { get; set; }
    }
}