using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.VarlikKatmani.Models
{
    [Table("tblSatislar")]
    public class Satis
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TarihSaat { get; set; }
        public decimal ToplamTutar { get; set; }
        public virtual List<SatisDetay> Detaylar { get; set; }

        public Satis()
        {
            Detaylar = new List<SatisDetay>();
        }
    }
}