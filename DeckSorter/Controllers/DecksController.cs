using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using DeckSorter.Request;
using DeckSorter.Response;
using DeckSorter.Services;

namespace DeckSorter.Controllers
{
    /// <summary>
    /// контроллер кород
    /// </summary>
    public class DecksController : Controller
    {
        private DeckService service = new DeckService();

        /// <summary>
        /// список колод
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAllDecks());
        }

        /// <summary>
        /// подробно о колоде
        /// </summary>
        /// <param name="id">ид колоды</param>
        /// <returns></returns>
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var deck = await service.GetDeckDetailById((long)id);
            if (deck == null)
                return HttpNotFound();
            return View(deck);
        }

        /// <summary>
        /// перетусовать карты в колоде
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details(DeckDetailResponse deck)
        {
            if (ModelState.IsValid)
                deck = await service.Mixing(deck);
                
            return View(deck);
        }

        /// <summary>
        /// карты в колоде по порядку
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Sort(DeckDetailResponse deck)
        {
            if (ModelState.IsValid)
                deck = await service.Sorting(deck);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// создание колоды
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            var model = await service.CreateDeckModel();
            return View(model);
        }

        /// <summary>
        /// создать колоду
        /// </summary>
        /// <param name="deck"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateDeckRequest deck)
        {
            if (ModelState.IsValid)
            {
                await service.CreateDeck(deck);

                return RedirectToAction("Index");
            }

            return View(deck);
        }

        /// <summary>
        /// удаление колоды
        /// </summary>
        /// <param name="id">ид колоды</param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var deck = await service.GetDeckDetailById((long)id);
            if (deck == null)
                return HttpNotFound();
            return View(deck);
        }

        /// <summary>
        /// удалить колоду
        /// </summary>
        /// <param name="id">ид колоды</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await service.DeleteDeck(id);

            return RedirectToAction("Index");
        }
    }
}
