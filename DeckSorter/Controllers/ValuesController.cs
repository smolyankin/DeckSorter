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

namespace DeckSorter.Controllers
{
    public class ValuesController : Controller
    {
        private DeckService service = new DeckService();

        // GET: Values
        public async Task<ActionResult> Index()
        {

            var values = await service.GetAllValues();

            return View(values);
        }

        // GET: Values/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var value = await service.GetValueById((long)id);
            if (value == null)
                return HttpNotFound();
            return View(value);
        }

        // GET: Values/Create
        public ActionResult Create()
        {
            var request = new CreateValueRequest();
            return View(request);
        }

        // POST: Values/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateValueRequest request)
        {
            if (ModelState.IsValid)
            {
                await service.CreateValue(request);

                return RedirectToAction("Index");
            }

            return View(request);
        }

        // GET: Values/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var value = await service.GetValueById((long)id);
            if (value == null)
                return HttpNotFound();
            return View(value);
        }

        // POST: Values/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title")] Value value)
        {
            /*if (ModelState.IsValid)
            {
                db.Entry(value).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }*/
            return View(value);
        }

        // GET: Values/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var value = await service.GetValueById((long)id);
            if (value == null)
                return HttpNotFound();
            return View(value);
        }

        // POST: Values/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            /*Value value = await db.Values.FindAsync(id);
            db.Values.Remove(value);
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
