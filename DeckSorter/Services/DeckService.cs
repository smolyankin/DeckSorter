using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Response;
using DeckSorter.Extensions;
using DeckSorter.Utils;
using Newtonsoft.Json;

namespace DeckSorter.Services
{
    /// <summary>
    /// сервис колод
    /// </summary>
    public class DeckService : IDeckService
    {
        CardService _cardService = new CardService();

        /// <summary>
        /// создание колоды
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
                    var selectedCards = request.Cards.Where(x => x.IsEnabled).OrderBy(x => x.SuitTitle).ThenBy(x => x.ValueTitle).ToList();
                    var cardsIds = selectedCards.Select(x => x.Id).ToList();
                    cardsToDeck = cardsIds.SerializeToJson();
                }
                else
                    cardsToDeck = new List<long>().SerializeToJson();
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

        /// <summary>
        /// создание модели создания колоды
        /// </summary>
        /// <returns></returns>
        public async Task<CreateDeckRequest> CreateDeckModel()
        {
            var result = new CreateDeckRequest();
            var cards = await _cardService.GetAllCards();
            result.Cards = cards.Select(x => x.Transform<CardDeckResponse>()).OrderBy(x => x.SuitTitle).ThenBy(x => x.ValueTitle).ToList();

            return result;
        }

        /// <summary>
        /// получить все доски
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeckResponse>> GetAllDecks()
        {
            using (var db = new DeckContext())
            {
                var decks = db.Decks.ToList();
                var resp = decks.Select(x => x.Transform<DeckResponse>()).OrderByDescending(x => x.DateModify).ToList();
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

        /// <summary>
        /// получить детально колоду по ид
        /// </summary>
        /// <param name="id">ид колоды</param>
        /// <returns></returns>
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

        /// <summary>
        /// перемешать карты
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        public async Task<DeckDetailResponse> Mixing(DeckDetailResponse deck)
        {
            var tempManual = deck.Manual;
            var tempCount = deck.ManualCount;
            deck = await GetDeckDetailById(deck.Id);
            if (tempManual)
                await MixingManual(deck);
            else
                await MixingAuto(deck);
            await SaveMixingDeck(deck);
            deck.Manual = tempManual;
            deck.ManualCount = tempCount;

            return deck;
        }

        /// <summary>
        /// случайное перемешивание
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        private async Task MixingAuto(DeckDetailResponse deck)
        {
            deck.Cards = deck.Cards.OrderBy(a => Guid.NewGuid()).ToList();
            deck.CardsIds = deck.Cards.Select(x => x.Id).ToList().SerializeToJson();
        }

        /// <summary>
        /// имитация ручного перемешивания приблизительно по половине
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        private async Task MixingManual(DeckDetailResponse deck)
        {
            var tempCards = new List<CardResponse>();
            var cardsIds = new JsonSerializer().Deserialize<List<long>>
            (new JsonTextReader
                (new StringReader(deck.CardsIds)));
            var count = cardsIds.Count;
            for (int i = 0; i < deck.ManualCount; i++)
            {
                var random = new Random();
                var minCount = Convert.ToInt32(count * 0.4);
                var maxCount = Convert.ToInt32(count * 0.6);
                var cnt = random.Next(minCount, maxCount);
                tempCards = deck.Cards.GetRange(0, cnt);
                deck.Cards.RemoveRange(0, cnt);
                deck.Cards.AddRange(tempCards);
            }
            deck.CardsIds = deck.Cards.Select(x => x.Id).ToList().SerializeToJson();
        }

        /// <summary>
        /// сортировка карт по умолчанию
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        public async Task<DeckDetailResponse> Sorting(DeckDetailResponse deck)
        {
            var tempManual = deck.Manual;
            var tempCount = deck.ManualCount;
            deck = await GetDeckDetailById(deck.Id);
            deck.Cards = deck.Cards.OrderBy(x => x.SuitTitle).ThenBy(x => x.ValueTitle).ToList();
            deck.CardsIds = deck.Cards.Select(x => x.Id).ToList().SerializeToJson();
            await SaveMixingDeck(deck);
            deck.Manual = tempManual;
            deck.ManualCount = tempCount;

            return deck;
        }

        /// <summary>
        /// сохранить новый порядок карт
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        private async Task SaveMixingDeck(DeckDetailResponse deck)
        {
            using (var db = new DeckContext())
            {
                var exist = await db.Decks.FindAsync(deck.Id);
                if (exist != null)
                {
                    exist.CardsIds = deck.CardsIds;
                    exist.DateModify = DateTime.UtcNow;
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// удалить доску
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteDeck(long id)
        {
            using (var db = new DeckContext())
            {
                var exist = await db.Decks.FindAsync(id);
                if (exist != null)
                {
                    db.Decks.Remove(exist);
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
        /// <summary>
        /// создание колоды
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Deck> CreateDeck(CreateDeckRequest request);

        /// <summary>
        /// получить все доски
        /// </summary>
        /// <returns></returns>
        Task<List<DeckResponse>> GetAllDecks();

        /// <summary>
        /// получить детально колоду по ид
        /// </summary>
        /// <param name="id">ид колоды</param>
        /// <returns></returns>
        Task<DeckDetailResponse> GetDeckDetailById(long id);

        /// <summary>
        /// перемешать карты
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        Task<DeckDetailResponse> Mixing(DeckDetailResponse deck);

        /// <summary>
        /// сортировка карт по умолчанию
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        Task<DeckDetailResponse> Sorting(DeckDetailResponse deck);

        /// <summary>
        /// удалить доску
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteDeck(long id);
    }
}