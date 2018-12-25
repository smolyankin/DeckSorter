using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeckSorter.Models;

namespace DeckSorter.Response
{
    [NotMapped]
    public class CardResponse : Card
    {
        [Display(Name = "Значение")]
        public string ValueTitle { get; set; }

        [Display(Name = "Масть")]
        public string SuitTitle { get; set; }
    }
}