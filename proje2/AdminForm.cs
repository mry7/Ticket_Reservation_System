using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proje2
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            // Admin sınıfından bir nesne oluşturuluyor.
            //constructer çağrılıyor
             Admin admin = new Admin("kullaniciAdi", "sifre"); 

            // Kullanıcı adı ve şifre doğrulama işlemleri Admin sınıfında gerçekleştiriliyor.
            if (admin.KullaniciGirisi(kullaniciAdi, sifre))
            {
                // Doğrulama başarılıysa AdminIslemleriForm'a yönlendir
                AdminİslemleriForm adminIslemleriForm = new AdminİslemleriForm();
                adminIslemleriForm.Show(); // AdminIslemleriForm'u görüntüle
                this.Hide(); //Mevcut AdminForm'u gizle

                }
          
        }

    }
}