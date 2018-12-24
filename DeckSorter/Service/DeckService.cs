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
        public async Task<Suit> CreateSuit(CreateSuitRequest request)
        {
            using (var db = new DeckContext())
            {
                var result = new Suit();
                var exist = db.Suits.FirstOrDefault(x => x.Title.ToLower() == request.Title.ToLower());
                if (exist != null)
                    throw new Exception($"suit with title {request.Title} exist");
                db.Suits.Add(new Suit { Title = request.Title });
                await db.SaveChangesAsync();
                return result;
            }
        }

        public async Task<Value> CreateValue(CreateValueRequest request)
        {
            using (var db = new DeckContext())
            {
                var result = new Value();
                var exist = db.Values.FirstOrDefault(x => x.Title.ToLower() == request.Title.ToLower());
                if (exist != null)
                    throw new Exception($"value with title {request.Title} exist");
                db.Values.Add(new Value { Title = request.Title });
                await db.SaveChangesAsync();
                return result;
            }
        }

        public async Task<Card> CreateCard(CreateCardRequest request)
        {
            using (var db = new DeckContext())
            {
                var result = new Card();
                var exist = db.Cards.FirstOrDefault(x => x.SuitId == request.SuitId && x.ValueId == request.ValueId);
                if (exist != null)
                    throw new Exception($"card exist");
                db.Cards.Add(new Card() { SuitId = request.SuitId, ValueId = request.ValueId });
                await db.SaveChangesAsync();
                return result;
            }
        }

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

        public async Task<List<Suit>> GetAllSuits()
        {
            using (var db = new DeckContext())
            {
                return db.Suits.ToList();
            }
        }

        public async Task<List<Value>> GetAllValues()
        {
            using (var db = new DeckContext())
            {
                return db.Values.ToList();
            }
        }

        public async Task<List<CardResponse>> GetAllCards()
        {
            using (var db = new DeckContext())
            {
                var cards = db.Cards.ToList();
                var valuesIds = cards.Select(c => c.ValueId).Distinct().ToList();
                var values = db.Values.Where(x => valuesIds.Contains(x.Id)).ToList();
                var suitsIds = cards.Select(c => c.SuitId).Distinct().ToList();
                var suits = db.Suits.Where(x => suitsIds.Contains(x.Id)).ToList();
                var response = new List<CardResponse>();
                foreach (var card in cards)
                {
                    var cardResponse = card.Transform<CardResponse>();
                    cardResponse.ValueTitle = values.FirstOrDefault(x => x.Id == card.ValueId)?.Title;
                    cardResponse.SuitTitle = suits.FirstOrDefault(x => x.Id == card.SuitId)?.Title;
                    response.Add(cardResponse);
                }
                return response;
            }
        }

        public async Task<List<Deck>> GetAllDecks()
        {
            using (var db = new DeckContext())
            {
                return db.Decks.ToList();
            }
        }

        public async Task<Value> GetValueById(long id)
        {
            using (var db = new DeckContext())
            {
                return await db.Values.FindAsync(id);
            }
        }

        public async Task<Suit> GetSuitById(long id)
        {
            using (var db = new DeckContext())
            {
                return await db.Suits.FindAsync(id);
            }
        }

        public async Task<CardResponse> GetCardById(long id)
        {
            using (var db = new DeckContext())
            {
                var card = await db.Cards.FindAsync(id);
                var value = await GetValueById(card.ValueId);
                var suit = await GetSuitById(card.SuitId);
                var response = card.Transform<CardResponse>();
                response.ValueTitle = value.Title;
                response.SuitTitle = suit.Title;

                return response;
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