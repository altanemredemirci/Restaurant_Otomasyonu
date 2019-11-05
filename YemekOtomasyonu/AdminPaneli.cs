using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YemekOtomasyonu
{
    public partial class AdminPaneli : Form
    {
        public int adminid;
        public AdminPaneli()
        {
            InitializeComponent();
        }

        private void AdminPaneli_Load(object sender, EventArgs e)
        {
            string ustyetkikontrol = "Select UstYetki from Kullanicilar where KullaniciID=" + adminid;
            DataTable tbl = Data.select(ustyetkikontrol);
            if (tbl.Rows[0]["UstYetki"].ToString() == "1")
            {
                adminSilToolStripMenuItem.Visible = true;
                button1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Sadece Ust Yetkisi Olan Kullanici Lokanta Ekleyebilir", "Uyarı");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Lokantalar lokantalar = new Lokantalar();
            lokantalar.kid = adminid;
            lokantalar.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LokantaEkle lokantaEkle = new LokantaEkle();
            lokantaEkle.Kid = adminid;
            lokantaEkle.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YemekEkle yemekEkle = new YemekEkle();
            yemekEkle.adminid = adminid;
            yemekEkle.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            YemekListesi yemekListesi = new YemekListesi();
            yemekListesi.aid = adminid;
            yemekListesi.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminLokantaYorumlari adminLokantaYorumlari = new AdminLokantaYorumlari();
            adminLokantaYorumlari.adminid = adminid;
            adminLokantaYorumlari.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Siparisler siparisler = new Siparisler();
            siparisler.adminid = adminid;
            siparisler.Show();
        }

        private void kullanıcıBilgilerimiGuncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KullaniciBilgileriGuncelle kullaniciBilgileriGuncelle = new KullaniciBilgileriGuncelle();
            kullaniciBilgileriGuncelle.kid = adminid;
            kullaniciBilgileriGuncelle.Show();
        }

        private void adminEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminEkle adminEkle = new AdminEkle();
            adminEkle.id = adminid;
            adminEkle.Show();
        }

        private void adminSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminSil adminSil = new AdminSil();
            adminSil.adminid = adminid;
            adminSil.Show();
        }

        private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
