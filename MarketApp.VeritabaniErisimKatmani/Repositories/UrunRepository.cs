using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MarketApp.VeritabaniErisimKatmani.Repositories
{
    public class UrunRepository : Repository<Urun>, IUrunRepository
    {
        public UrunRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Urun> GetAllWithMarkaAndKategori()
        {
            return context.Set<Urun>().Include(u => u.Marka).Include(u => u.Kategori).ToList();
        }

        // Yeni metod ekleniyor
        public Urun GetUrunWithMarkaAndKategori(int id)
        {
            return context.Set<Urun>()
                .Include(u => u.Marka)
                .Include(u => u.Kategori)
                .FirstOrDefault(u => u.Id == id);
        }
    }
}