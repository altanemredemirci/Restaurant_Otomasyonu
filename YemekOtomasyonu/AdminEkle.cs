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
    public partial class AdminEkle : Form
    {
        public AdminEkle()
        {
            InitializeComponent();
        }
        public int id;

        void Lokanta(DataTable table)
        {


        }

        private void button4_Click(object sender, EventArgs e)
        {
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
                    string sql = "INSERT INTO Kullanicilar values('" + txt_ad.Text + "','" + txt_soyad.Text + "','" + txt_kullanici.Text + "','" + sifrekayit.Text + "','" + txt_adres.Text + "','" + txt_telefon.Text + "','" + txt_mail.Text + "','1','0','"+id+"')SELECT SCOPE_IDENTITY();";
                    string yeniid=Data.execScalar(sql);

                    string adminlokantalar = "select * from Lokantalar where KullaniciID=" + id;
                    DataTable tbl = Data.select(adminlokantalar);

                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        string ekle= "Insert into Lokantalar(LokantaAdi,ILID,IlceID,Adres,KullaniciID,Aktif) values('"+tbl.Rows[i]["LokantaAdi"] +"','"+ tbl.Rows[i]["ILID"] + "','"+ tbl.Rows[i]["IlceID"] + "','"+ tbl.Rows[i]["Adres"] + "','"+ yeniid+ "','"+ tbl.Rows[i]["Aktif"] + "')";
                        Data.ExecSql(ekle);
                    }


                    MessageBox.Show("Kaydınız Oluşturulmuştur..");
                    this.Hide();
                }
            }
        }
    }
}
