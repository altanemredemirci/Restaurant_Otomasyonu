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
    public partial class Lokantalar : Form
    {
        public Lokantalar()
        {
            InitializeComponent();
        }
        public int kid;
        DataTable tbl;
        public void Goster()
        {
            string sql = @"select lk.LokantaID,lk.LokantaAdi,il.sehir,ilc.ilce,lk.Adres,lk.KullaniciID from Lokantalar lk,iller il,ilceler ilc
                            where lk.ILID = il.id and lk.IlceID = ilc.id and lk.KullaniciID = " + kid;
            tbl = Data.select(sql);
            dataGridView1.DataSource = tbl;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void Lokantalar_Load(object sender, EventArgs e)
        {
            Goster();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;

            if (tbl.Rows.Count < 1) return;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem item = new MenuItem("Sil");


                if (row_index == null
                    || row_index < 0
                    || row_index >= tbl.Rows.Count) return;

                item.Click += new EventHandler(delegate (object s, EventArgs args)
                {
                    string lkid = dataGridView1.Rows[row_index].Cells["LokantaID"].Value.ToString();

                    DialogResult dialogResult = MessageBox.Show("Silmek İstediğinize Eminmisiniz?", "Lokanta Sil", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string lsil = "Delete from Lokantalar where LokantaID=" + lkid;
                        Data.ExecSql(lsil);
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
