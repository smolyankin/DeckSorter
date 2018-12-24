using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Response;
using DeckSorter.Extensions;

namespace DeckSorter.Service
{
    /// <summary>
    /// сервис колод
    /// </summary>
    public class DeckService
    {
        public async Task<Deck> CreateDeck(CreateDeckRequest request)
        {
            using (var db = new DeckContext())
            {
                var result = new Deck();
                var exist = db.Decks.FirstOrDefault(x => x.Title.ToLower() == request.Title.ToLower());
                if (exist != null)
                    throw new Exception($"deck with title {request.Title} exist");
                db.Decks.Add(new Deck() { Title = request.Title, DateModify = DateTime.UtcNow });
                await db.SaveChangesAsync();
                return result;
            }
        }

        public async Task<Deck> AddCard(EditDeckRequest request)
        {
            using (var db = new DeckContext())
            {
                var deck = db.Decks.FirstOrDefault(x => x.Id == request.Id);
                if (deck == null)
                    throw new Exception("deck not exist");
                var card = db.Cards.FirstOrDefault(x => x.Id == request.CardId);
                if (card == null)
                    throw new Exception("card not exist");
                deck.Cards.Add(card.Id);
                await db.SaveChangesAsync();
                return deck;
            }
        }

        public async Task<Deck> RemoveCard(EditDeckRequest request)
        {
            using (var db = new DeckContext())
            {
                var deck = db.Decks.FirstOrDefault(x => x.Id == request.Id);
                if (deck == null)
                    throw new Exception("deck not exist");
                var card = db.Cards.FirstOrDefault(x => x.Id == request.CardId);
                if (card == null)
                    throw new Exception("card not exist");
                deck.Cards.Remove(card.Id);
                await db.SaveChangesAsync();
                return deck;
            }
        }

        public async Task<List<Deck>> GetAllDecks()
        {
            using (var db = new DeckContext())
            {
                return db.Decks.ToList();
            }
        }

        
    }

    /// <summary>
    /// интерфейс колод
    /// </summary>
    public interface IDeckService
    {
        Task<Suit> CreateSuit(CreateSuitRequest request);

        Task<Value> CreateValue(CreateValueRequest request);

        Task<Card> CreateCard(CreateCardRequest request);

        Task<Deck> CreateDeck(CreateDeckRequest request);

        Task<Deck> AddCard(EditDeckRequest request);

        Task<Deck> RemoveCard(EditDeckRequest request);

        Task<List<Suit>> GetAllSuits();

        Task<List<Value>> GetAllValues();

        Task<List<Card>> GetAllCards();

        Task<List<Card>> GetAllDecks();
    }
}