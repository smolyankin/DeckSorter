using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DeckSorter.Models;

namespace DeckSorter.Request
{
    /// <summary>
    /// запрос создания карты
    /// </summary>
    public class CreateCardRequest
    {
        [Display(Name = "Значение")]
        public int SelectedValueId { get; set; }

        public IEnumerable<SelectListItem> Values { get; set; }

        [Display(Name = "Масть")]
        public int SelectedSuitId { get; set; }

        public IEnumerable<SelectListItem> Suits { get; set; }

        public string Message { get; set; }
    }
}