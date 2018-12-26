using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeckSorter.Models;

namespace DeckSorter.Response
{
    [NotMapped]
    public class DeckResponse : Deck
    {
        [Display(Name = "Количество карт")]
        public long Count{ get; set; }
    }
}