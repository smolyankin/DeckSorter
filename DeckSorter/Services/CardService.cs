using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Response;
using DeckSorter.Extensions;

namespace DeckSorter.Services
{
    public class CardService
    {
        ValueService _valueService = new ValueService();
        SuitService _suitService= new SuitService();

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

        public async Task CreateCard(CreateCardRequest request)
        {
            using (var db = new DeckContext())
            {
                /*var exist = db.Cards.FirstOrDefault(x => x.SuitId == request.SelectedSuitId && x.ValueId == request.SelectedValueId);
                if (exist != null)
                    request.Message = "card exist";*/
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

        /*public async Task<CardResponse> EditCard(CardResponse card)
        {
            using (var db = new DeckContext())
            {
                var exist = await GetCardById(card.Id);
                if (exist == null)
                    throw new Exception($"card not exist");
                var value = await db.Values.FindAsync(card.ValueId);
                var suit = await db.Suits.FindAsync(card.SuitId);
                exist.ValueId = card.ValueId;
                exist.SuitId = card.SuitId;
                await db.SaveChangesAsync();
                var result = card.Transform<CardResponse>();
                result.ValueTitle = value.Title;
                result.SuitTitle = suit.Title;

                return result;
            }
        }*/

        public async Task DeleteCard(long id)
        {
            using (var db = new DeckContext())
            {
                var exist = await db.Cards.FindAsync(id);
                if (exist == null)
                    throw new Exception($"card not exist");
                db.Cards.Remove(exist);
                await db.SaveChangesAsync();
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

    /// <summary>
    /// интерфейс колод
    /// </summary>
    public interface ICardService
    {
        Task CreateCard(CreateCardRequest request);

        Task<Card> EditCard(CardResponse card);

        Task DeleteCard(CardResponse card);

        Task<List<Card>> GetAllCards();

        Task<Card> GetCardById(long id);
    }
}