using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckSorter.Request
{
    /// <summary>
    /// создать масть
    /// </summary>
    public class CreateSuitRequest
    {
        /// <summary>
        /// название
        /// </summary>
        public string Title { get; set; }
    }
}