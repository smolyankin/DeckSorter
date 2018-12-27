using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;

namespace DeckSorter.Services
{
    /// <summary>
    /// сервис мастей
    /// </summary>
    public class SuitService
    {
        /// <summary>
        /// создать масть
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task CreateSuit(CreateSuitRequest request)
        {
            using (var db = new DeckContext())
            {
                var exist = db.Suits.FirstOrDefault(x => x.Title.ToLower() == request.Title.ToLower());
                if (exist != null)
                    throw new Exception($"suit with title {request.Title} exist");
                db.Suits.Add(new Suit { Title = request.Title });
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// изменить масть
        /// </summary>
        /// <param name="suit"></param>
        /// <returns></returns>
        public async Task<Suit> EditSuit(Suit suit)
        {
            using (var db = new DeckContext())
            {
                var exist = await db.Suits.FindAsync(suit.Id);
                if (exist != null && exist.Title != suit.Title)
                {
                    exist.Title = suit.Title;
                    await db.SaveChangesAsync();
                }

                return exist;
            }
        }

        /// <summary>
        /// удалить масть
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteSuit(long id)
        {
            using (var db = new DeckContext())
            {
                var exist = await db.Suits.FindAsync(id);
                if (exist != null)
                {
                    var cards = db.Cards.ToList();
                    foreach (var card in cards)
                        if (card.SuitId == id)
                        {
                            var cardService = new CardService();
                            await cardService.DeleteCard(card.Id);
                        }
                            
                    db.Suits.Remove(exist);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// получить все масти
        /// </summary>
        /// <returns></returns>
        public async Task<List<Suit>> GetAllSuits()
        {
            using (var db = new DeckContext())
            {
                return db.Suits.OrderBy(x => x.Title).ToList();
            }
        }

        /// <summary>
        /// получить масть по ид
        /// </summary>
        /// <param name="id">ид масти</param>
        /// <returns></returns>
        public async Task<Suit> GetSuitById(long id)
        {
            using (var db = new DeckContext())
            {
                return await db.Suits.FindAsync(id);
            }
        }
    }
}