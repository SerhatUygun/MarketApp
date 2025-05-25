using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketApp.IsKatmani
{
    public class KullaniciManager : IDisposable
    {
        private readonly UnitOfWork unitOfWork;

        public KullaniciManager()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<Kullanici> Listele()
        {
            return unitOfWork.KullaniciWork.GetAll().ToList();
        }

        public Kullanici GetKullanici(string eposta)
        {
            return unitOfWork.KullaniciWork.GetItem(eposta);
        }

        public void Ekle(Kullanici kullanici)
        {
            unitOfWork.KullaniciWork.Add(kullanici);
            unitOfWork.Save();
        }

        public void Guncelle(Kullanici kullanici)
        {
            unitOfWork.KullaniciWork.Update(kullanici);
            unitOfWork.Save();
        }

        public void Sil(Kullanici kullanici)
        {
            unitOfWork.KullaniciWork.Remove(kullanici);
            unitOfWork.Save();
        }

        public bool Login(string eposta, string parola)
        {
            return unitOfWork.KullaniciWork.Login(eposta, parola);
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}