using MarketApp.IsKatmani;
using MarketApp.SunumKatmani.Filters;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MarketApp.SunumKatmani.Areas.Yonetim.Controllers
{
    public class SatisController : Controller
    {
        public ActionResult Listele()
        {
            using (var manager = new SatisManager())
            {
                return View(manager.Listele());
            }
        }

        public ActionResult SatisDetay(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new SatisDetayManager())
            {
                var detaylar = manager.GetSatisDetay(id.Value);
                if (detaylar == null)
                {
                    return HttpNotFound("Satış detayı bulunamadı.");
                }
                return View(detaylar);
            }
        }

        [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new SatisManager())
            {
                var satis = manager.GetSatis(id.Value);
                if (satis == null)
                {
                    return HttpNotFound("Satış bulunamadı.");
                }
                return View(satis);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult SilConfirmed(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new SatisManager())
            {
                var satis = manager.GetSatis(id.Value);
                if (satis == null)
                {
                    return HttpNotFound("Satış bulunamadı.");
                }
                manager.Sil(satis);
                return RedirectToAction("Listele");
            }
        }

        [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult SatisEkle()
        {
            using (var uow = new UnitOfWork())
            {
                var urunler = uow.UrunWork.GetAllWithMarkaAndKategori();
                if (urunler == null || !urunler.Any())
                {
                    return RedirectToAction("Listele");
                }
                return View(urunler);
            }
        }

        [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult SepeteEkle(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("SatisEkle");
            }
            var sepet = Session["sepet"] as Sepet ?? new Sepet();
            using (var uow = new UnitOfWork())
            {
                var urun = uow.UrunWork.GetItem(id.Value);
                if (urun == null)
                {
                    return HttpNotFound("Ürün bulunamadı.");
                }
                sepet.Ekle(urun);
                Session["sepet"] = sepet;
                TempData["SuccessMessage"] = $"{urun.Ad} sepete eklendi!";
            }
            return RedirectToAction("SatisEkle");
        }

        [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult SepetGoruntule()
        {
            var sepet = Session["sepet"] as Sepet ?? new Sepet();
            if (sepet.Satis == null)
            {
                return RedirectToAction("SatisEkle");
            }
            return View(sepet.Satis);
        }

        [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult SatisTamamla()
        {
            var sepet = Session["sepet"] as Sepet;
            if (sepet == null || sepet.Satis == null || !sepet.Satis.Detaylar.Any())
            {
                return RedirectToAction("SatisEkle");
            }
            using (var uow = new UnitOfWork())
            {
                sepet.Satis.TarihSaat = DateTime.Now;
                uow.SatisWork.Add(sepet.Satis);
                foreach (var item in sepet.Satis.Detaylar)
                {
                    var urun = uow.UrunWork.GetItem(item.Urun.Id);
                    if (urun == null || urun.StokAdet < item.Adet)
                    {
                        ModelState.AddModelError("", $"Ürün '{item.Urun.Ad}' için yeterli stok yok.");
                        return RedirectToAction("SepetGoruntule");
                    }
                    urun.StokAdet -= item.Adet;
                    uow.UrunWork.Update(urun);
                }
                uow.Save();
                Session["sepet"] = null;
            }
            return RedirectToAction("SatisEkle");
        }
    }
}