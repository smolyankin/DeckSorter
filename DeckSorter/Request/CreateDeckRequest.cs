using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DeckSorter.Response;

namespace DeckSorter.Request
{
    /// <summary>
    /// запрос создания колоды
    /// </summary>
    public class CreateDeckRequest
    {
        /// <summary>
        /// название колоды
        /// </summary>
        [Display(Name = "Название")]
        public string Title { get; set; }

        /// <summary>
        /// список карт
        /// </summary>
        public List<CardDeckResponse> Cards { get; set; } = new List<CardDeckResponse>();
    }
}