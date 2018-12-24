using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Web;
using DeckSorter.Models;

namespace DeckSorter.Context
{
    public class DeckContext : DbContext
    {
        public DeckContext() : base("DeckConnection")
        {

        }

        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<Suit> Suits { get; set; }
    }
}