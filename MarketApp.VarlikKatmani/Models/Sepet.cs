using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.VarlikKatmani.Models
{
    public class Sepet
    {
        public Satis Satis { get; set; }

        public Sepet()
        {
            Satis = new Satis();
        }

        public void Ekle(Urun urun)
        {
            var existingDetay = Satis.Detaylar.FirstOrDefault(x => x.Urun.Id == urun.Id);
            if (existingDetay == null)
            {
                Satis.Detaylar.Add(new SatisDetay { Adet = 1, Urun = urun, Tutar = urun.Fiyat });
                Satis.ToplamTutar += urun.Fiyat;
            }
            else
            {
                existingDetay.Adet++;
                existingDetay.Tutar += urun.Fiyat;
                Satis.ToplamTutar += urun.Fiyat;
            }
        }

        public void Sil(int urunId)
        {
            var index = Satis.Detaylar.FindIndex(x => x.Id == urunId);
            if (index != -1)
            {
                Satis.Detaylar.RemoveAt(index);
            }
        }
    }
}