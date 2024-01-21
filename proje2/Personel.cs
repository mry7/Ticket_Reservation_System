using proje2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public class Personel : Person
    {

        public string Pozisyon { get; set; }
        public decimal Maas { get; set; }
        public string AracId { get; set; }
        public string FirmaAdi { get; set; }
        public string AracTuru { get; set; }

          public Personel(string ad, string soyad, string pozisyon, decimal maas, string aracId, string firmaAdi, string aracTuru)
          {
              Ad = ad;
              Soyad = soyad;
              Pozisyon = pozisyon;
              Maas = maas;
              AracId = aracId;
              FirmaAdi = firmaAdi;
              AracTuru = aracTuru;
          }

    

        public static List<Personel> Calisanlar { get; set; } = new List<Personel>();


        public static List<Personel> CalisanEkle()
        {
            if (Calisanlar.Count == 0)
            {


                //_______________________________________________________________________________________________________
                //A firması

                //b1 araci
                Calisanlar.Add(new Personel("Berat", "Yıldırım", "Surucu", 10000, "b1", "A", "Otobus"));
                Calisanlar.Add(new Personel("Mahmut", "Köseoğlu", "Muavin", 4000, "b1", "A", "Otobus"));

                //b2 araci
                Calisanlar.Add(new Personel("Zeki", "Çakmak", "Surucu", 10000, "b2", "A", "Otobus"));
                Calisanlar.Add(new Personel("Mansur", "Alp", "Muavin", 4000, "b2", "A", "Otobus"));




                //_______________________________________________________________________________________________________
                //B firması

                //b3 araci
                Calisanlar.Add(new Personel("Mehmet", "Önen", "Surucu", 6000, "b3", "B", "Otobus"));
                Calisanlar.Add(new Personel("Ege", "Öztürk", "Muavin", 3000, "b3", "B", "Otobus"));


                //b4 araci
                Calisanlar.Add(new Personel("Salim", "Yılmaz", "Surucu", 6000, "b4", "B", "Otobus"));
                Calisanlar.Add(new Personel("İbrahim", "Yurdakul", "Muavin", 3000, "b4", "B", "Otobus"));



                //_______________________________________________________________________________________________________
                //C firması

                //b5 araci
                Calisanlar.Add(new Personel("Mehmet", "Yavuz", "Surucu", 8000, "b5", "C", "Otobus"));
                Calisanlar.Add(new Personel("İsmail", "Yıldız", "Muavin", 4000, "b5", "C", "Otobus"));


                //a1 araci
                Calisanlar.Add(new Personel("Hikmet", "Köse", "Surucu", 20000, "a1", "C", "Ucak"));
                Calisanlar.Add(new Personel("Semih", "Acar", "Muavin", 12000, "a1", "C", "Ucak"));

                //a2 araci
                Calisanlar.Add(new Personel("Eren", "Kandaz", "Surucu", 20000, "a2", "C", "Ucak"));
                Calisanlar.Add(new Personel("Ahmet", "Kalender", "Muavin", 12000, "a2", "C", "Ucak"));




                //_______________________________________________________________________________________________________
                //D firması

                //t1 aracı
                Calisanlar.Add(new Personel("Ahmet", "Yılmaz", "Surucu", 4000, "t1", "D", "Tren"));
                Calisanlar.Add(new Personel("Mahmut", "Kozcuğlu", "Muavin", 2000, "t1", "D", "Tren"));

                //t2 aracı
                Calisanlar.Add(new Personel("Fethi", "Karaduman", "Surucu", 4000, "t2", "D", "Tren"));
                Calisanlar.Add(new Personel("Sinan", "Özkan", "Muavin", 2000, "t2", "D", "Tren"));

                //t3 aracı
                Calisanlar.Add(new Personel("Akif", "Özer", "Surucu", 4000, "t3", "D", "Tren"));
                Calisanlar.Add(new Personel("Ali", "Bilgin", "Muavin", 2000, "t3", "D", "Tren"));



                //_______________________________________________________________________________________________________
                //F firması

                //a3 araci
                Calisanlar.Add(new Personel("Abdurrahman", "Öcal", "Surucu", 15000, "a3", "F", "Ucak"));
                Calisanlar.Add(new Personel("Yusuf", "Muslu", "Muavin", 8000, "a3", "F", "Ucak"));

                //a4 araci
                Calisanlar.Add(new Personel("Halil", "Kaygusuz", "Surucu", 15000, "a4", "F", "Ucak"));
                Calisanlar.Add(new Personel("Kerem", "Erkoç", "Muavin", 8000, "a4", "F", "Ucak"));




            }

            return Calisanlar;

        }



    }
}






