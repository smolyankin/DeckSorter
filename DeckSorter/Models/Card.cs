namespace DeckSorter.Models
{
    /// <summary>
    /// сущность карты
    /// </summary>
    public class Card
    {
        /// <summary>
        /// ид
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// ид масти
        /// </summary>
        public long SuitId { get; set; }

        /// <summary>
        /// ид значения
        /// </summary>
        public long ValueId { get; set; }
    }
}