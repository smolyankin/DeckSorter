using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Response;
using DeckSorter.Extensions;
using DeckSorter.Services;
using DeckSorter.Utils;
using Newtonsoft.Json;

namespace DeckSorter.Services
{
    /// <summary>
    /// сервис колод
    /// </summary>
    public class DeckService
    {
        CardService _cardService = new CardService();

        public async Task<Deck> CreateDeck(CreateDeckRequest request)
        {
            using (var db = new DeckContext())
            {
                var result = new Deck();
                var exist = db.Decks.FirstOrDefault(x => x.Title.ToLower() == request.Title.ToLower());
                if (exist != null)
                    throw new Exception($"deck with title {request.Title} exist");
                var cardsToDeck = string.Empty;
                if (request.Cards.Any())
                {
                    var selectedCards = request.Cards.Where(x => x.IsEnabled).ToList();
                    var cardsIds = selectedCards.Select(x => x.Id).ToList();
                    cardsToDeck = cardsIds.SerializeToJson();
                }
                else
                    cardsToDeck = new List<long>().SerializeToJson();
                /*var cardsToDeck = request.Cards.Any(x => x.IsEnabled) ?
                    request.Cards.Where(w => w.IsEnabled).Select(x => x.Id).ToList().SerializeToJson() :
                    new List<long>().SerializeToJson();*/
                db.Decks.Add(new Deck()
                {
                    Title = request.Title,
                    CardsIds = cardsToDeck,
                    DateModify = DateTime.UtcNow
                });
                await db.SaveChangesAsync();

                return result;
            }
        }

        public async Task<CreateDeckRequest> CreateDeckModel()
        {
            var result = new CreateDeckRequest();
            var cards = await _cardService.GetAllCards();
            result.Cards = cards.Select(x => x.Transform<CardDeckResponse>()).ToList();

            return result;
        }

        public async Task<List<DeckResponse>> GetAllDecks()
        {
            using (var db = new DeckContext())
            {
                var decks = db.Decks.ToList();
                var resp = decks.Select(x => x.Transform<DeckResponse>()).ToList();
                foreach (var deck in resp)
                {
                    var cardsIds = new JsonSerializer().Deserialize<List<long>>
                    (new JsonTextReader
                        (new StringReader(deck.CardsIds)));
                    deck.Count = cardsIds.Count;
                }

                return resp;
            }
        }

        public async Task<DeckDetailResponse> GetDeckDetailById(long id)
        {
            using (var db = new DeckContext())
            {
                var deck = await db.Decks.FindAsync(id);
                var response = deck.Transform<DeckDetailResponse>();
                var cardsIds = new JsonSerializer().Deserialize<List<long>>
                    (new JsonTextReader
                        (new StringReader(deck.CardsIds)));
                response.Count = cardsIds.Count;
                foreach (var cardId in cardsIds)
                {
                    var cardResp = await _cardService.GetCardById(cardId);
                    response.Cards.Add(cardResp);
                }

                return response;
            }
        }

        public async Task<DeckDetailResponse> Mixing(DeckDetailResponse deck)
        {
            //var tempManual = deck.Manual;
            deck = await GetDeckDetailById(deck.Id);
            if (deck.Manual)
                await MixingManual(deck);
            else
                await MixingAuto(deck);
            await SaveMixingDeck(deck);

            return deck;
        }

        private async Task MixingAuto(DeckDetailResponse deck)
        {
            deck.Cards = deck.Cards.OrderBy(a => Guid.NewGuid()).ToList();
            deck.CardsIds = deck.Cards.Select(x => x.Id).ToList().SerializeToJson();
        }

        private async Task MixingManual(DeckDetailResponse deck)
        {
            var tempCards = new List<CardResponse>();
            var cardsIds = new JsonSerializer().Deserialize<List<long>>
            (new JsonTextReader
                (new StringReader(deck.CardsIds)));
            var count = cardsIds.Count;
            for (int i = 0; i < 10; i++)
            {
                var random = new Random();
                var minIndex = count > 10 ? - Convert.ToInt32(count * 0.4) : 0;
                var maxCount = count > 10 ? Convert.ToInt32(count * 0.4) : count - 1;
                var index = random.Next(minIndex, maxCount);
                tempCards = deck.Cards.GetRange(0, index + 1);
                deck.Cards.RemoveRange(0, index + 1);
                deck.Cards.AddRange(tempCards);
            }

            deck.Cards = tempCards;
            deck.CardsIds = tempCards.Select(x => x.Id).ToList().SerializeToJson();
        }

        public async Task SaveMixingDeck(DeckDetailResponse deck)
        {
            using (var db = new DeckContext())
            {
                var exist = await db.Decks.FindAsync(deck.Id);
                if (exist != null)
                {
                    exist.CardsIds = deck.CardsIds;
                    await db.SaveChangesAsync();
                }
            }
        }
    }

    /// <summary>
    /// интерфейс колод
    /// </summary>
    public interface IDeckService
    {
        Task<Deck> CreateDeck(CreateDeckRequest request);

        Task<Deck> AddCard(EditDeckRequest request);

        Task<Deck> RemoveCard(EditDeckRequest request);

        Task<List<Card>> GetAllDecks();
    }
}