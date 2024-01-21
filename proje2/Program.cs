using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje2
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Train sınıfı için örnek AracTuru özelliği
            Train train = new Train();
            train.AracTuru = "Tren";

            // Bus sınıfı için örnek AracTuru özelliği
            Bus bus = new Bus();
            bus.AracTuru = "Otobus";

            // Airplane sınıfı için örnek AracTuru özelliği
            Airplane airplane = new Airplane();
            airplane.AracTuru = "Ucak";


            // Başlangıçta araç listelerine örnek veriler eklemek için kullanılır
            Bus.OtobuslistesineEkle();
            Train.TrenlistesineEkle();
            Airplane.UcaklistesineEkle();


            Reservation.KoltukRezervasyonlari();


            // Yolcuları ekleyen metotları çağır
            Passenger.YolcuBilgileri();

          


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
