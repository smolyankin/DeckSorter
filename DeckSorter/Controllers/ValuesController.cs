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
using DeckSorter.Services;

namespace DeckSorter.Controllers
{
    public class ValuesController : Controller
    {
        private ValueService service = new ValueService();

        // GET: Values
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAllValues());
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
            return View(new CreateValueRequest());
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
        public async Task<ActionResult> Edit(Value value)
        {
            if (ModelState.IsValid)
                return View(await service.EditValue(value));
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
            await service.DeleteValue(id);

            return RedirectToAction("Index");
        }
    }
}
