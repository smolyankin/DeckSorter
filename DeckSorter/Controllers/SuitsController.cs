using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using DeckSorter.Models;
using DeckSorter.Request;
using DeckSorter.Services;

namespace DeckSorter.Controllers
{
    /// <summary>
    /// контроллер мастей
    /// </summary>
    public class SuitsController : Controller
    {
        private SuitService service = new SuitService();

        /// <summary>
        /// список мастей
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAllSuits());
        }

        /// <summary>
        /// подробно о масти
        /// </summary>
        /// <param name="id">ид масти</param>
        /// <returns></returns>
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var suit = await service.GetSuitById((long)id);
            if (suit == null)
                return HttpNotFound();
            return View(suit);
        }

        /// <summary>
        /// создание масти
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View(new CreateSuitRequest());
        }

        /// <summary>
        /// создать масть
        /// </summary>
        /// <param name="suit"></param>
        /// <returns></returns>
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

        /// <summary>
        /// изменение масти
        /// </summary>
        /// <param name="id">ид масти</param>
        /// <returns></returns>
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var suit = await service.GetSuitById((long)id);
            if (suit == null)
                return HttpNotFound();
            return View(suit);
        }

        /// <summary>
        /// изменить масть
        /// </summary>
        /// <param name="suit"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Suit suit)
        {
            if (ModelState.IsValid)
                return View(await service.EditSuit(suit));
            return View(suit);
        }

        /// <summary>
        /// удаление масти
        /// </summary>
        /// <param name="id">ид масти</param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var suit = await service.GetSuitById((long)id);
            if (suit == null)
                return HttpNotFound();
            return View(suit);
        }

        /// <summary>
        /// удалить масть
        /// </summary>
        /// <param name="id">ид масти</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await service.DeleteSuit(id);

            return RedirectToAction("Index");
        }
    }
}
