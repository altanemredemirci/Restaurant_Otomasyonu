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
    public partial class AdminLokantaYorumlari : Form
    {
        public AdminLokantaYorumlari()
        {
            InitializeComponent();
        }
        public int adminid;

        private void AdminLokantaYorumlari_Load(object sender, EventArgs e)
        {
            string sql = "select * from Lokantalar where KullaniciID=" + adminid;
            DataTable table = Data.select(sql);
            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "LokantaAdi";
            comboBox1.ValueMember = "LokantaID";
            comboBox1.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YorumOnayla yorumOnayla = new YorumOnayla();
            yorumOnayla.adminid = adminid;
            yorumOnayla.Show();
        }
        DataTable dataTable;
        public void Getir()
        {
            if (comboBox1.SelectedIndex == -1)
            {
                dataGridView1.DataSource = "";
            }
            else
            {
                DataRowView oDataRowView = comboBox1.SelectedItem as DataRowView;
                int sValue = 0;

                if (oDataRowView != null)
                {
                    sValue = Convert.ToInt32(oDataRowView.Row["LokantaID"]);
                    string sql = @"SELECT y.YorumID,CONVERT(DATE, y.Tarih, 104) AS Tarih, k.KullaniciAdi,y.Yorum FROM Yorumlar y, Kullanicilar k,Lokantalar l
                            where y.AktifMi=1 and y.KullaniciID = k.KullaniciID and l.LokantaID =" + sValue;

                    dataTable = Data.select(sql);
                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns[0].Visible = false;

                }
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Getir();
        }
    }
}
