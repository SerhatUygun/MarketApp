using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketApp.IsKatmani
{
    public class KategoriManager : IDisposable
    {
        private readonly UnitOfWork unitOfWork;

        public KategoriManager()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<Kategori> Listele()
        {
            return unitOfWork.KategoriWork.GetAll().ToList();
        }

        public Kategori GetKategori(int id)
        {
            return unitOfWork.KategoriWork.GetItem(id);
        }

        public void Ekle(Kategori kategori)
        {
            unitOfWork.KategoriWork.Add(kategori);
            unitOfWork.Save();
        }

        public void Guncelle(Kategori kategori)
        {
            unitOfWork.KategoriWork.Update(kategori);
            unitOfWork.Save();
        }

        public void Sil(Kategori kategori)
        {
            unitOfWork.KategoriWork.Remove(kategori);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}