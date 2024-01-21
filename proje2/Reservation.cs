using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{

    public class Reservation
    {
        public string Aracİd { get; set; }
        public int KoltukId { get; set; }
        public string Tc { get; set; }
        public bool KoltukDurumu { get; set; }


           public Reservation(string aracİd, int koltukId, string tc, bool koltukDurumu)
           {
               Aracİd = aracİd;
               KoltukId = koltukId;
               Tc = tc;
               KoltukDurumu = koltukDurumu;
           }
        


        public static List<Reservation> Rezervasyon = new List<Reservation>();

        public static void KoltukRezervasyonlari()
        {
            Rezervasyon.Add(new Reservation("b4", 8, "12345678901", true ));
            Rezervasyon.Add(new Reservation("b4", 5,  "23456789012", true));



        }
    }

    }

