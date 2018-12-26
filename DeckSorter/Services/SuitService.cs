using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Response;
using DeckSorter.Extensions;

namespace DeckSorter.Services
{
    public class SuitService
    {
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

        public async Task<Suit> EditSuit(Suit suit)
        {
            using (var db = new DeckContext())
            {
                var result = new Suit();
                var exist = await GetSuitById(suit.Id);
                if (exist == null)
                    throw new Exception($"suit with title {suit.Title} not exist");
                if (exist.Title != suit.Title)
                {
                    exist.Title = suit.Title;
                    await db.SaveChangesAsync();
                }

                return result;
            }
        }

        public async Task DeleteSuit(long id)
        {
            using (var db = new DeckContext())
            {
                var exist = await GetSuitById(id);
                if (exist == null)
                    throw new Exception("suit not exist");
                db.Suits.Remove(exist);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Suit>> GetAllSuits()
        {
            using (var db = new DeckContext())
            {
                return db.Suits.OrderBy(x => x.Title).ToList();
            }
        }

        public async Task<Suit> GetSuitById(long id)
        {
            using (var db = new DeckContext())
            {
                return await db.Suits.FindAsync(id);
            }
        }
    }

    /// <summary>
    /// интерфейс колод
    /// </summary>
    public interface ISuitService
    {
        Task CreateSuit(CreateSuitRequest request);

        Task<Suit> EditSuit(Suit suit);

        Task DeleteSuit(Suit suit);

        Task<List<Suit>> GetAllSuits();

        Task<Suit> GetSuitById(long id);
    }
}