using System.Data.Entity;
using DeckSorter.Models;

namespace DeckSorter.Context
{
    /// <summary>
    /// подключение к базе данных
    /// </summary>
    public class DeckContext : DbContext
    {
        /// <summary>
        /// базовое подключение
        /// </summary>
        public DeckContext() : base("DeckConnection")
        {

        }

        /// <summary>
        /// колоды
        /// </summary>
        public DbSet<Deck> Decks { get; set; }

        /// <summary>
        /// карты
        /// </summary>
        public DbSet<Card> Cards { get; set; }

        /// <summary>
        /// значения
        /// </summary>
        public DbSet<Value> Values { get; set; }

        /// <summary>
        /// масти
        /// </summary>
        public DbSet<Suit> Suits { get; set; }
    }
}