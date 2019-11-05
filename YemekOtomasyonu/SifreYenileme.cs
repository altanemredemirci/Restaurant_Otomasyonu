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
    public partial class SifreYenileme : Form
    {
        public SifreYenileme()
        {
            InitializeComponent();
        }
        public int kullaniciid;
        public int sifrevar = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (sifrevar == 1)
            {
                string sorgu = "Select * from SifremiUnuttum where Kid='" + kullaniciid +"' and Durum=1";
                DataTable table = Data.select(sorgu);
                if (table.Rows[0]["Kod"].ToString() == SifreKoduTxt.Text)
                {
                    if (YeniSifreTxt.Text != YeniSifreTekrarTxt.Text)
                    {
                        MessageBox.Show("Şifreler Uyuşmuyor..");
                        return;
                    }
                    else
                    {
                        string degistir = "Update Kullanicilar set Sifre='" + YeniSifreTxt.Text + "' where KullaniciID=" + kullaniciid;
                        Data.ExecSql(degistir);
                        MessageBox.Show("Değiştirildi");
                        string deger = "Select * from Kullanicilar where KullaniciID=" + kullaniciid;
                        DataTable tbl = Data.select(deger);
                        string kodsil = "Update SifremiUnuttum set Guncelleme_Tarih='" + DateTime.Today.ToString("yyyy-MM-dd") + "',Durum=0 where Kid=" + kullaniciid;
                        Data.ExecSql(kodsil);

                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Girdiğiniz Kod Yanlış");
                }



            }

            else
            {


                string sorgu = "Select * from SifremiUnuttum where Kid=" + kullaniciid;
                DataTable table = Data.select(sorgu);
                if (table.Rows[0]["Kod"].ToString() == SifreKoduTxt.Text)
                {
                    if (YeniSifreTxt.Text != YeniSifreTekrarTxt.Text)
                    {
                        MessageBox.Show("Şifreler Uyuşmuyor..");
                        return;
                    }
                    else
                    {
                        string degistir = "Update Kullanicilar set Sifre='" + YeniSifreTxt.Text + "' where KullaniciID=" + kullaniciid;
                        Data.ExecSql(degistir);
                        MessageBox.Show("Değiştirildi");
                        string deger = "Select * from Kullanicilar where KullaniciID=" + kullaniciid;
                        DataTable tbl = Data.select(deger);
                        string kodsil = "Update SifremiUnuttum set Guncelleme_Tarih='" + DateTime.Today.ToString("yyyy-MM-dd") + "',Durum=0 where Kid=" + kullaniciid;
                        Data.ExecSql(kodsil);

                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Girdiğiniz Kod Yanlış");
                }

            }
        }

        private void SifreYenileme_Load(object sender, EventArgs e)
        {

        }
    }
}
