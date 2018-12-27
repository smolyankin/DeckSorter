using System.ComponentModel.DataAnnotations;

namespace DeckSorter.Request
{
    /// <summary>
    /// запрос создания значения
    /// </summary>
    public class CreateValueRequest
    {
        /// <summary>
        /// название
        /// </summary>
        [Display(Name = "Название")]
        public string Title { get; set; }
    }
}