using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DeckSorter.Models;

namespace DeckSorter.Response
{
    [NotMapped]
    public class DeckDetailResponse : DeckResponse
    {
        [Display(Name = "Карты")]
        public List<CardResponse> Cards { get; set; } = new List<CardResponse>();

        [Display(Name = "Имитация ручного перемешивания")]
        public bool Manual { get; set; } = false;

        [Display(Name = "Количество ручного перемешивания")]
        public long ManualCount { get; set; } = 10;
    }
}