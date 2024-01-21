using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace proje2
{
    public partial class AdminİslemleriForm : Form
    {
        private List<Company.firmabilgi> firmalarListesi;

        public AdminİslemleriForm()
        {
            InitializeComponent();

            // Form yüklendiğinde firmaları yükle
            firmalarListesi = Company.firmalar;
            UpdateDataGridView();
        }


        //FİRMA EKLEME
        private void button1_Click(object sender, EventArgs e)
        {
            string firmaAdi = textBox1.Text;
            string firmaSifre = textBox2.Text;
            string aracTuru = textBox3.Text;
            decimal hizmetBedeli;

            // Kontrol: Tüm alanlar doldurulmuş mu?
            if (string.IsNullOrWhiteSpace(firmaAdi) || string.IsNullOrWhiteSpace(firmaSifre) || string.IsNullOrWhiteSpace(aracTuru) || !decimal.TryParse(textBox4.Text, out hizmetBedeli))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Hata durumunda işlemi sonlandır
            }

            // Kontrol: Firma adı zaten listede var mı?
            if (firmalarListesi.Any(firma => firma.KullaniciAdi == firmaAdi))
            {
                MessageBox.Show("Hata bu firma zaten var!"); return; // firma varsa işlemi sonlandır
            }

            // Yeni bir firmabilgi oluştur (company class içinde class oluştur)
            Company.firmabilgi firmaEkle = new Company.firmabilgi
            {
                KullaniciAdi = firmaAdi,
                Sifre = firmaSifre,
                AracTurleri = new List<string> { aracTuru },
                HizmetBedeli = hizmetBedeli
            };

            // Company sınıfındaki firmalar listesine yeni firmayı ekle
            firmalarListesi.Add(firmaEkle);

            // DataGridView'i güncelle
            UpdateDataGridView();

            MessageBox.Show("Firma başarıyla eklendi!");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }


        //FİRMA ÇIKARMA
        private void button2_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrol et
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili satırın indeksini al
                int secilenSatir = dataGridView1.SelectedRows[0].Index;

                // Seçili firma bilgisini al
                Company.firmabilgi secilenFirma = firmalarListesi[secilenSatir];

                // Onay dialogu (emin misniz)
                DialogResult sonuc = MessageBox.Show($"'{secilenFirma.KullaniciAdi}' firmasını silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Silme işlemi onaylandıysa
                if (sonuc == DialogResult.Yes)
                {
                    // Firma listesinden sil
                    firmalarListesi.RemoveAt(secilenSatir);

                    // DataGridView'i güncelle
                    UpdateDataGridView();

                    MessageBox.Show("Firma başarıyla silindi!");
                }
            }
            else
            {
                // Seçili satır yoksa hata mesajı göster
                MessageBox.Show("Lütfen bir firma seçiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateDataGridView()
        {
            // DataGridView'i temizle
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear(); // Sütunları temizle

            // Sütunları ekle
            dataGridView1.Columns.Add("FirmaAdiColumn", "Firma Adı");
            dataGridView1.Columns.Add("SifreColumn", "Şifre");
            dataGridView1.Columns.Add("AracTuruColumn", "Araç Türü");
            dataGridView1.Columns.Add("HizmetBedeliColumn", "Hizmet Bedeli");



            // Firmalar listesini DataGridView'e ekle
            foreach (var firma in firmalarListesi)
            {
                dataGridView1.Rows.Add(firma.KullaniciAdi, firma.Sifre, string.Join(", ", firma.AracTurleri), firma.HizmetBedeli);
            }
        }
    }
}
