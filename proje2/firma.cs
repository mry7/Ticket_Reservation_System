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
    public partial class firma : Form
    {
        public firma()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            // Company sınıfından bir nesne oluşturuluyor.
            Company company = new Company(kullaniciAdi, sifre);

            // Kullanıcı adı ve şifre doğrulama işlemleri Company sınıfında gerçekleştiriliyor.
            if (company.KullaniciGirisi(kullaniciAdi, sifre))
            {
                // Doğrulama başarılıysa FirmaIslemleriForm'a yönlendir
                FirmaİslemleriForm firmaIslemleriForm = new FirmaİslemleriForm();
                firmaIslemleriForm.Show(); // FirmaIslemleriForm'u görüntüle
                this.Hide(); // Mevcut FirmaForm'u gizle
            }
        }
    }
}

