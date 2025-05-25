using MarketApp.IsKatmani;
using MarketApp.SunumKatmani.Filters;
using MarketApp.VarlikKatmani.Models;
using System.Web.Mvc;

namespace MarketApp.SunumKatmani.Areas.Yonetim.Controllers
{
    public class KullaniciController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult Listele()
        {
            using (var manager = new KullaniciManager())
            {
                return View(manager.Listele());
            }
        }

        [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult Ekle(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                using (var manager = new KullaniciManager())
                {
                    manager.Ekle(kullanici);
                    return RedirectToAction("Listele");
                }
            }
            return View(kullanici);
        }

        [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult Duzenle(string eposta)
        {
            using (var manager = new KullaniciManager())
            {
                var kullanici = manager.GetKullanici(eposta);
                if (kullanici != null)
                {
                    return View(kullanici);
                }
                return HttpNotFound();
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult Duzenle(Kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                using (var manager = new KullaniciManager())
                {
                    manager.Guncelle(kullanici);
                    return RedirectToAction("Listele");
                }
            }
            return View(kullanici);
        }

        [Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult Sil(string eposta)
        {
            if (string.IsNullOrEmpty(eposta))
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new KullaniciManager())
            {
                var kullanici = manager.GetKullanici(eposta);
                if (kullanici == null)
                {
                    return HttpNotFound("Kullanıcı bulunamadı.");
                }
                return View(kullanici);
            }
        }

        [HttpPost, ValidateAntiForgeryToken, Kimlik, Yetki(Rol = Yetkiler.Yonetici)]
        public ActionResult SilConfirmed(string eposta)
        {
            if (string.IsNullOrEmpty(eposta))
            {
                return RedirectToAction("Listele");
            }
            using (var manager = new KullaniciManager())
            {
                var kullanici = manager.GetKullanici(eposta);
                if (kullanici == null)
                {
                    return HttpNotFound("Kullanıcı bulunamadı.");
                }
                manager.Sil(kullanici);
                return RedirectToAction("Listele");
            }
        }

        public ActionResult Login()
        {
            if (Session["user"] == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(Kullanici kullanici)
        {
            if (kullanici != null)
            {
                using (var manager = new KullaniciManager())
                {
                    if (manager.Login(kullanici.EPosta, kullanici.Parola))
                    {
                        var user = manager.GetKullanici(kullanici.EPosta);
                        Session["user"] = user;
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }
            ModelState.AddModelError("", "E-posta veya parola hatalı!");
            return View(kullanici);
        }

        public ActionResult Logout()
        {
            if (Session["user"] != null)
            {
                Session.Remove("user");
            }
            return RedirectToAction("Login");
        }
    }
}