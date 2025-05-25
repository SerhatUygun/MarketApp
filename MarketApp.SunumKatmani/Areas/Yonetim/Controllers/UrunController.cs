using MarketApp.IsKatmani;
using MarketApp.SunumKatmani.Filters;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani;
using System.Web.Mvc;

namespace MarketApp.SunumKatmani.Areas.Yonetim.Controllers
{
    [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
    public class UrunController : Controller
    {
        public ActionResult Listele()
        {
            using (var manager = new UrunManager())
            {
                return View(manager.Listele());
            }
        }

        public ActionResult Ekle()
        {
            using (var uow = new UnitOfWork())
            {
                ViewBag.Markalar = new SelectList(uow.MarkaWork.GetAll(), "Id", "Ad");
                ViewBag.Kategoriler = new SelectList(uow.KategoriWork.GetAll(), "Id", "Ad");
            }
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Ekle(Urun urun)
        {
            if (ModelState.IsValid)
            {
                using (var manager = new UrunManager())
                {
                    manager.Ekle(urun);
                    return RedirectToAction("Listele");
                }
            }
            using (var uow = new UnitOfWork())
            {
                ViewBag.Markalar = new SelectList(uow.MarkaWork.GetAll(), "Id", "Ad");
                ViewBag.Kategoriler = new SelectList(uow.KategoriWork.GetAll(), "Id", "Ad");
            }
            ModelState.AddModelError("", "Ekleme başarısız!");
            return View(urun);
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new UrunManager())
            {
                var urun = manager.GetUrun(id.Value);
                if (urun == null)
                {
                    return HttpNotFound("Ürün bulunamadı.");
                }
                using (var uow = new UnitOfWork())
                {
                    ViewBag.Markalar = new SelectList(uow.MarkaWork.GetAll(), "Id", "Ad");
                    ViewBag.Kategoriler = new SelectList(uow.KategoriWork.GetAll(), "Id", "Ad");
                }
                return View(urun);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Duzenle(Urun urun)
        {
            if (ModelState.IsValid)
            {
                using (var manager = new UrunManager())
                {
                    manager.Guncelle(urun);
                    return RedirectToAction("Listele");
                }
            }
            using (var uow = new UnitOfWork())
            {
                ViewBag.Markalar = new SelectList(uow.MarkaWork.GetAll(), "Id", "Ad");
                ViewBag.Kategoriler = new SelectList(uow.KategoriWork.GetAll(), "Id", "Ad");
            }
            ModelState.AddModelError("", "Düzenleme başarısız!");
            return View(urun);
        }

        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new UrunManager())
            {
                var urun = manager.GetUrun(id.Value);
                if (urun == null)
                {
                    return HttpNotFound("Ürün bulunamadı.");
                }
                return View(urun);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SilConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new UrunManager())
            {
                var urun = manager.GetUrun(id.Value);
                if (urun == null)
                {
                    return HttpNotFound("Ürün bulunamadı.");
                }
                manager.Sil(urun);
                return RedirectToAction("Listele");
            }
        }
    }
}