using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Service;
using DeckSorter.Response;
using DeckSorter.Services;

namespace DeckSorter.Controllers
{
    public class CardsController : Controller
    {
        private CardService service = new CardService();

        // GET: Cards
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAllCards());
        }

        // GET: Cards/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var card = await service.GetCardById((long)id);
            if (card == null)
                return HttpNotFound();
            return View(card);
        }

        // GET: Cards/Create
        public ActionResult Create()
        {
            var model = service.CreateCardModel().GetAwaiter().GetResult();
            return View(model);
        }

        // POST: Cards/Create
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
        }

        // GET: Cards/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var card = await service.GetCardById((long)id);
            if (card == null)
                return HttpNotFound();
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await service.DeleteCard(id);

            return RedirectToAction("Index");
        }
    }
}
