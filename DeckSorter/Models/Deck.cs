using System;
using System.Collections.Generic;

namespace DeckSorter.Models
{
    /// <summary>
    /// сущность колоды
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// ид
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// список ид карт
        /// </summary>
        public List<long> Cards { get; set; } = new List<long>();

        /// <summary>
        /// дата изменения
        /// </summary>
        public DateTime DateModify { get; set; }
    }
}