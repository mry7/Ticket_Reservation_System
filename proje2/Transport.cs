using proje2;
using System;
using System.Collections.Generic;

public class Transport : IReservable
{
    public List<string> Sirketler { get; set; }
    public List<string> Araclar { get; set; }
    public List<string> SeyahatBilgileri { get; set; }
    public bool[] KoltukDurum { get; set; }

        public static List<Trip> SeferBilgileri()
        {
            List<Trip> seferler = new List<Trip>
            {
                new Trip { FirmaAdi = "B", AracTuru = "Otobus", AracId = "b4", SeferId = 4, Fiyat = 600.0, Tarih = DateTime.Parse("07.12.2023") }
            };

            return seferler;
        }

        public static List<Reservation> KoltukRezervasyonlari()
        {
            List<Reservation> Rezervasyon = new List<Reservation>
            {
                new Reservation("b4", 8, "12345678901", true),
            };

            return Rezervasyon;
        }

        public static List<Passenger> YolcuBilgileri()
        {
            List<Passenger> yolcular = new List<Passenger>
            {
                new Passenger { Ad = "Ahmet", Soyad = "Yılmaz", DogumTarihi = new DateTime(1985, 3, 10), Tc = "12345678901" },
            };

            return yolcular;
        }

    public static List<Reservation> YolcuSeyahatBilgileri()
    {
        List<Reservation> Rezervasyon = KoltukRezervasyonlari();
        List<Trip> seferler = SeferBilgileri();
        List<Passenger> yolcular = YolcuBilgileri();

        List<Reservation> seyahatBilgileri = new List<Reservation>();

        foreach (var bilgi in Rezervasyon)
        {
            var yolcuBilgisi = yolcular.Find(yollcu => yollcu.Tc == bilgi.Tc);
            var seferBilgisi = seferler.Find(seferr => seferr.AracId == bilgi.Aracİd);

            if (yolcuBilgisi != null && seferBilgisi != null)
            {
                seyahatBilgileri.Add(new Reservation(seferBilgisi.AracId, bilgi.KoltukId, yolcuBilgisi.Tc, true));
            }
        }

        return seyahatBilgileri;
    }
}




