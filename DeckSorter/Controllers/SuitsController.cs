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
    public class SuitsController : Controller
    {
        private DeckContext db = new DeckContext();

        // GET: Suits
        public async Task<ActionResult> Index()
        {
            return View(await db.Suits.ToListAsync());
        }

        // GET: Suits/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suit suit = await db.Suits.FindAsync(id);
            if (suit == null)
            {
                return HttpNotFound();
            }
            return View(suit);
        }

        // GET: Suits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suits/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title")] Suit suit)
        {
            if (ModelState.IsValid)
            {
                db.Suits.Add(suit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(suit);
        }

        // GET: Suits/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suit suit = await db.Suits.FindAsync(id);
            if (suit == null)
            {
                return HttpNotFound();
            }
            return View(suit);
        }

        // POST: Suits/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] Suit suit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(suit);
        }

        // GET: Suits/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suit suit = await db.Suits.FindAsync(id);
            if (suit == null)
            {
                return HttpNotFound();
            }
            return View(suit);
        }

        // POST: Suits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Suit suit = await db.Suits.FindAsync(id);
            db.Suits.Remove(suit);
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
