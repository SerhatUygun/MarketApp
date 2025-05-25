using MarketApp.VarlikKatmani;
using MarketApp.VarlikKatmani.Models;
using MarketApp.VeritabaniErisimKatmani.DatabaseContext;
using MarketApp.VeritabaniErisimKatmani.Repositories;
using System;

namespace MarketApp.VeritabaniErisimKatmani
{
    public class UnitOfWork : IDisposable
    {
        private readonly AppDbContext context;
        private UrunRepository urunRepo;
        private Repository<Marka> markaRepo;
        private Repository<Kategori> kategoriRepo;
        private Repository<Satis> satisRepo;
        private SatisDetayRepository satisDetayRepo;
        private KullaniciRepository kullaniciRepo;

        public UrunRepository UrunWork => urunRepo ?? (urunRepo = new UrunRepository(context));
        public Repository<Marka> MarkaWork => markaRepo ?? (markaRepo = new Repository<Marka>(context));
        public Repository<Kategori> KategoriWork => kategoriRepo ?? (kategoriRepo = new Repository<Kategori>(context));
        public Repository<Satis> SatisWork => satisRepo ?? (satisRepo = new Repository<Satis>(context));
        public SatisDetayRepository SatisDetayWork => satisDetayRepo ?? (satisDetayRepo = new SatisDetayRepository(context));
        public KullaniciRepository KullaniciWork => kullaniciRepo ?? (kullaniciRepo = new KullaniciRepository(context));

        public UnitOfWork()
        {
            context = new AppDbContext();
        }

        public void Save()
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Dispose()
        {
            urunRepo?.Dispose();
            markaRepo?.Dispose();
            kategoriRepo?.Dispose();
            satisRepo?.Dispose();
            satisDetayRepo?.Dispose();
            kullaniciRepo?.Dispose();
            context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}