using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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
    /// сервис карт
    /// </summary>
    public class CardService
    {
        ValueService _valueService = new ValueService();
        SuitService _suitService= new SuitService();

        /// <summary>
        /// создание модели создания карты
        /// </summary>
        /// <returns></returns>
        public async Task<CreateCardRequest> CreateCardModel()
        {
            var model = new CreateCardRequest();
            var values = await _valueService.GetAllValues();
            model.Values = values.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });
            var suits = await _suitService.GetAllSuits();
            model.Suits = suits.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });

            return model;
        }

        /// <summary>
        /// создание карты
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task CreateCard(CreateCardRequest request)
        {
            using (var db = new DeckContext())
            {
                if(long.TryParse(request.SelectedSuitId.ToString(), out long suitId) &&
                   suitId > 0 && 
                   long.TryParse(request.SelectedValueId.ToString(), out long valueId) &&
                   valueId > 0)
                {
                    db.Cards.Add(new Card {SuitId = suitId, ValueId = valueId});
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// удаление карты
        /// </summary>
        /// <param name="id">ид карты</param>
        /// <returns></returns>
        public async Task DeleteCard(long id)
        {
            using (var db = new DeckContext())
            {
                var exist = await db.Cards.FindAsync(id);
                if (exist != null)
                {
                    var decks = db.Decks.ToList();
                    foreach (var deck in decks)
                    {
                        var cardsIds = new JsonSerializer().Deserialize<List<long>>
                        (new JsonTextReader
                            (new StringReader(deck.CardsIds)));
                        if (cardsIds.Contains(exist.Id))
                        {
                            cardsIds.Remove(exist.Id);
                            deck.CardsIds = cardsIds.SerializeToJson();
                        }
                    }
                    db.Cards.Remove(exist);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// список карт
        /// </summary>
        /// <returns></returns>
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
                return response.OrderBy(x => x.SuitTitle).ThenBy(x => x.ValueTitle).ToList();
            }
        }

        /// <summary>
        /// получить карту по ид
        /// </summary>
        /// <param name="id">ид карты</param>
        /// <returns></returns>
        public async Task<CardResponse> GetCardById(long id)
        {
            using (var db = new DeckContext())
            {
                var card = await db.Cards.FindAsync(id);
                var value = await _valueService.GetValueById(card.ValueId);
                var suit = await _suitService.GetSuitById(card.SuitId);
                var response = card.Transform<CardResponse>();
                response.ValueTitle = value.Title;
                response.SuitTitle = suit.Title;

                return response;
            }
        }
    }
}