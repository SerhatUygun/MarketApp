using MarketApp.IsKatmani;
using MarketApp.SunumKatmani.Filters;
using MarketApp.VarlikKatmani.Models;
using System.Web.Mvc;

namespace MarketApp.SunumKatmani.Areas.Yonetim.Controllers
{
    [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
    public class KategoriController : Controller
    {
        public ActionResult Listele()
        {
            using (var manager = new KategoriManager())
            {
                return View(manager.Listele());
            }
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ekle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                using (var manager = new KategoriManager())
                {
                    manager.Ekle(kategori);
                    return RedirectToAction("Listele");
                }
            }
            ModelState.AddModelError("", "Ekleme başarısız!");
            return View(kategori);
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new KategoriManager())
            {
                var kategori = manager.GetKategori(id.Value);
                if (kategori == null)
                {
                    return HttpNotFound("Kategori bulunamadı.");
                }
                return View(kategori);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                using (var manager = new KategoriManager())
                {
                    manager.Guncelle(kategori);
                    return RedirectToAction("Listele");
                }
            }
            ModelState.AddModelError("", "Düzenleme başarısız!");
            return View(kategori);
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new KategoriManager())
            {
                var kategori = manager.GetKategori(id.Value);
                if (kategori == null)
                {
                    return HttpNotFound("Kategori bulunamadı.");
                }
                return View(kategori);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SilConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new KategoriManager())
            {
                var kategori = manager.GetKategori(id.Value);
                if (kategori == null)
                {
                    return HttpNotFound("Kategori bulunamadı.");
                }
                manager.Sil(kategori);
                return RedirectToAction("Listele");
            }
        }
    }
}