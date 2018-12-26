using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeckSorter.Response;

namespace DeckSorter.Request
{
    public class CreateDeckRequest
    {
        public string Title { get; set; }
        public List<CardDeckResponse> Cards { get; set; } = new List<CardDeckResponse>();
    }
}