using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Linq;
using System.Linq;
using System.Web;
using DeckSorter.Models;

namespace DeckSorter.Context
{
    public class DeckContext : DataContext
    {
        public DeckContext() : base("DeckConnection")
        {

        }

        public DbSet<Deck> Decks;
        public DbSet<Card> Cards;
        public DbSet<Value> Values;
        public DbSet<Suit> Suits;
    }
}