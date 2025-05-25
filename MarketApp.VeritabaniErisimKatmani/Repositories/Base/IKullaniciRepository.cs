using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;

namespace MarketApp.VeritabaniErisimKatmani.Repositories.Base
{
    public interface IKullaniciRepository : IRepository<Kullanici>
    {
        bool Login(string eposta, string parola);
    }
}