using System.Collections.Generic;
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
        public string Title { get; set; }

        /// <summary>
        /// список карт
        /// </summary>
        public List<CardDeckResponse> Cards { get; set; } = new List<CardDeckResponse>();
    }
}