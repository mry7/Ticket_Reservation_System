using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace proje2
{
    public class Admin : User
    {

        public Admin(string kullaniciAdi, string sifre)
        {
            KullaniciAdi = kullaniciAdi;
            Sifre = sifre;
        }
        public override void Giris()
        {
            Console.WriteLine($"Admin olarak {KullaniciAdi} giriş yaptı.");
            // kullanici dogrulanmasi islemi:
            MessageBox.Show("Giriş başarılı!");
        }

        public bool KullaniciGirisi(string kullaniciAdi, string sifre)
        {
           
            bool sonuc = kullaniciAdi == "a" && sifre == "1";

            if (!sonuc)
            {
                // giris saglanamazsa
                MessageBox.Show("Hatalı kullanıcı adı veya şifre!");
            }
            else
            {
                Giris(); // Başarılı giriş durumunda Login metodunu çağır.
            }
            return sonuc;
        }
    }
}
