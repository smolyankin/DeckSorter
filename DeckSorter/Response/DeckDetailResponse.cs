using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeckSorter.Response
{
    /// <summary>
    /// колода детально
    /// </summary>
    [NotMapped]
    public class DeckDetailResponse : DeckResponse
    {
        /// <summary>
        /// список карт
        /// </summary>
        [Display(Name = "Карты")]
        public List<CardResponse> Cards { get; set; } = new List<CardResponse>();

        /// <summary>
        /// флаг типа перемешивания
        /// </summary>
        [Display(Name = "Имитация ручного перемешивания")]
        public bool Manual { get; set; } = false;

        /// <summary>
        /// сколько раз перемешивать
        /// </summary>
        [Display(Name = "Количество ручного перемешивания")]
        public long ManualCount { get; set; } = 1;
    }
}