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
    public partial class LokantaEkle : Form
    {
        public LokantaEkle()
        {
            InitializeComponent();
        }
        public int Kid;
        private void LokantaEkle_Load(object sender, EventArgs e)
        {
            string sql = "select * from iller";
            DataTable table = Data.select(sql);
            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "sehir";
            comboBox1.ValueMember = "id";
            comboBox1.SelectedIndex = -1;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView oDataRowView = comboBox2.SelectedItem as DataRowView;
                int sValue = 0;

                if (oDataRowView != null)
                {
                    sValue = Convert.ToInt32(oDataRowView.Row["id"]);
                    string sql = "INSERT INTO Lokantalar(LokantaAdi,ILID,IlceID,Adres,KullaniciID,Aktif) VALUES('" + textBox1.Text + "','" + comboBox1.SelectedValue + "','" + sValue+ "','" + textBox2.Text + "','" + Kid + "','1')";
                    Data.ExecSql(sql);
                    MessageBox.Show("Lokanta Eklendi");
                    this.Hide();

                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
                
            }




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {



                if (Convert.ToInt32(comboBox1.SelectedValue) > 0)
                {
                    string sql = "select * from ilceler where sehir=" + comboBox1.SelectedValue;
                    DataTable table = Data.select(sql);
                    comboBox2.DataSource = table;
                    comboBox2.DisplayMember = "ilce";
                    comboBox1.ValueMember = "id";
                }
                else
                {
                    return;
                }

            }
            catch (Exception)
            {

             
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
