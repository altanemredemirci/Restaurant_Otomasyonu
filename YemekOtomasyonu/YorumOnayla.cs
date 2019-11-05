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
    public partial class YorumOnayla : Form
    {
        public YorumOnayla()
        {
            InitializeComponent();
        }
        public int adminid;
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
                            where y.AktifMi=0 and y.KullaniciID = k.KullaniciID and l.LokantaID =" + sValue;

                    dataTable = Data.select(sql);
                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns[0].Visible = false;

                }
            }

        }
        private void YorumOnayla_Load(object sender, EventArgs e)
        {
            string sql = "select * from Lokantalar where KullaniciID=" + adminid;
            DataTable table = Data.select(sql);
            comboBox1.DataSource = table;
            comboBox1.DisplayMember = "LokantaAdi";
            comboBox1.ValueMember = "LokantaID";
            comboBox1.SelectedIndex = -1;
        }
        DataTable dataTable;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Getir();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (row_index == null || row_index < 0 || row_index >= dataTable.Rows.Count) return;
            //Sağ Tık
            string yorumid;

            int[] selectedIndexes = new int[dataGridView1.SelectedRows.Count];
            if (dataTable.Rows.Count < 1) return;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem item = new MenuItem("Aktif Et");
                if (row_index == null
                    || row_index < 0
                    || row_index >= dataTable.Rows.Count) return;

                item.Click += new EventHandler(delegate (object s, EventArgs args)
                {


                    yorumid = dataGridView1.Rows[row_index].Cells["YorumID"].Value.ToString();


                    DialogResult dialogResult = MessageBox.Show("Yorum Aktif Edilsin Mi?", "Aktif Et", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string aktifsql = "Update Yorumlar set AktifMi=1 where YorumID=" + yorumid;
                        Data.ExecSql(aktifsql);
                        MessageBox.Show("Aktif Edildi.");
                        Getir();
                    }
                    else if (dialogResult == DialogResult.No)
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
