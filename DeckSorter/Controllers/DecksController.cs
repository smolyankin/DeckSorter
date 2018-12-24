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

namespace DeckSorter.Controllers
{
    public class DecksController : Controller
    {
        private DeckContext db = new DeckContext();

        // GET: Decks
        public async Task<ActionResult> Index()
        {
            return View(await db.Decks.ToListAsync());
        }

        // GET: Decks/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = await db.Decks.FindAsync(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // GET: Decks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Decks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,DateModify")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                db.Decks.Add(deck);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(deck);
        }

        // GET: Decks/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = await db.Decks.FindAsync(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // POST: Decks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,DateModify")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deck).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(deck);
        }

        // GET: Decks/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = await db.Decks.FindAsync(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // POST: Decks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Deck deck = await db.Decks.FindAsync(id);
            db.Decks.Remove(deck);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
