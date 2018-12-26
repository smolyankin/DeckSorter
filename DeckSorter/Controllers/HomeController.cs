using System.Web.Mvc;

namespace DeckSorter.Controllers
{
    /// <summary>
    /// основной контроллер
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Сортировщик колод";

            return View();
        }
    }
}
