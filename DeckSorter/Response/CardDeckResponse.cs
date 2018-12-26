using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckSorter.Response
{
    /// <summary>
    /// модель карты в колоде
    /// </summary>
    [NotMapped]
    public class CardDeckResponse : CardResponse
    {
        /// <summary>
        /// флаг включения в колоду
        /// </summary>
        [Display(Name = "В колоде")]
        public bool IsEnabled { get; set; } = false;
    }
}