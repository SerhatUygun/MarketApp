using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using System.Collections.Generic;

namespace MarketApp.VeritabaniErisimKatmani.Repositories.Base
{
    public interface ISatisDetayRepository : IRepository<SatisDetay>
    {
        IEnumerable<SatisDetay> GetAllWithUrun();
        IEnumerable<SatisDetay> GetAllWithUrun(int satisId);
    }
}