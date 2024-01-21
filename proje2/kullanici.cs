using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace proje2
{
    public partial class kullanici : Form
    {

        public kullanici()
        {
            InitializeComponent();

            KoltukOlustur(Bus.OtobusList.Cast<Vehicle>().ToList(), tabPage2);
            KoltukOlustur(Airplane.UcakList.Cast<Vehicle>().ToList(), tabPage2);
            KoltukOlustur(Train.TrenList.Cast<Vehicle>().ToList(), tabPage2);

        }

        private void kullanici_Load(object sender, EventArgs e)
        {
            // comboBox1 ve comboBox2'yi temizle
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            //  Route classta bulunan GuzergahBilgileri listesindeki şehirleri  comboBox1 ve comboBox2'ye ekledik
            foreach (Route sfr in Route.GuzergahBilgileri)
            {
                foreach (string sehir in sfr.Sehirler)
                {
                    if (!comboBox1.Items.Contains(sehir))
                    {
                        comboBox1.Items.Add(sehir);
                    }
                    if (!comboBox2.Items.Contains(sehir))
                    {
                        comboBox2.Items.Add(sehir);
                    }
                }
            }

            // Başlangıç ve bitiş tarih sınırlarını ayarladık
            DateTime baslangicTarihi = DateTime.Parse("04.12.2023");
            DateTime bitisTarihi = DateTime.Parse("10.12.2023");

            // Başlangıç tarihi için minimum ve maksimum değerleri sınırladık
            dateTimePicker1.MinDate = baslangicTarihi;
            dateTimePicker1.MaxDate = bitisTarihi;

            // Başlangıç tarihi için varsayılan değeri ayarladık
            dateTimePicker1.Value = baslangicTarihi;



            // DataGridView sütunlarını doldurduk
            dataGridView1.Columns.Add("FirmaAdi", "Firma Adı");
            dataGridView1.Columns.Add("AracTuru", "Araç Türü");
            dataGridView1.Columns.Add("AracId", "Araç ID");
            dataGridView1.Columns.Add("SeferId", "Sefer ID");
            dataGridView1.Columns.Add("Fiyat", "Fiyat");
            dataGridView1.Columns.Add("Tarih", "Tarih");


        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcının seçtiği kalkış ve varış şehirleri
            string kalkisYeri = comboBox1.SelectedItem?.ToString();
            string varisYeri = comboBox2.SelectedItem?.ToString();

            // Kalkış ve varış şehirlerini seçip seçmediğini kontrol et. Eğer seçimediyse uyarı göster
            if (string.IsNullOrEmpty(kalkisYeri) || string.IsNullOrEmpty(varisYeri))
            {
                MessageBox.Show("Lütfen kalkış ve varış şehirlerini seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Seçilen tarih
            DateTime kullanicininSectigiTarih = dateTimePicker1.Value;

            // Kullanıcının seçtiği kalkış ve varış şehirlerine uygun SeferId'leri bul
            List<int> eslesenRota = Route.GuzergahBilgileri
                .Where(sfr => sfr.Sehirler.Contains(kalkisYeri) && sfr.Sehirler.Contains(varisYeri))
                .Select(sfr => sfr.SeferId)
                .ToList();

            // DataGridView'i temizle
            dataGridView1.Rows.Clear();

            // Trip classta bulunan SeferBilgileri methodundan uygun SeferId'lere sahip ve seçilen tarih aralığındaki seferleri bul
            List<Trip> eslesenSeferBilgileri = Trip.SeferBilgileri()
                .Where(sfr => eslesenRota.Contains(sfr.SeferId) && sfr.Tarih.Date == kullanicininSectigiTarih.Date)
                .ToList();

            // DataGridView'e uygun sefer bilgilerini ekle
            foreach (Trip sfr in eslesenSeferBilgileri)
            {
                dataGridView1.Rows.Add(sfr.FirmaAdi, sfr.AracTuru, sfr.AracId, sfr.SeferId, sfr.Fiyat, sfr.Tarih);
            }

            // Eğer uygun sefer bulunamadıysa MessageBox ile bilgi ver
            if (eslesenSeferBilgileri.Count == 0)
            {
                MessageBox.Show("Aradığınız kriterlere göre uygun bir sefer bulunamamıştır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //________________________________________________________________________________________________________________

        private string girilenAracId;

        private void button2_Click(object sender, EventArgs e)
        {
            // Kullanıcının girdiği AracTuru ve AracId
            string girilenAracTuru = textBox1.Text;
            girilenAracId = textBox2.Text;

            // Girdi değerlerinin kontrolü
            if (string.IsNullOrEmpty(girilenAracTuru) || string.IsNullOrEmpty(girilenAracId))
            {
                MessageBox.Show("Lütfen AracTuru ve AracId bilgilerini girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // AracTuru ve AracId ile karşılaştırma yap
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string aracTuru = row.Cells["AracTuru"].Value?.ToString();
                string aracId = row.Cells["AracId"].Value?.ToString();

                if (aracTuru == girilenAracTuru && aracId == girilenAracId)
                {
                    // Eşleşme bulunduğunda işlemleri gerçekleştir
                    Console.WriteLine($"AracTuru: {aracTuru}, AracId: {aracId}");

                    // 2. sekmeye yönlendir.
                    tabControl1.SelectedTab = tabPage2;

                    // tabPage2'yi güncelle
                    KoltukOlustur(FiltrelenenAracListesi(aracTuru, aracId), tabPage2);


                    return; // Eşleşme bulunduğunda döngüden çık
                }
            }

            // Eğer eşleşme bulunamadıysa uyarı göster
            MessageBox.Show("Girilen AracTuru ve AracId'ye uygun bir sefer bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }



        // AracTuru ve AracId'ye göre filtrelenmiş araç listesini döndüren fonksiyon
        private List<Vehicle> FiltrelenenAracListesi(string aracTuru, string aracId)
        {
            var listeFiltrele = new List<Vehicle>();

            // Bus, Airplane ve Train listelerini birleştir
            var tumAraclar = Bus.OtobusList.Cast<Vehicle>().Concat(Airplane.UcakList.Cast<Vehicle>()).Concat(Train.TrenList.Cast<Vehicle>());

            // Filtreleme işlemi
            if (!string.IsNullOrEmpty(aracTuru) && !string.IsNullOrEmpty(aracId))
            {
                listeFiltrele = tumAraclar.Where(vehicle => vehicle.AracTuru == aracTuru && vehicle.Aracİd == aracId).ToList();
            }
            else
            {
                // Eğer filtreleme için gerekli kriterler sağlanmıyorsa, tüm araçları göster
                listeFiltrele = tumAraclar.ToList();
            }

            return listeFiltrele;
        }





        // ____________________________________________________________________________________________________



        private List<Button> secilenButonlar = new List<Button>(); // Kullanıcının seçtiği butonları saklamak için liste

        private void KoltukOlustur(List<Vehicle> vehicleList, Panel tabPaneli)
        {
            // Önce var olan kontrolleri temizle
            tabPaneli.Controls.Clear();

            var KoltukButtonlari = new GroupBox();
            KoltukButtonlari.Dock = DockStyle.Top;

            var KoltukPaneli = new FlowLayoutPanel();
            KoltukPaneli.AutoScroll = true;
            KoltukPaneli.Dock = DockStyle.Top;
            KoltukPaneli.FlowDirection = FlowDirection.TopDown;
            KoltukPaneli.WrapContents = true;
            KoltukButtonlari.Controls.Add(KoltukPaneli);

            tabPaneli.BackColor = Color.LightBlue;


            int buttonTop = 20;

            foreach (var vehicle in vehicleList)
            {
                if (vehicle != null && vehicle.Koltuklar != null)
                {
                    foreach (var AnahtarDeger in vehicle.Koltuklar)
                    {
                        var button = new Button();
                        button.Text = AnahtarDeger.Key.ToString(); // Koltuk numarasını yazdır
                        button.Tag = AnahtarDeger.Key.ToString(); // Koltuk ID'sini Tag olarak ayarla

                        // Kontrol et: Bu koltuk rezerve edilmiş mi?
                        bool rezerveEdilmis = Reservation.Rezervasyon.Any(rrezervasyonn => rrezervasyonn.Aracİd == vehicle.Aracİd && rrezervasyonn.KoltukId == AnahtarDeger.Key);



                        // Eğer rezerve edilmişse, koltuğu işaretle ve etkisizleştir
                        if (rezerveEdilmis)
                        {
                            button.BackColor = Color.Red;
                            button.Enabled = false;
                        }
                        else
                        {
                            button.BackColor = SystemColors.Control;
                            button.Click += (sender, e) => { KoltukSecildi(button); }; // Koltuk seçildiğinde rengini değiştir
                        }

                        KoltukPaneli.Controls.Add(button);
                        buttonTop += button.Height + 10;
                    }
                }
            }


            // bir buton oluştur
            var solAltButon = new Button();
            solAltButon.Text = "Bilet Al";
            solAltButon.Size = new Size(139, 40);
            solAltButon.Location = new System.Drawing.Point(350, 250);
            solAltButon.Margin = new Padding(2);
            solAltButon.BackColor = Color.Pink;
            solAltButon.UseVisualStyleBackColor = true;

            // TextBox oluştur
            var kisiSayisi = new TextBox();
            kisiSayisi.Name = "kisiSayisi";
            kisiSayisi.Size = new Size(100, 20);
            kisiSayisi.Location = new System.Drawing.Point(20, 200);
            kisiSayisi.Enabled = true;

            // Label oluştur
            var label = new Label();
            label.Text = "Kişi Sayısı:";
            label.Location = new System.Drawing.Point(20, 175);

            solAltButon.Click += (sender, e) => SolAltButon_Click(kisiSayisi);

            // tabPaneli'ne kontrolleri ekle
            tabPaneli.Controls.Add(label);
            tabPaneli.Controls.Add(kisiSayisi);
            tabPaneli.Controls.Add(solAltButon);
            tabPaneli.Controls.Add(KoltukButtonlari);
            tabPaneli.AutoScroll = true;
        }


        private void KoltukSecildi(Button koltukButton)
        {
            if (!secilenButonlar.Contains(koltukButton))
            {
                koltukButton.BackColor = Color.Red;
                secilenButonlar.Add(koltukButton);
            }
            else
            {
                koltukButton.BackColor = SystemColors.Control; // Eğer tekrar tıklanırsa rengi varsayılan rengine dön
                secilenButonlar.Remove(koltukButton);
            }
        }



        private List<int> secilenKoltukIdList = new List<int>();

        //---------------------------------------------------------------------------------

        private void SolAltButon_Click(TextBox kisiSayisi)
        {
            // Kullanıcının girdiği sayıyı al
            if (int.TryParse(kisiSayisi.Text, out int kisiSayisiValue))
            {
                if (kisiSayisiValue == secilenButonlar.Count)
                {
                    foreach (var secilenKoltuk in secilenButonlar)
                    {
                        Console.WriteLine($"Seçilen Koltuk: {secilenKoltuk.Text}");

                        if (secilenKoltuk.Tag != null && int.TryParse(secilenKoltuk.Tag.ToString(), out int koltukId))
                        {
                            secilenKoltukIdList.Add(koltukId); // secilenKoltukId'yi List<int> olarak güncelle

                            // Kontrol et: Bu koltuk rezerve edilmiş mi?
                            if (Reservation.Rezervasyon.Any(rrezervasyonn => rrezervasyonn.Aracİd == girilenAracId && rrezervasyonn.KoltukId == koltukId))
                            {
                                // Eğer rezerve edilmişse, koltuğu işaretle ve etkisizleştir
                                secilenKoltuk.BackColor = Color.Red;
                                secilenKoltuk.Enabled = false;
                            }
                            else
                            {
                                // Eğer rezerve edilmemişse, koltuğu işaretle
                                secilenKoltuk.BackColor = Color.Green;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Seçilen koltuk için geçerli bir KoltukId bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // İşlem başarılıysa tabPage4'ye yönlendir
                    tabControl1.SelectedTab = tabPage4;

                    // YolcuBilgileriAl metodunu çağırırken girilenAracId değeri ve yolcu sayısını parametre olarak ver
                    // Yolcu bilgilerini al
                    YolcuBilgileriAl(kisiSayisiValue, secilenKoltukIdList, girilenAracId);
                }
                else
                {
                    MessageBox.Show("Lütfen doğru sayıda koltuk seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir sayı giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private List<Passenger> Yolcular = new List<Passenger>(); // Yolcu bilgilerini saklamak için liste

        private bool bilgilerYazdirildi = false;

        private void YolcuBilgileriAl(int kisiSayisi, List<int> secilenKoltukIdList, string girilenAracId)
        {

            // Bilgiler zaten yazdırıldıysa fonksiyonu bitir
            if (bilgilerYazdirildi)
            {
                Console.WriteLine("Bilgiler zaten yazdırıldı. Yeni form açılamaz.");
                return;
            }

            for (int i = 1; i <= kisiSayisi; i++)
            {
                // Yolcu bilgi girişi için form oluştur
                Form yolcuForm = new Form();
                yolcuForm.Text = $"Yolcu {i} Bilgisi";
                yolcuForm.Size = new Size(300, 200);

                Label adLabel = new Label();
                adLabel.Text = "Adı:";
                adLabel.Location = new Point(20, 20);

                TextBox adTextBox = new TextBox();
                adTextBox.Size = new Size(150, 20);
                adTextBox.Location = new Point(120, 20);

                Label soyadLabel = new Label();
                soyadLabel.Text = "Soyadı:";
                soyadLabel.Location = new Point(20, 50);

                TextBox soyadTextBox = new TextBox();
                soyadTextBox.Size = new Size(150, 20);
                soyadTextBox.Location = new Point(120, 50);

                Label tcLabel = new Label();
                tcLabel.Text = "T.C. Kimlik No:";
                tcLabel.Location = new Point(20, 80);

                TextBox tcTextBox = new TextBox();
                tcTextBox.Size = new Size(150, 20);
                tcTextBox.Location = new Point(120, 80);

                Label dogumTarihiLabel = new Label();
                dogumTarihiLabel.Text = "Doğum Tarihi (yyyy-MM-dd):";
                dogumTarihiLabel.Location = new Point(20, 110);

                TextBox dogumTarihiTextBox = new TextBox();
                dogumTarihiTextBox.Size = new Size(150, 20);
                dogumTarihiTextBox.Location = new Point(180, 110);

                Button kaydetButton = new Button();
                kaydetButton.Text = "Kaydet";
                kaydetButton.Size = new Size(80, 30);
                kaydetButton.Location = new Point(120, 140);

                kaydetButton.Click += (sender, e) =>
                {
                    // Doğum tarihini ayrıştır
                    if (DateTime.TryParse(dogumTarihiTextBox.Text, out DateTime dogumTarihi))
                    {
                        // Kontrol et: Aynı T.C. kimlik numarasına sahip yolcu zaten var mı?
                        if (Passenger.Yolcular.Any(yollcu => yollcu.Tc == tcTextBox.Text))
                        {
                            MessageBox.Show("Bu T.C. kimlik numarasına sahip bir yolcu zaten kayıtlı. Bilgiler kaydedilmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {

                            // Yeni yolcu oluştur ve listeye ekle
                            Passenger.Yolcular.Add(new Passenger { Ad = adTextBox.Text, Soyad = soyadTextBox.Text, DogumTarihi = dogumTarihi, Tc = tcTextBox.Text });
                            Yolcular.Add(new Passenger { Ad = adTextBox.Text, Soyad = soyadTextBox.Text, DogumTarihi = dogumTarihi, Tc = tcTextBox.Text });

                            yolcuForm.Close(); // Formu kapat
                        }
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz doğum tarihi formatı. Bilgiler kaydedilmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                // Form kapanma olayını kontrol et
                yolcuForm.FormClosing += (sender, e) =>
                {
                    // Kullanıcı formu kapatmaya çalışıyorsa, eğer bilgileri henüz kaydetmemişse uyarı ver
                    if (!Yolcular.Any(yollcu => yollcu.Ad == adTextBox.Text && yollcu.Soyad == soyadTextBox.Text))
                    {
                        DialogResult result = MessageBox.Show("Henüz bilgileri kaydetmediniz. Formu kapatmak istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.No)
                        {
                            e.Cancel = true; // Formun kapanmasını iptal et
                        }
                    }
                };

                yolcuForm.Controls.Add(adLabel);
                yolcuForm.Controls.Add(adTextBox);
                yolcuForm.Controls.Add(soyadLabel);
                yolcuForm.Controls.Add(soyadTextBox);
                yolcuForm.Controls.Add(tcLabel);
                yolcuForm.Controls.Add(tcTextBox);
                yolcuForm.Controls.Add(dogumTarihiLabel);
                yolcuForm.Controls.Add(dogumTarihiTextBox);
                yolcuForm.Controls.Add(kaydetButton);

                yolcuForm.ShowDialog();
            }

            // Tüm bilgiler kaydedildikten sonra yazdır
            foreach (var yolcu in Yolcular)
            {
                MessageBox.Show($"Ad: {yolcu.Ad}, Soyad: {yolcu.Soyad}, T.C.: {yolcu.Tc}, Doğum Tarihi: {yolcu.DogumTarihi.ToString("yyyy-MM-dd")}");
            }


            // Bilgilerin yazdırıldığını işaretle
            bilgilerYazdirildi = true;
            // Koltukları Yolculara Ata fonksiyonunu çağır
            KoltuklariYolcularaAta(Yolcular, secilenKoltukIdList, girilenAracId);

        }

        public static List<Reservation> Rezervasyon = new List<Reservation>();

        private void KoltuklariYolcularaAta(List<Passenger> Yolcular, List<int> secilenKoltukIdList, string girilenAracId)
        {
            // Bilgi mesajı göster
            MessageBox.Show("Yolcuların Koltuk Bilgilerini görüntelemek için tıklayınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            int yolcuIndex = 0;

            foreach (var koltukId in secilenKoltukIdList)
            {
                // Daha önce bilgileri girilen ancak koltuk atanmamış bir yolcu seç
                var secilenYolcu = Yolcular.Skip(yolcuIndex).FirstOrDefault(yollcu => yollcu.Tc != null && yollcu.Tc.Trim() != "" && !Rezervasyon.Any(rrezervasyonn => rrezervasyonn.Tc == yollcu.Tc));



                if (secilenYolcu != null)
                {
                    // Yolcu bilgilerini rezervasyona ekle
                    Reservation.Rezervasyon.Add(new Reservation(girilenAracId, koltukId, secilenYolcu.Tc, true));
                    Rezervasyon.Add(new Reservation(girilenAracId, koltukId, secilenYolcu.Tc, true));

                    MessageBox.Show($"Koltuk {koltukId} için {secilenYolcu.Ad} {secilenYolcu.Soyad} atandı.\nArac ID: {girilenAracId}, Yolcu TC: {secilenYolcu.Tc}, Rezerve Durumu: true", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    yolcuIndex++; // Bir sonraki yolcuya geç
                }
                else
                {
                    MessageBox.Show("Atanacak yolcu bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break; // Atanacak yolcu kalmadığında döngüyü sonlandır
                }
            }
        }

        //_______________________________________________________________________
        //Random Kayıt örnekleri oluşturma kısmı

        private Random randomNesne = new Random();
        //random kayıt buttonu
        private void button4_Click_1(object sender, EventArgs e)
        {
            // Reservation sınıfındaki rezervasyonları temizle
            Reservation.Rezervasyon.Clear();

            // ListBox1'i temizle
            listBox1.Items.Clear();

            //   rezervasyon ekleyelim
            for (int i = 0; i < 250; i++)
            {
                // Random bir araç seçimi yap
                string rrandommAracId = RandomAracIdOlustur();

                // Random bir koltuk seçimi yap
                int rrandommKoltukId = RandomKoltukIdOlustur(rrandommAracId);

                // Random bir TC oluştur
                string rrandommTc = RandomTcOlustur();

                // Yeni rezervasyonu oluştur ve listeye ekle
                Reservation.Rezervasyon.Add(new Reservation(rrandommAracId, rrandommKoltukId, rrandommTc, true));

                // ListBox1'e rezervasyon detaylarını ekle
                listBox1.Items.Add($"{i + 1}. Araç ID: {rrandommAracId}, Koltuk ID: {rrandommKoltukId}, TC: {rrandommTc}");

            }
        }

        // Random bir araç ID 
        private string RandomAracIdOlustur()
        {
            // Desenlere göre araç ID'leri
            string[] aracIdleri = { "t1", "t2", "t3", "a1", "a2", "a3", "a4", "b1", "b2", "b3", "b4", "b5" };

            // Random bir araç ID seç
            return aracIdleri[randomNesne.Next(aracIdleri.Length)];
        }

        // Random bir koltuk ID seçen metod
        private int RandomKoltukIdOlustur(string aracId)
        {
            // 1 ile 25 arasındaki koltuk ID'leri (koltukların kapasitesine bağlı)
            List<int> koltukIdList = Enumerable.Range(1, 25).ToList();

            // Random bir koltuk ID seç
            return koltukIdList[randomNesne.Next(koltukIdList.Count)];
        }

        // Random bir TC oluşturan metod
        private string RandomTcOlustur()
        {
            long mindeger = 10000000000;
            long maxdeger = 99999999999;

            // Random bir long sayı üret
            long rrandommTcLong = (long)(randomNesne.NextDouble() * (maxdeger - mindeger) + mindeger);

            return rrandommTcLong.ToString();
        }






        //______________________________________________________________________________________________________________________
    }
}




