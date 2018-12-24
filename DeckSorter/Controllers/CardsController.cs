using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeckSorter.Context;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Service;
using DeckSorter.Response;

namespace DeckSorter.Controllers
{
    public class CardsController : Controller
    {
        private DeckService service = new DeckService();

        // GET: Cards
        public async Task<ActionResult> Index()
        {
            var cards = await service.GetAllCards();

            return View(cards);
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
            var model = new CreateCardRequest();

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
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Cards.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }*/
            return View(new Card());
        }

        // POST: Cards/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SuitId,ValueId")] Card card)
        {
            /*if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }*/
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Cards.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }*/
            return View(new Card());
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            /*Card card = await db.Cards.FindAsync(id);
            db.Cards.Remove(card);
            await db.SaveChangesAsync();*/
            return RedirectToAction("Index");
        }
        /*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
