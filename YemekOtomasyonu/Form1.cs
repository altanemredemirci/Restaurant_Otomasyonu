using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YemekOtomasyonu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string kullanici = "select * from Kullanicilar where KullaniciAdi='" + txt_KullaniciAdi.Text + "' and Sifre='" + txt_Sifre.Text + "'";
                if (Data.select(kullanici).Rows.Count > 0)
                {

                    DataTable table = Data.select(kullanici);

                    string sifreyenilemekontrol = "Select * from SifremiUnuttum where Kid='" + table.Rows[0]["KullaniciId"].ToString() + "' and Durum=1";
                    if (Data.select(sifreyenilemekontrol).Rows.Count > 0)
                    {
                        MessageBox.Show("Daha Önceden Şifre Yenileme İsteğinde Bulunduğunuz İçin Şifre Yenileme Sayfasına Yönlendiriliyorsunuz");
                        SifreYenileme sifreYenilemeKod = new SifreYenileme();
                        sifreYenilemeKod.kullaniciid = Convert.ToInt32(table.Rows[0]["KullaniciID"]);
                        sifreYenilemeKod.Show();
                        return;
                    }


                    string yetki = table.Rows[0]["Yetki"].ToString();
                    if (yetki == "1")
                    {
                        AdminPaneli adminPaneli = new AdminPaneli();
                        adminPaneli.adminid = Convert.ToInt32(table.Rows[0]["KullaniciID"].ToString());
                        adminPaneli.Show();
                        this.Hide();
                    }
                    else
                    {
                        Anasayfa anasyf = new Anasayfa();
                        anasyf.KullaniciID = Convert.ToInt32(table.Rows[0]["KullaniciID"].ToString());
                        anasyf.Show();
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı Veya Şifre Yanlış");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Geçersiz!");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            

            Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (txt_kullanici.Text == String.Empty || txt_soyad.Text == String.Empty || txt_ad.Text == String.Empty || sifrekayit.Text == String.Empty || txt_adres.Text == String.Empty || txt_telefon.Text == String.Empty || txt_mail.Text == String.Empty)
            {

                MessageBox.Show("Alanları Boş Geçemezsiniz", "Boş Alan Hatası");
                return;
            }
            string kullaniciadikontrol = "Select Count(*) from Kullanicilar where KullaniciAdi='" + txt_kullanici.Text+"'";
            DataTable tblKullanici = Data.select(kullaniciadikontrol);
            if (Convert.ToInt32(tblKullanici.Rows[0][0]) > 0)
            {
                MessageBox.Show("Kullanici Adı Daha Onceden Alınmış.");
                return;
            }
           
            if (!reg.IsMatch(txt_mail.Text))
            {
                MessageBox.Show("Geçersiz Mail Adresi");
                return;
            }

            string mailkontrol = "Select Count(*) from Kullanicilar where MailAdresi='" + txt_mail.Text+"'";
            DataTable tblmailkontrol = Data.select(mailkontrol);
            if (Convert.ToInt32(tblmailkontrol.Rows[0][0]) > 0)
            {
                MessageBox.Show("Mail Adresi Kullanılıyor.");
                return;
            }


            if (txt_telefon.Text.Length !=14)
            {
                MessageBox.Show("Telefon Numarası Alanı Boş Geçilemez");
                return;
            }
            else
            {

                string sql = "INSERT INTO Kullanicilar values('" + txt_ad.Text + "','" + txt_soyad.Text + "','" + txt_kullanici.Text + "','" + sifrekayit.Text + "','" + txt_adres.Text + "','" + txt_telefon.Text + "','" + txt_mail.Text + "','0','0','0')";
                Data.ExecSql(sql);
                MessageBox.Show("Kaydınız Oluşturulmuştur..");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SifremiUnuttum sifremiUnuttum = new SifremiUnuttum();
            sifremiUnuttum.Show();
        }
    }
}
