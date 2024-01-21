using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje2
{

    public class User
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }


        public virtual void Giris()
        {
            Console.WriteLine("Kullanıcı olarak giriş yaptınız.");
        }
    }
}
