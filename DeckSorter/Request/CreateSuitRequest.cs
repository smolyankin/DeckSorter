using System.ComponentModel.DataAnnotations;

namespace DeckSorter.Request
{
    /// <summary>
    /// запрос создание масти
    /// </summary>
    public class CreateSuitRequest
    {
        /// <summary>
        /// название
        /// </summary>
        [Display(Name = "Название")]
        public string Title { get; set; }
    }
}