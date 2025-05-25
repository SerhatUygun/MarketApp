using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MarketApp.VeritabaniErisimKatmani.Repositories
{
    public class SatisDetayRepository : Repository<SatisDetay>, ISatisDetayRepository
    {
        public SatisDetayRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<SatisDetay> GetAllWithUrun()
        {
            return context.Set<SatisDetay>().Include(s => s.Urun).ToList();
        }

        public IEnumerable<SatisDetay> GetAllWithUrun(int satisId)
        {
            return context.Set<SatisDetay>().Include(s => s.Urun).Where(s => s.SatisId == satisId).ToList();
        }
    }
}