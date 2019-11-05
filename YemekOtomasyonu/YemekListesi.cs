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
    public partial class YemekListesi : Form
    {
        public YemekListesi()
        {
            InitializeComponent();
        }
        public int aid;
        private void YemekListesi_Load(object sender, EventArgs e)
        {
            string sql = "select * from Lokantalar where KullaniciID=" + aid;
            DataTable table2 = Data.select(sql);
            comboBox1.DataSource = table2;
            comboBox1.DisplayMember = "LokantaAdi";
            comboBox1.ValueMember = "LokantaID";
            comboBox1.SelectedIndex = -1;
        }
        DataTable table;
       
        public void Goster()
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
                    string sql = @"select ymk.YemekID,ymk.YemekAdi,ktg.KategoriAdi,ymk.Resmi,lk.LokantaAdi,ymk.Fiyat from Yemekler ymk,Kategoriler ktg,Lokantalar lk
                                     where ymk.KategoriID = ktg.KategoriID
                                        and ymk.LokantaID=lk.LokantaID
										and ymk.LokantaID=" + sValue;

                    table = Data.select(sql);
                    dataGridView1.DataSource = table;

                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Goster();


        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            pictureBox1.ImageLocation = "";


            if (row_index == null
                       || row_index < 0
                       || row_index >= table.Rows.Count) return;
            string resim1 = "Select * from Yemekler where YemekID=" + dataGridView1.Rows[row_index].Cells["YemekID"].Value.ToString();
            if (Data.select(resim1).Rows.Count > 0)
            {

                try
                {
                    DataTable table = Data.select(resim1);
                    if (table.Rows[0]["Resmi"].ToString() != "")
                    {
                        pictureBox1.ImageLocation = Application.StartupPath + @"\resimler\" + table.Rows[0]["Resmi"].ToString();
                    }
                    else
                    {
                        pictureBox1.ImageLocation = "";
                    }

                }
                catch (Exception)
                {


                }


            }


            if (table.Rows.Count < 1) return;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem item = new MenuItem("Sil");


                if (row_index == null
                    || row_index < 0
                    || row_index >= table.Rows.Count) return;

                item.Click += new EventHandler(delegate (object s, EventArgs args)
                {
                    string yid = dataGridView1.Rows[row_index].Cells["YemekID"].Value.ToString();

                    DialogResult dialogResult = MessageBox.Show("Silmek İstediğinize Eminmisiniz?", "Yemek Sil", MessageBoxButtons.YesNo);
                    if (dialogResult==DialogResult.Yes)
                    {
                        string ysil = "Delete from Yemekler where YemekID=" + yid;
                        Data.ExecSql(ysil);
                        MessageBox.Show("Silindi");

                        Goster();
                    }
                    else
                    {
                        return;
                    }







                });

                menu.MenuItems.Add(item);
                menu.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }
    } 
}
