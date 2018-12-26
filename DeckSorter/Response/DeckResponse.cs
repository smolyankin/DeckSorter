using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeckSorter.Models;

namespace DeckSorter.Response
{
    /// <summary>
    /// представление колоды
    /// </summary>
    [NotMapped]
    public class DeckResponse : Deck
    {
        /// <summary>
        /// количество карт в колоде
        /// </summary>
        [Display(Name = "Количество карт")]
        public long Count{ get; set; }
    }
}