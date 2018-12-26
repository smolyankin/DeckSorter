using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeckSorter.Models;

namespace DeckSorter.Response
{
    /// <summary>
    /// представление карты
    /// </summary>
    [NotMapped]
    public class CardResponse : Card
    {
        /// <summary>
        /// значение карты
        /// </summary>
        [Display(Name = "Значение")]
        public string ValueTitle { get; set; }

        /// <summary>
        /// масть карты
        /// </summary>
        [Display(Name = "Масть")]
        public string SuitTitle { get; set; }
    }
}