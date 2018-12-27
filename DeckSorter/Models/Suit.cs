using System.ComponentModel.DataAnnotations;

namespace DeckSorter.Models
{
    /// <summary>
    /// сущность масти
    /// </summary>
    public class Suit
    {
        /// <summary>
        /// ид
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// название
        /// </summary>
        [Display(Name = "Название")]
        public string Title{ get; set; }
    }
}