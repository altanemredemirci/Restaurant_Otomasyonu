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
    public partial class LokantaYorumlari : Form
    {
        public LokantaYorumlari()
        {
            InitializeComponent();
        }
        public string lokantaid;
        private void LokantaYorumlari_Load(object sender, EventArgs e)
        {
            string sql = @"SELECT CONVERT(DATE, y.Tarih, 104) AS Tarih, k.KullaniciAdi,y.Yorum FROM Yorumlar y, Kullanicilar k,Lokantalar l
                            where y.AktifMi=1 and y.KullaniciID = k.KullaniciID and l.LokantaID ="+lokantaid;
            DataTable dataTable = Data.select(sql);
            if (dataTable.Rows.Count>0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    string[] row = { dataTable.Rows[i]["Tarih"].ToString(), dataTable.Rows[i]["KullaniciAdi"].ToString(), dataTable.Rows[i]["Yorum"].ToString() };
                    var satir = new ListViewItem(row);
                    listView1.Items.Add(satir);
                }
            }
            else
            {
                listView1.Items.Add("Yorum Yok.İlk Yapan Sen Ol...");
            }
        }

        private void listView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            
        }
    }
}
