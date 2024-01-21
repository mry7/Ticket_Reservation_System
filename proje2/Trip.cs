using System.Collections.Generic;
using System;
using System.Linq;

public class Trip
{
    public string AracTuru { get; set; }
    public string AracId { get; set; }
    public string FirmaAdi { get; set; }
    public int SeferId { get; set; }
    public double Fiyat { get; set; }
    public DateTime Tarih { get; set; }

    public static List<Trip> Seferler = new List<Trip>();

    public static List<Trip> SeferBilgileri()
    {
        if (Seferler.Count == 0)
        {
            Seferler.Add(new Trip { FirmaAdi = "A", AracTuru = "Otobus", AracId = "b1", SeferId = 3, Fiyat = 900.0, Tarih = DateTime.Parse("05.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "A", AracTuru = "Otobus", AracId = "b2", SeferId = 3, Fiyat = 900.0, Tarih = DateTime.Parse("09.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "B", AracTuru = "Otobus", AracId = "b3", SeferId = 3, Fiyat = 900.0, Tarih = DateTime.Parse("06.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "B", AracTuru = "Otobus", AracId = "b4", SeferId = 4, Fiyat = 600.0, Tarih = DateTime.Parse("07.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "C", AracTuru = "Otobus", AracId = "b5", SeferId = 4, Fiyat = 600.0, Tarih = DateTime.Parse("08.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "C", AracTuru = "Ucak", AracId = "a1", SeferId = 5, Fiyat = 2400.0, Tarih = DateTime.Parse("10.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "C", AracTuru = "Ucak", AracId = "a2", SeferId = 5, Fiyat = 2400.0, Tarih = DateTime.Parse("05.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "D", AracTuru = "Tren", AracId = "t1", SeferId = 1, Fiyat = 500.0, Tarih = DateTime.Parse("06.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "D", AracTuru = "Tren", AracId = "t2", SeferId = 2, Fiyat = 600.0, Tarih = DateTime.Parse("07.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "D", AracTuru = "Tren", AracId = "t3", SeferId = 2, Fiyat = 600.0, Tarih = DateTime.Parse("08.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "F", AracTuru = "Ucak", AracId = "a3", SeferId = 6, Fiyat = 2000.0, Tarih = DateTime.Parse("09.12.2023") });
            Seferler.Add(new Trip { FirmaAdi = "F", AracTuru = "Ucak", AracId = "a4", SeferId = 6, Fiyat = 2000.0, Tarih = DateTime.Parse("10.12.2023") });
        }

        return Seferler;
    }

    public static bool SeferVarMi(DateTime tarih, string firmaAdi,string aracTuru, string aracId)
    {
        // Belirtilen tarihte, firma adında, araç türünde ve aracId'si ile sefer var mı kontrol et
        return Seferler.Any(sefer => sefer.Tarih.Date == tarih.Date && sefer.FirmaAdi == firmaAdi && sefer.AracTuru==aracTuru && sefer.AracId == aracId);
    }


    public static Dictionary<int, string> SeferIdSehirleri = new Dictionary<int, string>
    {
        { 1, "Istanbul - Kocaeli - Bilecik - Ankara - Eskişehir - Konya" },
        { 2, "Istanbul - Kocaeli - Bilecik - Eskişehir - Konya" },
        { 3, "Istanbul - Kocaeli - Ankara - Eskişehir - Konya" },
        { 4, "Istanbul - Kocaeli - Eskişehir - Ankara - Kocaeli - Istanbul" },
        { 5, "Istanbul - Ankara - Konya - Istanbul" },
        { 6, "Istanbul - Ankara - Istanbul" }
    };





}
