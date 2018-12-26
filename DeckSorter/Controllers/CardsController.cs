using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using DeckSorter.Request;
using DeckSorter.Services;

namespace DeckSorter.Controllers
{
    /// <summary>
    /// контроллер карт
    /// </summary>
    public class CardsController : Controller
    {
        private CardService service = new CardService();

        /// <summary>
        /// список карт
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAllCards());
        }

        /// <summary>
        /// подробно о карте
        /// </summary>
        /// <param name="id">ид карты</param>
        /// <returns></returns>
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var card = await service.GetCardById((long)id);
            if (card == null)
                return HttpNotFound();
            return View(card);
        }

        /// <summary>
        /// создание карты
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create()
        {
            var model = await service.CreateCardModel();
            return View(model);
        }

        /// <summary>
        /// создать карту
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCardRequest request)
        {
            if (ModelState.IsValid)
            {
                await service.CreateCard(request);
                
                return RedirectToAction("Index");
            }

            return View(request);
        }
        /*
        // GET: Cards/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var card = await service.GetCardById((long)id);
            if (card == null)
                return HttpNotFound();
            return View(card);
        }

        // POST: Cards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CardResponse card)
        {
            await service.EditCard(card);

            return View(card);
        }*/

        /// <summary>
        /// удаление карты
        /// </summary>
        /// <param name="id">ид карты</param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var card = await service.GetCardById((long)id);
            if (card == null)
                return HttpNotFound();
            return View(card);
        }

        /// <summary>
        /// удалить карту
        /// </summary>
        /// <param name="id">ид карты</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await service.DeleteCard(id);

            return RedirectToAction("Index");
        }
    }
}
