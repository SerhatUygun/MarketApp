using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.VarlikKatmani.Models
{
    public enum Yetkiler
    {
        [Display(Name = "Yönetici")]
        Yonetici = 1,
        [Display(Name = "Kasiyer")]
        Kasiyer = 2
    }

    [Table("Kullanicilar")]
    public class Kullanici
    {
        [Key]
        public string EPosta { get; set; }
        public string Parola { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public Yetkiler Yetki { get; set; }
    }
}