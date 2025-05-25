using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani.Repositories.Base;
using System.Data.Entity;
using System.Linq;

namespace MarketApp.VeritabaniErisimKatmani.Repositories
{
    public class KullaniciRepository : Repository<Kullanici>, IKullaniciRepository
    {
        public KullaniciRepository(DbContext context) : base(context)
        {
        }

        public bool Login(string eposta, string parola)
        {
            return context.Set<Kullanici>().Any(x => x.EPosta.ToLower() == eposta.ToLower() && x.Parola == parola);
        }
    }
}