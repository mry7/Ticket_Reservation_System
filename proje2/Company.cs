using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace proje2
{
    public class Company : User, IProfitable
    {

        public static string FirmaDogrulamasi { get; private set; }

        public class firmabilgi
        {
            public string KullaniciAdi { get; set; }
            public string Sifre { get; set; }
            public List<string> AracTurleri { get; set; }

            public decimal HizmetBedeli { get; set; }

         
        }


      //FirmaİslemeleriForm da kullanılacak (firma eklemek için) 
        public static List<firmabilgi> firmalar { get; } = new List<firmabilgi>
    {
        new firmabilgi { KullaniciAdi = "A", Sifre = "100", AracTurleri = new List<string>{"Otobus"},HizmetBedeli=1000 },
        new firmabilgi { KullaniciAdi = "B", Sifre = "200", AracTurleri = new List<string>{"Otobus"},HizmetBedeli=1000},
        new firmabilgi { KullaniciAdi = "C", Sifre = "300", AracTurleri = new List<string>{"Otobus", "Uçak"} ,HizmetBedeli=1000 },
        new firmabilgi { KullaniciAdi = "D", Sifre = "400", AracTurleri = new List<string>{"Tren"},HizmetBedeli=1000},
        new firmabilgi { KullaniciAdi = "F", Sifre = "500", AracTurleri = new List<string>{"Uçak"} ,HizmetBedeli=1000 }

    };

        //Company constructer 
        public Company(string kullaniciAdi, string sifre)
        {
            KullaniciAdi = kullaniciAdi;
            Sifre = sifre;
        }

        //Giriş işlemleri
        public override void Giris()
        {
            // Firma özgü giriş işlemleri burada gerçekleştirilir.
            Console.WriteLine($"Firma olarak {KullaniciAdi} giriş yaptı.");
            MessageBox.Show("Giriş başarılı!");

            // Başarılı giriş durumunda giriş yapan firmanın adını sakla
            FirmaDogrulamasi = KullaniciAdi;
        }

        public bool KullaniciGirisi(string kullaniciAdi, string sifre)
        {
            bool sonuc = false;

            foreach (var firmabilgi in firmalar)
            {
                if (firmabilgi.KullaniciAdi == kullaniciAdi && firmabilgi.Sifre == sifre)
                {
                    sonuc = true;
                    break;
                }
            }

            if (!sonuc)
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre!");
            }
            else
            {
                Giris();
            }

            return sonuc;
        }

        //______________________________________________________________________________________________________

        //zarar hesaplama
        public decimal ZararHesapla(DateTime tarih, string firmaAdi, string aracTuru, string aracId)
        {
            decimal toplamYakitMaliyeti = 0;
            decimal toplamPersonelMaasi = 0;
            decimal toplamHizmetBedeli = 0;

            // Belirtilen tarihte, firma adında ve araç türünde sefer var mı kontrol et
            if (Trip.SeferVarMi(tarih, firmaAdi, aracTuru, aracId))
            {
                // Filtrelenmiş seferlerin toplam yakıt maliyetini hesapla
                foreach (var sefer in Trip.Seferler
                    .Where(sefer => sefer.Tarih.Date == tarih.Date && sefer.FirmaAdi == firmaAdi && sefer.AracTuru == aracTuru))
                {
                    // Seferdeki toplam mesafeyi hesapla (gidiş-dönüş olduğu için 2 ile çarp)
                    int toplamMesafe = Route.GuzergahBilgileri.First(guzergah => guzergah.SeferId == sefer.SeferId).Sehirler.Count - 1;

                    // Seferdeki toplam yakıt maliyetini hesapla
                    decimal yakitUcreti = YakitUcretiHesaplaa(sefer.FirmaAdi, sefer.AracTuru);
                    decimal seferYakitMaliyeti = toplamMesafe * yakitUcreti;

                    // Toplam yakıt maliyetini güncelle
                    toplamYakitMaliyeti += seferYakitMaliyeti;

                    // Filtrelenmiş seferlerin toplam personel maaşını hesapla
                    foreach (var personel in Personel.Calisanlar
                        .Where(perrsonel => perrsonel.AracId == sefer.AracId))
                    {
                        // Personelin maaşını topla sadece uygun durumlar için
                        decimal personelMaasi = HesaplaFirmaMaasi(personel, sefer.FirmaAdi, sefer.AracTuru, sefer.FirmaAdi);

                        // 0'dan farklı bir maaş değeri varsa toplam maaşa ekle
                        if (personelMaasi > 0)
                        {
                            toplamPersonelMaasi += personelMaasi;
                        }
                    }
                }

                // Toplam hizmet bedelini hesapla
                var firma = firmalar.FirstOrDefault(firrmaa => firrmaa.KullaniciAdi == firmaAdi);
                toplamHizmetBedeli = firma != null ? firma.HizmetBedeli : 0;

                // Toplam maliyeti hesapla (personel maaşları, yakıt maliyeti ve hizmet bedeli)
                decimal toplamMaliyet = toplamYakitMaliyeti + toplamPersonelMaasi + toplamHizmetBedeli;

                Console.WriteLine($"Toplam Yakıt Maliyeti: {toplamYakitMaliyeti}, Toplam Personel Maaşı: {toplamPersonelMaasi}, Hizmet Bedeli: {toplamHizmetBedeli}, Toplam Maliyet: {toplamMaliyet}");

                return toplamMaliyet;
            }
            else
            {
                MessageBox.Show("Belirtilen tarih, firma adı, araç türü veya araç ID'sine sahip bir sefer bulunamadı.");

                return 0;
            }
        }

        // HesaplaFirmaMaasi fonksiyonu güncellenmiş hali
        private decimal HesaplaFirmaMaasi(Personel personel, string firmaAdi, string aracTuru, string seferFirmaAdi)
        {
            // Personelin çalıştığı firma ile belirtilen firma aynı olmalı ve araç türü de uygun olmalı
            if (personel.FirmaAdi == seferFirmaAdi && personel.AracTuru == aracTuru)
            {
                return personel.Maas;
            }

            return 0;
        }



        //__________________________________________________________________________________________________________________

        //yakıt maliyeti hesaplama (Vehiclede bulunna class ) her bir arac tüür ve firma adı için ayrı oluştuldu
        public decimal YakitUcretiHesaplaa(string firmaAdi, string aracTuru)
        {
            decimal ucret = 0;

            // Firma adına ve araç türüne bağlı olarak ücreti belirle
            switch (aracTuru)
            {
                case "Otobus":
                    ucret = YakitUcretiOtobus(firmaAdi);
                    break;
                case "Tren":
                    ucret = YakitUcretiTren(firmaAdi);
                    break;
                case "Ucak":
                    ucret = YakitUcretiUcak(firmaAdi);
                    break;
                default:
                    ucret = 0; 
                    break;
            }

            return ucret;
        }

        private decimal YakitUcretiOtobus(string firmaAdi)
        {
            // Otobüs için firma adına bağlı olarak ücreti belirle
            switch (firmaAdi)
            {
                case "A":
                    return 10;
                case "B":
                    return 5;
                case "C":
                    return 6;
                default:
                    return 0; 
            }
        }

        private decimal YakitUcretiTren(string firmaAdi)
        {
            // Tren için firma adına bağlı olarak ücreti belirle
            switch (firmaAdi)
            {
                case "D":
                    return 3;
                default:
                    return 0; 
            }
        }

        private decimal YakitUcretiUcak(string firmaAdi)
        {
            // Uçak için firma adına bağlı olarak ücreti belirle
            switch (firmaAdi)
            {
                case "C":
                    return 25;
                case "F":
                    return 20;
                default:
                    return 0; 
            }
        }



    }

}