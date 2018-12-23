using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;

namespace DeckSorter.Service
{
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
                db.SubmitChanges();
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
                db.SubmitChanges();
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
                db.SubmitChanges();
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
                db.SubmitChanges();
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
                db.SubmitChanges();
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
                db.SubmitChanges();
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

        public async Task<List<Card>> GetAllCards()
        {
            using (var db = new DeckContext())
            {
                return db.Cards.ToList();
            }
        }
    }
}