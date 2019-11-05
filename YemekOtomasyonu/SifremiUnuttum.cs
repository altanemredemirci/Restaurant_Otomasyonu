using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YemekOtomasyonu
{
    public partial class SifremiUnuttum : Form
    {
        public SifremiUnuttum()
        {
            InitializeComponent();
        }
        private string CreatePassword(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPRSTUVYZWXQ1234567890";
            StringBuilder Password = new StringBuilder();
            Random Rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                Password.Append(chars[Rnd.Next(0, chars.Length)]);
            }

            return Password.ToString();
        }
        char karakter;
        int sayi;
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!reg.IsMatch(emailtxt.Text))
            {
                MessageBox.Show("Geçersiz Mail Adresi");
                return;
            }

            string sifreyenilemekontrol = "Select * from SifremiUnuttum where Mail='" + emailtxt.Text + "' and Durum=1";
            if (Data.select(sifreyenilemekontrol).Rows.Count > 0)
            {
                DataTable tbl = Data.select(sifreyenilemekontrol);
                MessageBox.Show("Daha Önceden Şifre Yenileme İsteğinde Bulunduğunuz İçin Şifre Yenileme Sayfasına Yönlendiriliyorsunuz");
                SifreYenileme sifreYenilemeKod = new SifreYenileme();
                sifreYenilemeKod.kullaniciid = Convert.ToInt32(tbl.Rows[0]["Kid"]);
                sifreYenilemeKod.sifrevar = 1;
                sifreYenilemeKod.Show();
                return;
            }




            string sifre = CreatePassword(6);
            string emailsorgu = "select * from Kullanicilar where MailAdresi='" + emailtxt.Text + "'";
            if (Data.select(emailsorgu).Rows.Count > 0)
            {
                DataTable table = Data.select(emailsorgu);
                MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress("tiklaa.gelsinnn@gmail.com");
                ePosta.To.Add(emailtxt.Text);
                ePosta.Subject = "Parolanızı Yenileyin";
                ePosta.Body = sifre;
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential("tiklaa.gelsinnn@gmail.com", "Tikla1234");
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                object userState = ePosta;
                bool kontrol = true;
                try
                {
                    smtp.SendAsync(ePosta, (object)ePosta);
                    string kodekle = "INSERT INTO SifremiUnuttum(Mail,Kod,Kid,Olusturulan_Tarih,Durum) Values('"
                + emailtxt.Text + "','" + sifre + "','" + table.Rows[0]["KullaniciID"].ToString() + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "','1')";
                    Data.ExecSql(kodekle);
                    string sorgu = "Select KullaniciID from Kullanicilar where MailAdresi='" + emailtxt.Text.Trim() + "'";
                    DataTable tbl = Data.select(sorgu);
                    SifreYenileme sifreYenileme = new SifreYenileme();
                    sifreYenileme.kullaniciid = Convert.ToInt32(tbl.Rows[0]["KullaniciID"]);
                    sifreYenileme.Show();
                    this.Hide();
                }
                catch (SmtpException ex)
                {
                    kontrol = false;
                    System.Windows.Forms.MessageBox.Show(ex.Message, "Mail Gönderme Hatasi");
                }
            }
            else
            {
                MessageBox.Show("Mail Adresine Kayıtlı Kullanıcı Yoktur..");
            }
        }
    }
}
