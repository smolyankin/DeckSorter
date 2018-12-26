using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Services;

namespace DeckSorter.Controllers
{
    /// <summary>
    /// контроллер значений
    /// </summary>
    public class ValuesController : Controller
    {
        private ValueService service = new ValueService();

        /// <summary>
        /// список значений
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAllValues());
        }

        /// <summary>
        /// подробно о значении
        /// </summary>
        /// <param name="id">ид значения</param>
        /// <returns></returns>
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var value = await service.GetValueById((long)id);
            if (value == null)
                return HttpNotFound();
            return View(value);
        }

        /// <summary>
        /// создание значения
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View(new CreateValueRequest());
        }

        /// <summary>
        /// создать значение
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// изменение значения
        /// </summary>
        /// <param name="id">ид значения</param>
        /// <returns></returns>
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var value = await service.GetValueById((long)id);
            if (value == null)
                return HttpNotFound();
            return View(value);
        }

        /// <summary>
        /// изменить значение
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Value value)
        {
            if (ModelState.IsValid)
                return View(await service.EditValue(value));
            return View(value);
        }

        /// <summary>
        /// удаление значения
        /// </summary>
        /// <param name="id">ид значения</param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var value = await service.GetValueById((long)id);
            if (value == null)
                return HttpNotFound();
            return View(value);
        }

        /// <summary>
        /// удалить значение
        /// </summary>
        /// <param name="id">ид значения</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await service.DeleteValue(id);

            return RedirectToAction("Index");
        }
    }
}
