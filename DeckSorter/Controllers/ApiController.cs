using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DeckSorter.Services;
using System.Threading.Tasks;
using DeckSorter.Models;
using DeckSorter.Request;
//using DeckSorter.Response;

namespace DeckSorter.Controllers
{
    /// <summary>
    /// контроллер колод
    /// </summary>
    public class ApiController : System.Web.Http.ApiController
    {
        /*private readonly IDeckService _deckService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deckService"></param>
        public ApiController(IDeckService deckService)
        {
            _deckService = deckService;
        }

        /// <summary>
        /// создать масть
        /// </summary>
        /// <param name="request">запрос создания масти</param>
        /// <returns></returns>
        [HttpPost, Route("deck/suit")]
        public async Task<Suit> CreateSuit(CreateSuitRequest request)
        {
            return await _deckService.CreateSuit(request);
        }

        /// <summary>
        /// создать значение
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Value> CreateValue(CreateValueRequest request)
        {
            return await _deckService.CreateValue(request);
        }

        /// <summary>
        /// создать карту
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Card> CreateCard(CreateCardRequest request)
        {
            return await _deckService.CreateCard(request);
        }

        /// <summary>
        /// создать колоду
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Deck> CreateDeck(CreateDeckRequest request)
        {
            return await _deckService.CreateDeck(request);
        }

        /// <summary>
        /// добавить карту в колоду
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Deck> AddCard(EditDeckRequest request)
        {
            return await _deckService.AddCard(request);
        }

        /// <summary>
        /// убрать карту из колоды
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Deck> RemoveCard(EditDeckRequest request)
        {
            return await _deckService.RemoveCard(request);
        }

        /// <summary>
        /// получить все масти
        /// </summary>
        /// <returns></returns>
        // GET api/deck/getallsuits
        public async Task<List<Suit>> GetAllSuits()
        {
            return await _deckService.GetAllSuits();
        }

        /// <summary>
        /// получить все значения
        /// </summary>
        /// <returns></returns>
        public async Task<List<Value>> GetAllValues()
        {
            return await _deckService.GetAllValues();
        }

        /// <summary>
        /// получить все карты
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("deck/getallcards")]
        public async Task<List<Card>> GetAllCards()
        {
            return await _deckService.GetAllCards();
        }*/
    }
}
