using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckSorter.Request
{
    public class EditDeckRequest
    {
        public long Id { get; set; }
        public long CardId { get; set; }
        public List<long> Cards { get; set; } = new List<long>();
    }
}