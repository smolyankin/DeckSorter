using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeckSorter.Models;

namespace DeckSorter.Response
{
    [NotMapped]
    public class CardDeckResponse : CardResponse
    {
        [Display(Name = "В колоде")]
        public bool IsEnabled { get; set; } = false;
    }
}