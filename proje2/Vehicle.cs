using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{

    public abstract class Vehicle 
    {
        public string Aracİd { get; set; }
        public int Kapasite { get; set; }
        public String YakitTuru { get; set; }
        public string FirmaAdi { get; set; }
        public string AracTuru { get; set; }

        public Dictionary<int, int> Koltuklar { get; set; }


        public abstract void YakitUcreti(string firmAdi); // Abstract metot


    }
}
