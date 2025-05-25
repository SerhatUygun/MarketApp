using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketApp.IsKatmani
{
    public class SatisDetayManager : IDisposable
    {
        private readonly UnitOfWork unitOfWork;

        public SatisDetayManager()
        {
            unitOfWork = new UnitOfWork();
        }

        public List<SatisDetay> Listele()
        {
            return unitOfWork.SatisDetayWork.GetAllWithUrun().ToList();
        }

        public List<SatisDetay> GetSatisDetay(int satisId)
        {
            return unitOfWork.SatisDetayWork.GetAllWithUrun(satisId).ToList();
        }

        public void Ekle(SatisDetay detay)
        {
            unitOfWork.SatisDetayWork.Add(detay);
            unitOfWork.Save();
        }

        public void Guncelle(SatisDetay detay)
        {
            unitOfWork.SatisDetayWork.Update(detay);
            unitOfWork.Save();
        }

        public void Sil(SatisDetay detay)
        {
            unitOfWork.SatisDetayWork.Remove(detay);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}