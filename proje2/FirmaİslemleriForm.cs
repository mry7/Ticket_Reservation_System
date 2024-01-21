using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace proje2
{
    public partial class FirmaİslemleriForm : Form
    {

        public FirmaİslemleriForm()
        {
            InitializeComponent();


            //maliyet hesaplama için button5
            dataGridView3.Columns.Add("selectedDate", "Tarih");
            dataGridView3.Columns.Add("selectedFirma", "Firma Adı");
            dataGridView3.Columns.Add("selectedAracTuru", "Araç Türü");
            dataGridView3.Columns.Add("toplamMaliyet", "Toplam Maliyet");


            // (araç türü)  arac ekle button1
            comboBox1.Items.Add("Otobus");
            comboBox1.Items.Add("Tren");
            comboBox1.Items.Add("Ucak");

            // (yakıt türü) arac ekle button1
            comboBox2.Items.Add("Benzin");
            comboBox2.Items.Add("Motorin");
            comboBox2.Items.Add("Elektrik");
            comboBox2.Items.Add("Gaz");

            // (Araç türü)  maliyet hesaplama button5
            comboBox4.Items.Add("Otobus");
            comboBox4.Items.Add("Ucak");
            comboBox4.Items.Add("Tren");

            // Form başlatıldığında listeleme işlemini gerçekleştir
            RefreshDataGridView();


            // ComboBox3'ü doldur button3 sefer ekleme
            List<KeyValuePair<int, string>> seferlerList = new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "1. Sefer Tren: İstanbul - Kocaeli - Bilecik - Eskişehir - Ankara - Eskişehir - Bilecik - Kocaeli - İstanbul "),
                new KeyValuePair<int, string>(2, "2. Sefer Tren: İstanbul - Kocaeli - Bilecik - Eskişehir - Konya - Eskişehir - Bilecik - Kocaeli - İstanbul "),
                new KeyValuePair<int, string>(3, "3. Sefer Otobus: İstanbul - Kocaeli - Ankara - Kocaeli - İstanbul "),
                new KeyValuePair<int, string>(4, "4. Sefer Otobus: İstanbul - Kocaeli - Eskişehir - Konya - Eskişehir - Kocaeli - İstanbul "),
                new KeyValuePair<int, string>(5, "5. Sefer Ucak: İstanbul - Konya - İstanbul "),
                new KeyValuePair<int, string>(6, "6. Sefer Ucak: İstanbul - Ankara - İstanbul ")
            };

            comboBox3.DataSource = seferlerList;
            comboBox3.DisplayMember = "Value";
            comboBox3.ValueMember = "Key";


            // Form yüklendiğinde datagridview2 içindeki veriler otomatik olarak ekrana gelsin.
            string firmaAdi = Company.FirmaDogrulamasi;
            var girisYapanFirmaSeferleri = Trip.SeferBilgileri().Where(sefer => sefer.FirmaAdi == firmaAdi).ToList();
            // DataGridView'i BindingSource ile bağla
            dataGridView2.DataSource = girisYapanFirmaSeferleri;
        }

        private void FirmaİslemleriForm_Load(object sender, EventArgs e)
        {
            // Başlangıç ve bitiş tarih sınırlarını ayarladık
            DateTime baslangicTarihi = DateTime.Parse("04.12.2023");
            DateTime bitisTarihi = DateTime.Parse("10.12.2023");

            // Başlangıç tarihi için minimum ve maksimum değerleri sınırladık
            dateTimePicker1.MinDate = baslangicTarihi;
            dateTimePicker1.MaxDate = bitisTarihi;

            // Başlangıç tarihi için varsayılan değeri ayarladık
            dateTimePicker1.Value = baslangicTarihi;



            // Başlangıç tarihi için minimum ve maksimum değerleri sınırladık
            dateTimePicker2.MinDate = baslangicTarihi;
            dateTimePicker2.MaxDate = bitisTarihi;

            // Başlangıç tarihi için varsayılan değeri ayarladık
            dateTimePicker2.Value = baslangicTarihi;



        }
      //____________________________________________________________________________________________________________
      //ARAÇ EKLEME-ÇIKARMA İŞLEMİ BAŞLANGIÇ

        private void RefreshDataGridView() //arac ekleme işlemi için (button1)
        {
            // Giriş yapan firmanın adını al
            string FirmaDogrulamasi = Company.FirmaDogrulamasi;

            // Firmaya özgü otobüs, tren ve uçak listelerini al
            List<Bus> olistesi = Bus.OtobusList.FindAll(b => b.FirmaAdi == FirmaDogrulamasi);
            List<Train> tlistesi = Train.TrenList.FindAll(t => t.FirmaAdi == FirmaDogrulamasi);
            List<Airplane> ulistesi = Airplane.UcakList.FindAll(u => u.FirmaAdi == FirmaDogrulamasi);

            // Kontrol amacıyla console'a verileri yazdır
            Console.WriteLine("Otobüs Listesi:");
            foreach (var otobus in olistesi)
            {
                Console.WriteLine($"AracId: {otobus.Aracİd}, Kapasite: {otobus.Kapasite}, YakitTuru: {otobus.YakitTuru}, FirmaAdi: {otobus.FirmaAdi}, AracTuru: {otobus.AracTuru}");
            }

            Console.WriteLine("Tren Listesi:");
            foreach (var tren in tlistesi)
            {
                Console.WriteLine($"AracId: {tren.Aracİd}, Kapasite: {tren.Kapasite}, YakitTuru: {tren.YakitTuru}, FirmaAdi: {tren.FirmaAdi}, AracTuru: {tren.AracTuru}");
            }

            Console.WriteLine("Uçak Listesi:");
            foreach (var ucak in ulistesi)
            {
                Console.WriteLine($"AracId: {ucak.Aracİd}, Kapasite: {ucak.Kapasite}, YakitTuru: {ucak.YakitTuru}, FirmaAdi: {ucak.FirmaAdi}, AracTuru: {ucak.AracTuru}");
            }

            // Tüm araç listelerini birleştir
            var tumAraclar = olistesi.Cast<Vehicle>().Concat(tlistesi.Cast<Vehicle>()).Concat(ulistesi.Cast<Vehicle>()).ToList();

            // Kontrol amacıyla console'a birleştirilmiş verileri yazdır
            Console.WriteLine("Tüm Araçlar:");
            foreach (var arac in tumAraclar)
            {
                Console.WriteLine($"AracId: {arac.Aracİd}, Kapasite: {arac.Kapasite}, YakitTuru: {arac.YakitTuru}, FirmaAdi: {arac.FirmaAdi}, AracTuru: {arac.AracTuru}");
            }

                // DataGridView kontrolüne araç listesini bağla
                dataGridView1.DataSource = tumAraclar;

                dataGridView1.Columns["Aracİd"].Visible = true;
                dataGridView1.Columns["Kapasite"].Visible = true;
                dataGridView1.Columns["YakitTuru"].Visible = true;
                dataGridView1.Columns["FirmaAdi"].Visible = true;
                dataGridView1.Columns["AracTuru"].Visible = true;
                dataGridView1.Columns["Koltuklar"].Visible = false; 
            

        }

        //araç ekleme buttonu başlangıç
        private void button1_Click(object sender, EventArgs e) //arac ekleme işlemi
        {

            try
            {
                string aracId = textBox1.Text.Trim();
                int kapasite = 0;
                bool kapasiteGecerli = int.TryParse(textBox3.Text, out kapasite);
                string yakitTuru = comboBox2.SelectedItem?.ToString();
                string firmaAdi = Company.FirmaDogrulamasi; // Giriş yapan firmanın adını al
                string aracTuru = comboBox1.SelectedItem?.ToString();

                // Gerekli tüm alanlar doldurulmalı (kontrol sağla)
                if (string.IsNullOrWhiteSpace(aracId) || kapasiteGecerli == false || string.IsNullOrWhiteSpace(yakitTuru) || string.IsNullOrWhiteSpace(aracTuru))
                {
                    MessageBox.Show("Lütfen tüm bilgileri eksiksiz doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Bilgiler eksik olduğunda işlemi sonlandır
                }

                // Arac turune gore ilgili listeye ekleme yap
                bool aracVarmi = true;
                // Aynı araç ID'sine sahip bir araç zaten listede var mı kontrol et

                switch (aracTuru)
                {
                    case "Otobus":
                        if (Bus.OtobusList.Any(bus => bus.Aracİd == aracId))
                        {
                            MessageBox.Show("Bu araç ID'si zaten mevcut.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            aracVarmi = false;
                        }
                        else
                        {
                            Bus.OtobusList.Add(new Bus { Aracİd = aracId, Kapasite = kapasite, YakitTuru = yakitTuru, FirmaAdi = firmaAdi, AracTuru = aracTuru });
                        }
                        break;

                    case "Tren":
                        if (Train.TrenList.Any(train => train.Aracİd == aracId))
                        {
                            MessageBox.Show("Bu araç ID'si zaten mevcut.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            aracVarmi = false;
                        }
                        else
                        {
                            Train.TrenList.Add(new Train { Aracİd = aracId, Kapasite = kapasite, YakitTuru = yakitTuru, FirmaAdi = firmaAdi, AracTuru = aracTuru });
                        }
                        break;

                    case "Ucak":
                        if (Airplane.UcakList.Any(airplane => airplane.Aracİd == aracId))
                        {
                            MessageBox.Show("Bu araç ID'si zaten mevcut.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            aracVarmi = false;
                        }
                        else
                        {
                            Airplane.UcakList.Add(new Airplane { Aracİd = aracId, Kapasite = kapasite, YakitTuru = yakitTuru, FirmaAdi = firmaAdi, AracTuru = aracTuru });
                        }
                        break;

                    default:
                        MessageBox.Show("Geçersiz araç türü seçimi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        aracVarmi = false;
                        break;
                }

                // DataGridView kontrolünü güncelle başarılı bir şekilde araç eklenmişse
                if (aracVarmi)
                {
                    MessageBox.Show("Araç başarıyla eklendi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshDataGridView();
                    textBox1.Clear();
                    textBox3.Clear();



                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Format hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//araç ekleme buttonu bitiş


        //araç silem buttonu başlangıç
        private void button2_Click(object sender, EventArgs e) //Araç çıkarma işlemi
        {
            try
            {
                // Seçilen aracı al
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                    string aracId = selectedRow.Cells["Aracİd"].Value.ToString();
                    string aracTuru = selectedRow.Cells["AracTuru"].Value.ToString();

                    // Arac turune gore ilgili listeden silme yap
                    bool aracsilinsin = true;

                    switch (aracTuru)
                    {
                        case "Otobus":
                            Bus.OtobusList.RemoveAll(bus => bus.Aracİd == aracId);
                            break;

                        case "Tren":
                            Train.TrenList.RemoveAll(train => train.Aracİd == aracId);
                            break;

                        case "Ucak":
                            Airplane.UcakList.RemoveAll(airplane => airplane.Aracİd == aracId);
                            break;

                        default:
                            MessageBox.Show("Geçersiz araç türü seçimi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            aracsilinsin = false;
                            break;
                    }

                    // DataGridView kontrolünü güncelle  araç silme işlemi başarılıysa
                    if (aracsilinsin)
                    {
                        MessageBox.Show("Araç başarıyla silindi.", "Başarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataGridView();
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz aracı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } // araç silme buttonu bitiş

        //ARAÇ EKLEME-ÇIKARMA İŞLEMİ BİTİŞ


        //  ______________________________________________________________________________________________________________________


        //SEFER EKLEME - ÇIKARMA İŞLEMİ BAŞLANGIÇ

        //sefer ekleme işlemi buttonu başlangıç
        private void button3_Click_1(object sender, EventArgs e)//sefer ekleme
        {
            // Kullanıcı tarafından seçilen seferin ID'sini al
            int seferSecimi = ((KeyValuePair<int, string>)comboBox3.SelectedItem).Key;
            DateTime kullanicininSectigiTarih = dateTimePicker1.Value;
            string aracTuru = textBox8.Text;
            string aracId = textBox9.Text;
            double fiyat;

            //fiyat geçerli şekilde girilmediyse hata döndür
            if (!double.TryParse(textBox2.Text, out fiyat))
            {
                MessageBox.Show("Geçerli bir fiyat giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Giriş yapan firmanın adını al
            string firmaAdi = Company.FirmaDogrulamasi;
            // Aynı araç ID'sine sahip bir araç zaten listede var mı kontrol et
            //varsa ekleme yap yoksa ekleme yapma
            bool aracVarmi = false;

            if (aracTuru == "Otobus")
            {
                aracVarmi = Bus.OtobusList.Any(bus => bus.Aracİd == aracId && bus.FirmaAdi == firmaAdi);
            }
            else if (aracTuru == "Tren")
            {
                aracVarmi = Train.TrenList.Any(train => train.Aracİd == aracId && train.FirmaAdi == firmaAdi);
            }
            else if (aracTuru == "Ucak")
            {
                aracVarmi = Airplane.UcakList.Any(airplane => airplane.Aracİd == aracId && airplane.FirmaAdi == firmaAdi);
            }
            else
            {
                MessageBox.Show("Geçersiz araç türü seçimi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!aracVarmi)
            {
                MessageBox.Show("Bu araç ID'si mevcut değil!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Yeni bir Trip öğesi oluştur
                Trip seferEkle = new Trip
                {
                    SeferId = seferSecimi,
                    FirmaAdi = firmaAdi,
                    AracTuru = aracTuru,
                    AracId = aracId,
                    Fiyat = fiyat,
                    Tarih = kullanicininSectigiTarih
                };

                // Seferleri güncelle
                Trip.Seferler.Add(seferEkle);


                // Sadece giriş yapan firmanın verilerini göster
                var girisYapanFirmaSeferleri = Trip.Seferler.Where(sefer => sefer.FirmaAdi == firmaAdi).ToList();

                // DataGridView'e DataSource'u güncelle
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = girisYapanFirmaSeferleri;
            }
        }  //sefer ekleme işlemi buttonu bitiş


        //sefer çıkarma işlemi buttonu başlangıç
        private void button4_Click(object sender, EventArgs e)//sefer çıkarma işlemi
        {
            // DataGridView'de seçili satırı kontrol et
            if (dataGridView2.SelectedRows.Count > 0)
            {
                // Seçili satırın indeksini al
                int secilenSatir = dataGridView2.SelectedRows[0].Index;

                // Seçili satırın Trip öğesini al (tüm satırın verisi)
                Trip seferSil = (Trip)dataGridView2.Rows[secilenSatir].DataBoundItem;

                // Seçili seferi Trip.Seferler listesinden kaldır
                Trip.Seferler.Remove(seferSil);

                // Giriş yapan firmanın adını al
                string firmaAdi = Company.FirmaDogrulamasi;

                // Sadece giriş yapan firmanın verilerini göster
                var girisYapanFirmaSeferleri = Trip.Seferler.Where(sefer => sefer.FirmaAdi == firmaAdi).ToList();

                // DataGridView'e DataSource'u güncelle
                dataGridView2.DataSource = null;
                dataGridView2.DataSource = girisYapanFirmaSeferleri;
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir sefer seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        } //sefer çıkarma işlemi buttonu başlangıç


        //SEFER EKLEME - ÇIKARMA İŞLEMİ BİTİŞ

        //__________________________________________________________________________________________

        //ZARAR HESAPLAMA İŞLEMİ BAŞLANGIÇ

        //zarar maliyet hesaplama işlemi
        private void button5_Click_1(object sender, EventArgs e)
        {
            DateTime secilenTarih = dateTimePicker2.Value.Date;

            // Giriş yapan firmanın adını al
            string girisYapanFirma = Company.FirmaDogrulamasi;

            string secilenAracTuru = comboBox4.SelectedItem?.ToString(); 

            string secilenAracId = textBox4.Text;

            //kullanıcının seçtiği tarihe,arac türüne , arac idsine ve giriş yapan firmaya bağlı bir hesaplama yapılıyor


            // Company sınıfından bir örnek oluşturduk
            Company companyOrnek = new Company("kullaniciAdi", "sifre");

            // Daha sonra bu örneği kullanarak ZararHesapla metodunu çağırdık
            decimal toplamMaliyet = companyOrnek.ZararHesapla(secilenTarih, girisYapanFirma, secilenAracTuru, secilenAracId);


            dataGridView3.Rows.Add(secilenTarih, girisYapanFirma, secilenAracTuru, toplamMaliyet);
        }
        //ZARAR HESAPLAMA İŞLEMİ BİTİŞ




    }
}







