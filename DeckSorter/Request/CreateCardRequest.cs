using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DeckSorter.Request
{
    /// <summary>
    /// запрос создания карты
    /// </summary>
    public class CreateCardRequest
    {
        /// <summary>
        /// ид выбранного значения
        /// </summary>
        [Display(Name = "Значение")]
        public int SelectedValueId { get; set; }

        /// <summary>
        /// перечень значений
        /// </summary>
        public IEnumerable<SelectListItem> Values { get; set; }

        /// <summary>
        /// ид выбранной масти
        /// </summary>
        [Display(Name = "Масть")]
        public int SelectedSuitId { get; set; }

        /// <summary>
        /// перечень мастей
        /// </summary>
        public IEnumerable<SelectListItem> Suits { get; set; }
    }
}