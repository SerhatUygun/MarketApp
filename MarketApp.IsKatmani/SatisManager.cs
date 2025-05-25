using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketApp.IsKatmani
{
    public class SatisManager : IDisposable
    {
        private readonly UnitOfWork unitOfWork;

        public SatisManager()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<Satis> Listele()
        {
            return unitOfWork.SatisWork.GetAll().ToList();
        }

        public Satis GetSatis(int id)
        {
            return unitOfWork.SatisWork.GetItem(id);
        }

        public void Ekle(Satis satis)
        {
            unitOfWork.SatisWork.Add(satis);
            unitOfWork.Save();
        }

        public void Guncelle(Satis satis)
        {
            unitOfWork.SatisWork.Update(satis);
            unitOfWork.Save();
        }

        public void Sil(Satis satis)
        {
            unitOfWork.SatisWork.Remove(satis);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}