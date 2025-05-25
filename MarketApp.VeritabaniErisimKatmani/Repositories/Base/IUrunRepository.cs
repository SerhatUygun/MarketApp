using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using System.Collections.Generic;

namespace MarketApp.VeritabaniErisimKatmani.Repositories.Base
{
    public interface IUrunRepository : IRepository<Urun>
    {
        IEnumerable<Urun> GetAllWithMarkaAndKategori();
        Urun GetUrunWithMarkaAndKategori(int id); // Yeni metod
    }
}