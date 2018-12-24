using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Services;

namespace DeckSorter.Controllers
{
    public class SuitsController : Controller
    {
        private SuitService service = new SuitService();

        // GET: Suits
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAllSuits());
        }

        // GET: Suits/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var suit = await service.GetSuitById((long)id);
            if (suit == null)
                return HttpNotFound();
            return View(suit);
        }

        // GET: Suits/Create
        public ActionResult Create()
        {
            return View(new CreateSuitRequest());
        }

        // POST: Suits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSuitRequest suit)
        {
            if (ModelState.IsValid)
            {
                await service.CreateSuit(suit);

                return RedirectToAction("Index");
            }

            return View(suit);
        }

        // GET: Suits/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var suit = await service.GetSuitById((long)id);
            if (suit == null)
                return HttpNotFound();
            return View(suit);
        }

        // POST: Suits/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Suit suit)
        {
            if (ModelState.IsValid)
                return View(await service.EditSuit(suit));
            return View(suit);
        }

        // GET: Suits/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var suit = await service.GetSuitById((long)id);
            if (suit == null)
                return HttpNotFound();
            return View(suit);
        }

        // POST: Suits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await service.DeleteSuit(id);

            return RedirectToAction("Index");
        }
    }
}
