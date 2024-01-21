using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public class Customer
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }


        // Bilet alma işlemi
        public void MusteriBiletAlma(string VarisYeri, DateTime Tarih, int kisiSayisi)
        {
            Console.WriteLine($"{Ad} {Soyad}, {VarisYeri} noktasına {Tarih} tarihinde {kisiSayisi} kişilik bilet aldı.");
        }

        public void MusteriBilgileriGoster()
        {
            Console.WriteLine($"Müşteri Bilgileri:\nAd: {Ad}\nSoyad: {Soyad}\nTelefon: {Telefon}\n");
        }
    }

}
