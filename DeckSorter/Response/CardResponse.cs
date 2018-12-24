using System.ComponentModel.DataAnnotations.Schema;
using DeckSorter.Models;

namespace DeckSorter.Response
{
    [NotMapped]
    public class CardResponse : Card
    {
        public string ValueTitle { get; set; }
        public string SuitTitle { get; set; }
    }
}