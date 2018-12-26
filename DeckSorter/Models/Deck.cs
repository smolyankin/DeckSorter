using System;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Название колоды")]
        public string Title { get; set; }

        /// <summary>
        /// список ид карт
        /// </summary>
        public string CardsIds { get; set; }

        /// <summary>
        /// дата изменения
        /// </summary>
        [Display(Name = "Дата изменения")]
        public DateTime DateModify { get; set; }
    }
}