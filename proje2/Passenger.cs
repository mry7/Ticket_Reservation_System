using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{
    public class Passenger : Person
    {
        public DateTime DogumTarihi { get; set; }
        public string Tc { get; set; }

        // Yolcu bilgilerini içeren bir List koleksiyonu
        public static List<Passenger> Yolcular = new List<Passenger>();

        static Passenger()
        {
            YolcuBilgileri();
        }

        public static void YolcuBilgileri()
        {
            Yolcular.Add(new Passenger { Ad = "Ahmet", Soyad = "Yılmaz", DogumTarihi = new DateTime(1985, 3, 10), Tc = "12345678901" });
            Yolcular.Add(new Passenger { Ad = "Mehmet", Soyad = "Demir", DogumTarihi = new DateTime(1990, 5, 15), Tc = "23456789012" });
            Yolcular.Add(new Passenger { Ad = "Ayşe", Soyad = "Çelik", DogumTarihi = new DateTime(1988, 7, 20), Tc = "34567890123" });
            Yolcular.Add(new Passenger { Ad = "Fatma", Soyad = "Kaya", DogumTarihi = new DateTime(1995, 9, 5), Tc = "45678901234" });
            Yolcular.Add(new Passenger { Ad = "Hakan", Soyad = "Aksoy", DogumTarihi = new DateTime(1982, 11, 8), Tc = "56789012345" });
            Yolcular.Add(new Passenger { Ad = "Zeynep", Soyad = "Yıldırım", DogumTarihi = new DateTime(1998, 2, 18), Tc = "67890123456" });
            Yolcular.Add(new Passenger { Ad = "Onur", Soyad = "Turan", DogumTarihi = new DateTime(1987, 6, 25), Tc = "78901234567" });
            Yolcular.Add(new Passenger { Ad = "Esra", Soyad = "Kurt", DogumTarihi = new DateTime(1993, 8, 30), Tc = "89012345678" });
            Yolcular.Add(new Passenger { Ad = "Sevil", Soyad = "Güler", DogumTarihi = new DateTime(1996, 4, 12), Tc = "90123456789" });
            Yolcular.Add(new Passenger { Ad = "Burak", Soyad = "Aydın", DogumTarihi = new DateTime(1984, 10, 3), Tc = "01234567890" });
        }

    }
}