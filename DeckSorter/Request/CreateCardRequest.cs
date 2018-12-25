using System.Collections.Generic;
using System.Web.Mvc;
using DeckSorter.Models;

namespace DeckSorter.Request
{
    /// <summary>
    /// запрос создания карты
    /// </summary>
    public class CreateCardRequest
    {
        /// <summary>
        /// ид значения
        /// </summary>
        public long? ValueId { get; set; }

        /// <summary>
        /// ид масти
        /// </summary>
        public long? SuitId { get; set; }
        
        /// <summary>
        /// список значений
        /// </summary>
        public List<Value> Values { get; set; } = new List<Value>();

        /// <summary>
        /// список мастей
        /// </summary>
        public List<Suit> Suits{ get; set; } = new List<Suit>();

        /// <summary>
        /// список значений для селекта
        /// </summary>
        public List<SelectListItem> ValuesItems { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// список мастей для селекта
        /// </summary>
        public List<SelectListItem> SuitsItems { get; set; } = new List<SelectListItem>();
    }
}