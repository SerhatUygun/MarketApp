using MarketApp.VarlikKatmani.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.VeritabaniErisimKatmani.DatabaseContext
{
    // AppDbContext sınıfı, veritabanı ile iletişim kurar ve tabloları temsil eden varlıkları tanımlar.
    public class AppDbContext : DbContext
    {
        // Veritabanındaki tabloları temsil eden özellikler
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Marka> Markalar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Satis> Satislar { get; set; }
        public DbSet<SatisDetay> SatisDetaylari { get; set; }

        // Yapıcı metod, veritabanı bağlantı dizesini belirtir ve başlatıcıyı ayarlar
        public AppDbContext() : base("serhat")
        {
            Database.SetInitializer<AppDbContext>(new MyInitializer());
        }
    }

    // MyInitializer sınıfı, veritabanı oluşturulduğunda örnek verilerle doldurulmasını sağlar
    public class MyInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // Kullanıcılar ekleniyor
            context.Kullanicilar.Add(new Kullanici
            {
                EPosta = "admin@gmail.com",
                Ad = "Yönetici",
                Soyad = "Admin",
                Parola = "admin123",
                Yetki = Yetkiler.Yonetici
            });
            context.Kullanicilar.Add(new Kullanici
            {
                EPosta = "kasiyer@gmail.com",
                Ad = "Kasiyer",
                Soyad = "Çalışan",
                Parola = "kasiyer123",
                Yetki = Yetkiler.Kasiyer
            });

            // Markalar ekleniyor
            var marka1 = context.Markalar.Add(new Marka { Ad = "Nestle" });
            var marka2 = context.Markalar.Add(new Marka { Ad = "Coca-Cola" });
            var marka3 = context.Markalar.Add(new Marka { Ad = "Pınar" });

            // Kategoriler ekleniyor
            var kategori1 = context.Kategoriler.Add(new Kategori { Ad = "Gıda" });
            var kategori2 = context.Kategoriler.Add(new Kategori { Ad = "İçecek" });
            var kategori3 = context.Kategoriler.Add(new Kategori { Ad = "Atıştırmalık" });

            // Değişiklikler kaydediliyor
            context.SaveChanges();

            // Ürünler ekleniyor
            var urun1 = context.Urunler.Add(new Urun
            {
                Ad = "Çikolata",
                Marka = marka1,
                Kategori = kategori3,
                StokAdet = 50,
                Fiyat = 7.5m,
                Resim = "~/Images/cikolata.jpg"
            });
            var urun2 = context.Urunler.Add(new Urun
            {
                Ad = "Kola",
                Marka = marka2,
                Kategori = kategori2,
                StokAdet = 100,
                Fiyat = 4.0m,
                Resim = "~/Images/kola.jpg"
            });
            var urun3 = context.Urunler.Add(new Urun
            {
                Ad = "Süt",
                Marka = marka3,
                Kategori = kategori1,
                StokAdet = 80,
                Fiyat = 6.0m,
                Resim = "~/Images/sut.jpg"
            });

            // Satışlar ekleniyor
            var satis1 = context.Satislar.Add(new Satis
            {
                TarihSaat = DateTime.Now,
                ToplamTutar = 0
            });
            var satis2 = context.Satislar.Add(new Satis
            {
                TarihSaat = DateTime.Now.AddDays(-1),
                ToplamTutar = 0
            });

            // Satış detayları ekleniyor
            satis1.Detaylar = new System.Collections.Generic.List<SatisDetay>
            {
                new SatisDetay { Urun = urun1, Adet = 3, Tutar = urun1.Fiyat * 3 },
                new SatisDetay { Urun = urun2, Adet = 2, Tutar = urun2.Fiyat * 2 }
            };
            satis1.ToplamTutar = satis1.Detaylar.Sum(d => d.Tutar);

            satis2.Detaylar = new System.Collections.Generic.List<SatisDetay>
            {
                new SatisDetay { Urun = urun2, Adet = 5, Tutar = urun2.Fiyat * 5 },
                new SatisDetay { Urun = urun3, Adet = 1, Tutar = urun3.Fiyat * 1 }
            };
            satis2.ToplamTutar = satis2.Detaylar.Sum(d => d.Tutar);

            // Değişiklikler kaydediliyor
            context.SaveChanges();
            base.Seed(context);
        }
    }
}