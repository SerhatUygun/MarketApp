using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketApp.IsKatmani
{
    public class UrunManager : IDisposable
    {
        private readonly UnitOfWork unitOfWork;

        public UrunManager()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<Urun> Listele()
        {
            return unitOfWork.UrunWork.GetAllWithMarkaAndKategori().ToList();
        }

        public Urun GetUrun(int id)
        {
            return unitOfWork.UrunWork.GetUrunWithMarkaAndKategori(id); // Yeni metod kullanılıyor
        }

        public void Ekle(Urun urun)
        {
            unitOfWork.UrunWork.Add(urun);
            unitOfWork.Save();
        }

        public void Guncelle(Urun urun)
        {
            unitOfWork.UrunWork.Update(urun);
            unitOfWork.Save();
        }

        public void Sil(Urun urun)
        {
            unitOfWork.UrunWork.Remove(urun);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}