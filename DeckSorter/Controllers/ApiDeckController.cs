using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Response;
using DeckSorter.Services;


namespace DeckSorter.Controllers
{
    /// <summary>
    /// апи контроллер колод
    /// </summary>
    public class ApiDeckController : ApiController
    {
        private DeckService _deckService = new DeckService();
        /*private readonly IDeckService _deckService;

        /// <summary>
        /// const
        /// </summary>
        /// <param name="deckService"></param>
        public ApiDeckController(IDeckService deckService)
        {
            _deckService = deckService;
        }*/

        /// <summary>
        /// список колод
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("decks")]
        public async Task<List<DeckResponse>> Index()
        {
            return await _deckService.GetAllDecks();
        }

        /// <summary>
        /// подробно о колоде
        /// </summary>
        /// <param name="id">ид колоды</param>
        /// <returns></returns>
        [HttpGet, Route("deck/details")]
        public async Task<DeckDetailResponse> Details(long id)
        {
            return await _deckService.GetDeckDetailById(id);
        }

        /// <summary>
        /// перетусовать карты в колоде
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        [HttpPost, Route("deck/mix")]
        public async Task<DeckDetailResponse> Details(DeckDetailResponse deck)
        {
            return await _deckService.Mixing(deck);
        }

        /// <summary>
        /// карты в колоде по порядку
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        [HttpPost, Route("deck/sort")]
        public async Task<DeckDetailResponse> Sort(DeckDetailResponse deck)
        {
            return await _deckService.Sorting(deck);
        }

        /// <summary>
        /// создать колоду
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        [HttpPost, Route("deck/create")]
        public async Task<Deck> Create(CreateDeckRequest deck)
        {
            return await _deckService.CreateDeck(deck);
        }

        /// <summary>
        /// удалить колоду
        /// </summary>
        /// <param name="id">ид колоды</param>
        /// <returns></returns>
        [HttpDelete, Route("deck/delete")]
        public async Task Delete(long id)
        {
            await _deckService.DeleteDeck(id);
        }
    }
}
