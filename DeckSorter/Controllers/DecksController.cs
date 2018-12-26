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
using DeckSorter.Response;
using DeckSorter.Services;

namespace DeckSorter.Controllers
{
    public class DecksController : Controller
    {
        private DeckService service = new DeckService();

        // GET: Decks
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAllDecks());
        }

        // GET: Decks/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var deck = await service.GetDeckDetailById((long)id);
            if (deck == null)
                return HttpNotFound();
            return View(deck);
        }

        // POST: Decks/Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details(DeckDetailResponse deck)
        {
            if (ModelState.IsValid)
                deck = await service.Mixing(deck);
                

            return View(deck);
        }

        // GET: Decks/Create
        public async Task<ActionResult> Create()
        {
            var model = await service.CreateDeckModel();
            return View(model);
        }

        // POST: Decks/Create
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

        // GET: Decks/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var deck = await service.GetDeckDetailById((long)id);
            if (deck == null)
                return HttpNotFound();
            return View(deck);
        }

        // POST: Decks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,DateModify")] Deck deck)
        {
            /*if (ModelState.IsValid)
            {
                db.Entry(deck).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }*/
            return View(deck);
        }

        // GET: Decks/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var deck = await service.GetDeckDetailById((long)id);
            if (deck == null)
                return HttpNotFound();
            return View(deck);
        }

        // POST: Decks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            /*Deck deck = await db.Decks.FindAsync(id);
            db.Decks.Remove(deck);
            await db.SaveChangesAsync();*/
            return RedirectToAction("Index");
        }
    }
}
