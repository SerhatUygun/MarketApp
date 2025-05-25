using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketApp.IsKatmani
{
    public class MarkaManager : IDisposable
    {
        private readonly UnitOfWork unitOfWork;

        public MarkaManager()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<Marka> Listele()
        {
            return unitOfWork.MarkaWork.GetAll().ToList();
        }

        public Marka GetMarka(int id)
        {
            return unitOfWork.MarkaWork.GetItem(id);
        }

        public void Ekle(Marka marka)
        {
            unitOfWork.MarkaWork.Add(marka);
            unitOfWork.Save();
        }

        public void Guncelle(Marka marka)
        {
            unitOfWork.MarkaWork.Update(marka);
            unitOfWork.Save();
        }

        public void Sil(Marka marka)
        {
            unitOfWork.MarkaWork.Remove(marka);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}