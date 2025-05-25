using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.VarlikKatmani.Models
{
    [Table("SatisDetaylari")]
    public class SatisDetay
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Adet { get; set; }
        public decimal Tutar { get; set; }

        [ForeignKey("Urun")]
        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; }

        public int SatisId { get; set; }
        [ForeignKey("SatisId")]
        public virtual Satis Satis { get; set; }
    }
}