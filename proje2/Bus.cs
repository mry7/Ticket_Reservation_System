using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public class Bus : Vehicle
    {

        public int SeferId { get; set; }
        public static List<Trip> OtobusSeferList { get; set; } = new List<Trip>();
        public static List<Bus> OtobusList { get; set; } = new List<Bus>();



        public static void OtobuslistesineEkle()
        {
            OtobusList.Add(new Bus { Aracİd = "b1", Kapasite = 20, YakitTuru = "Benzin", FirmaAdi = "A", AracTuru = "Otobus" });
            OtobusList.Add(new Bus { Aracİd = "b2", Kapasite = 15, YakitTuru = "Benzin", FirmaAdi = "A", AracTuru = "Otobus" });
            OtobusList.Add(new Bus { Aracİd = "b3", Kapasite = 15, YakitTuru = "Motorin", FirmaAdi = "B", AracTuru = "Otobus" });
            OtobusList.Add(new Bus { Aracİd = "b4", Kapasite = 20, YakitTuru = "Motorin", FirmaAdi = "B", AracTuru = "Otobus" });
            OtobusList.Add(new Bus { Aracİd = "b5", Kapasite = 20, YakitTuru = "Motorin", FirmaAdi = "C", AracTuru = "Otobus" });


            foreach (var otobus in OtobusList)
            {
                otobus.Koltuklar = new Dictionary<int, int>();

                for (int i = 1; i <= otobus.Kapasite; i++)
                {
                    otobus.Koltuklar.Add(i, i);
                }
            }


        }


        // Override edilmiş YakitUcreti metodu
        public override void YakitUcreti(string firmaAdi)
        {
            decimal ucret;

            // Firma adına bağlı olarak ücreti belirle
            switch (firmaAdi)
            {
                case "A":
                    ucret = 10;
                    break;
                case "B":
                    ucret = 5;
                    break;
                case "C":
                    ucret = 6;
                    break;
                default:
                    ucret = 0; 
                    break;
            }

            Console.WriteLine($"Otobüs için yakıt ücreti hesaplanıyor. Firma {firmaAdi} için ücret: {ucret} TL");
        }
    }
}
