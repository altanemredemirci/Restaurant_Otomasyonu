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
    public partial class KullaniciSiparis : Form
    {
        public KullaniciSiparis()
        {
            InitializeComponent();
        }
        public int kid;
        public void Yukle()
        {
            string sql = @"SELECT [SiparisID]
                      ,spd.[KullaniciID]
                      ,LK.LokantaAdi
                      ,YMK.YemekID
                      ,spd.[Adet]
                      ,spd.[Tarih],
                      CASE [AktifMi] WHEN '2' THEN 'Gonderildi' WHEN '1 ' THEN 'Hazırlanıyor' WHEN '3 ' THEN 'Reddedildi' 
                      END,[SiparisToplamID]
                  FROM [dbo].[SiparisDetay] spd,Lokantalar LK,Yemekler YMK
				  where spd.LokantaID=LK.LokantaID and spd.YemekID=YMK.YemekID
                  and spd.[KullaniciID]=" + kid + "order by Tarih desc";
            DataTable dt = Data.select(sql);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].Visible = false;

            dataGridView1.Columns[7].Visible = false;

        }
        private void KullaniciSiparis_Load(object sender, EventArgs e)
        {

            Yukle();
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }
        string filtrele;
        private void button1_Click(object sender, EventArgs e)
        {
            filtrele = @"SELECT [SiparisID]
                      ,spd.[KullaniciID]
                      ,LK.LokantaAdi
                      ,YMK.YemekID
                      ,spd.[Adet]
                      ,spd.[Tarih],
                      CASE [AktifMi] WHEN '2' THEN 'Gonderildi' WHEN '1 ' THEN 'Hazırlanıyor' WHEN '3 ' THEN 'Reddedildi' 
                      END,[SiparisToplamID]
                  FROM [dbo].[SiparisDetay] spd,Lokantalar LK,Yemekler YMK
				  where spd.LokantaID=LK.LokantaID and spd.YemekID=YMK.YemekID
                    and spd.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "' and spd.[KullaniciID]=" + kid + " order by Tarih desc";

            DataTable dt = Data.select(filtrele);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].Visible = false;

            dataGridView1.Columns[7].Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Yukle();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {


                filtrele = @"SELECT [SiparisID]
                      ,spd.[KullaniciID]
                      ,LK.LokantaAdi
                      ,YMK.YemekID
                      ,spd.[Adet]
                      ,spd.[Tarih],
                      CASE [AktifMi] WHEN '2' THEN 'Gonderildi' WHEN '1 ' THEN 'Hazırlanıyor' WHEN '3 ' THEN 'Reddedildi' 
                      END,[SiparisToplamID]
                  FROM [dbo].[SiparisDetay] spd,Lokantalar LK,Yemekler YMK
				  where spd.LokantaID=LK.LokantaID and spd.YemekID=YMK.YemekID
                    and spd.AktifMi=1 and spd.[KullaniciID]=" + kid + " order by Tarih desc";

                DataTable dt = Data.select(filtrele);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;

                dataGridView1.Columns[1].Visible = false;

                dataGridView1.Columns[7].Visible = false;
            }
            else
            {
                Yukle();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {


                filtrele = @"SELECT [SiparisID]
                      ,spd.[KullaniciID]
                      ,LK.LokantaAdi
                      ,YMK.YemekID
                      ,spd.[Adet]
                      ,spd.[Tarih],
                      CASE [AktifMi] WHEN '2' THEN 'Gonderildi' WHEN '1 ' THEN 'Hazırlanıyor' WHEN '3 ' THEN 'Reddedildi' 
                      END,[SiparisToplamID]
                  FROM [dbo].[SiparisDetay] spd,Lokantalar LK,Yemekler YMK
				  where spd.LokantaID=LK.LokantaID and spd.YemekID=YMK.YemekID
                    and spd.AktifMi=2 and spd.[KullaniciID]=" + kid + " order by Tarih desc";

                DataTable dt = Data.select(filtrele);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;

                dataGridView1.Columns[1].Visible = false;

                dataGridView1.Columns[7].Visible = false;
            }
            else
            {
                Yukle();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Anasayfa anasayfa = new Anasayfa();
            anasayfa.KullaniciID = kid;
            anasayfa.Show();
            this.Hide();
        }
    }
}
