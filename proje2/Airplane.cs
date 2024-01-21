using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public class Airplane : Vehicle
    {

        public static List<Airplane> UcakList { get; set; } = new List<Airplane>();

        public static void UcaklistesineEkle()
        {
            UcakList.Add(new Airplane { Aracİd = "a1", Kapasite = 30, YakitTuru = "Gaz", FirmaAdi = "C", AracTuru = "Ucak" });
            UcakList.Add(new Airplane { Aracİd = "a2", Kapasite = 30, YakitTuru = "Gaz", FirmaAdi = "C", AracTuru = "Ucak" });
            UcakList.Add(new Airplane { Aracİd = "a3", Kapasite = 30, YakitTuru = "Gaz", FirmaAdi = "F", AracTuru = "Ucak" });
            UcakList.Add(new Airplane { Aracİd = "a4", Kapasite = 30, YakitTuru = "Gaz", FirmaAdi = "F", AracTuru = "Ucak" });

            foreach (var ucak in UcakList)
            {
                ucak.Koltuklar = new Dictionary<int, int>();

                for (int i = 1; i <= ucak.Kapasite; i++)
                {
                    ucak.Koltuklar.Add(i, i);
                }
            }

        }



        public override void YakitUcreti(string firmaAdi)
        {
            decimal ucret;

            // Firma adına bağlı olarak ücreti belirle
            switch (firmaAdi)
            {
                case "C":
                    ucret = 25;
                    break;
                case "F":
                    ucret = 20;
                    break;
                default:
                    ucret = 0; 
                    break;
            }

            Console.WriteLine($"Uçak için yakıt ücreti hesaplanıyor. Firma {firmaAdi} için ücret: {ucret} TL");
        }
    }

}
