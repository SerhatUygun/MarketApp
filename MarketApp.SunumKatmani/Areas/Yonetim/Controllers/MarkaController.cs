using MarketApp.IsKatmani;
using MarketApp.SunumKatmani.Filters;
using MarketApp.VarlikKatmani.Models;
using System.Web.Mvc;

namespace MarketApp.SunumKatmani.Areas.Yonetim.Controllers
{
    [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
    public class MarkaController : Controller
    {
        public ActionResult Listele()
        {
            using (var manager = new MarkaManager())
            {
                return View(manager.Listele());
            }
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ekle(Marka marka)
        {
            if (ModelState.IsValid)
            {
                using (var manager = new MarkaManager())
                {
                    manager.Ekle(marka);
                    return RedirectToAction("Listele");
                }
            }
            ModelState.AddModelError("", "Ekleme başarısız!");
            return View(marka);
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new MarkaManager())
            {
                var marka = manager.GetMarka(id.Value);
                if (marka == null)
                {
                    return HttpNotFound("Marka bulunamadı.");
                }
                return View(marka);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Marka marka)
        {
            if (ModelState.IsValid)
            {
                using (var manager = new MarkaManager())
                {
                    manager.Guncelle(marka);
                    return RedirectToAction("Listele");
                }
            }
            ModelState.AddModelError("", "Düzenleme başarısız!");
            return View(marka);
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new MarkaManager())
            {
                var marka = manager.GetMarka(id.Value);
                if (marka == null)
                {
                    return HttpNotFound("Marka bulunamadı.");
                }
                return View(marka);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SilConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new MarkaManager())
            {
                var marka = manager.GetMarka(id.Value);
                if (marka == null)
                {
                    return HttpNotFound("Marka bulunamadı.");
                }
                manager.Sil(marka);
                return RedirectToAction("Listele");
            }
        }
    }
}