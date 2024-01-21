using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public class Train : Vehicle
    {
        public static List<Train> TrenList { get; set; } = new List<Train>();

        public static void TrenlistesineEkle()
        {
            TrenList.Add(new Train { Aracİd = "t1", Kapasite = 25, YakitTuru = "Elektrik", FirmaAdi = "D", AracTuru = "Tren" });
            TrenList.Add(new Train { Aracİd = "t2", Kapasite = 25, YakitTuru = "Elektrik", FirmaAdi = "D", AracTuru = "Tren" });
            TrenList.Add(new Train { Aracİd = "t3", Kapasite = 25, YakitTuru = "Elektrik", FirmaAdi = "D", AracTuru = "Tren" });

       


            foreach (var Tren in TrenList)
            {
                Tren.Koltuklar = new Dictionary<int, int>();

                for (int i = 1; i <= Tren.Kapasite; i++)
                {
                    Tren.Koltuklar.Add(i, i);
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
                case "D":
                    ucret = 3;
                    break;
                default:
                    ucret = 0; 
                    break;
            }

            Console.WriteLine($"Tren için yakıt ücreti hesaplanıyor. Firma {firmaAdi} için ücret: {ucret} TL");
        }

    }
}
