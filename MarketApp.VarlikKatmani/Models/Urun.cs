using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.VarlikKatmani.Models
{
    [Table("Urunler")]
    public class Urun
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ad { get; set; }
        public int StokAdet { get; set; }
        public decimal Fiyat { get; set; }
        public string Resim { get; set; }

        public int MarkaId { get; set; }
        [ForeignKey("MarkaId")]
        public virtual Marka Marka { get; set; }

        public int KategoriId { get; set; }
        [ForeignKey("KategoriId")]
        public virtual Kategori Kategori { get; set; }
    }
}