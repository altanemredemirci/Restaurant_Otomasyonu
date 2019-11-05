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
    public partial class KullaniciBilgileriGuncelle : Form
    {
        public KullaniciBilgileriGuncelle()
        {
            InitializeComponent();
        }
        DataTable dataTable;
        public int kid;
        private void KullaniciBilgileriGuncelle_Load(object sender, EventArgs e)
        {
            string sql = "select * from Kullanicilar where KullaniciID=" + kid;
            dataTable = Data.select(sql);
            txt_ad.Text = dataTable.Rows[0]["Adi"].ToString();
            txt_soyad.Text = dataTable.Rows[0]["Soyadi"].ToString();
            txt_kullanici.Text = dataTable.Rows[0]["KullaniciAdi"].ToString();
            txt_mail.Text = dataTable.Rows[0]["MailAdresi"].ToString();
            txt_telefon.Text = dataTable.Rows[0]["Telefon"].ToString();
            txt_adres.Text = dataTable.Rows[0]["Adres"].ToString();
            sifrekayit.Text = dataTable.Rows[0]["Sifre"].ToString();
            txt_sifretekrar.Text = dataTable.Rows[0]["Sifre"].ToString();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            string kullaniciadikontrol = "Select Count(*) from Kullanicilar where KullaniciAdi='" + txt_kullanici.Text + "'";
            DataTable tblKullanici = Data.select(kullaniciadikontrol);
            if (txt_kullanici.Text != dataTable.Rows[0]["KullaniciAdi"].ToString())
            {
                if (Convert.ToInt32(tblKullanici.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Kullanici Adı Daha Onceden Alınmış.");
                    return;
                }
            }

            if (!reg.IsMatch(txt_mail.Text))
            {
                MessageBox.Show("Geçersiz Mail Adresi");
                return;
            }

            if (txt_mail.Text != dataTable.Rows[0]["MailAdresi"].ToString())
            {
                string mailkontrol = "Select Count(*) from Kullanicilar where MailAdresi='" + txt_mail.Text + "'";
                DataTable tblmailkontrol = Data.select(mailkontrol);
                if (Convert.ToInt32(tblmailkontrol.Rows[0][0]) > 0)
                {
                    MessageBox.Show("Mail Adresi Kullanılıyor.");
                    return;
                }
            }


            if (sifrekayit.Text != txt_sifretekrar.Text)
            {
                MessageBox.Show("Şifreniz uyuşmuyor.");
            }
            else
            {
                if (txt_kullanici.Text == String.Empty || txt_soyad.Text == String.Empty || txt_ad.Text == String.Empty || sifrekayit.Text == String.Empty || txt_adres.Text == String.Empty || txt_telefon.Text == String.Empty || txt_mail.Text == String.Empty || txt_sifretekrar.Text == String.Empty)
                {

                    MessageBox.Show("Alanları Boş Geçemezsiniz", "Boş Alan Hatası");
                    return;
                }
                else
                {
                    string kguncelle = "update Kullanicilar set Adi='" + txt_ad.Text + "',Soyadi='" + txt_soyad.Text + "',KullaniciAdi='" + txt_kullanici.Text + "',Sifre='" + sifrekayit.Text + "',Adres='" + txt_adres.Text + "',Telefon='" + txt_telefon.Text + "',MailAdresi='" + txt_mail.Text + "' where KullaniciID=" + kid;
                    Data.ExecSql(kguncelle);
                    MessageBox.Show("Bilgileriniz Guncellenmiştir.");
                    this.Hide();
                }
            }
        }
    }
}
