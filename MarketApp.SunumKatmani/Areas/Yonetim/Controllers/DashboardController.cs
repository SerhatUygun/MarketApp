using MarketApp.SunumKatmani.Filters;
using System.Web.Mvc;

namespace MarketApp.SunumKatmani.Areas.Yonetim.Controllers
{
    [Kimlik]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}