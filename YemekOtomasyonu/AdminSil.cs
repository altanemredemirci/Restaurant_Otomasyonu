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
    public partial class AdminSil : Form
    {
        public AdminSil()
        {
            InitializeComponent();
        }
        public int adminid;
        DataTable tbl;

        void Goster()
        {
            string kullanici = @"SELECT  
	             [KullaniciID]
                  ,[Adi]
                  ,[Soyadi]
                  ,[KullaniciAdi]
                  ,[Sifre]
                  ,[Adres]
                  ,[Telefon]
                  ,[MailAdresi]
                  ,[Yetki]
                  ,[UstYetki]
	              ,Ekleyen
              FROM [Kullanicilar]
              where 
                 Ekleyen=" + adminid;
            tbl = Data.select(kullanici);
            dataGridView1.DataSource = tbl;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;

        }
        private void AdminSil_Load(object sender, EventArgs e)
        {
            Goster();

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (row_index == null || row_index < 0 || row_index >= tbl.Rows.Count) return;
            //Sağ Tık
            string kullaniciid;





            int[] selectedIndexes = new int[dataGridView1.SelectedRows.Count];
            if (tbl.Rows.Count < 1) return;
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu menu = new ContextMenu();
                MenuItem item = new MenuItem("Yorumları Gor");
                if (row_index == null
                    || row_index < 0
                    || row_index >= tbl.Rows.Count) return;

                item.Click += new EventHandler(delegate (object s, EventArgs args)
                {


                    kullaniciid = dataGridView1.Rows[row_index].Cells["KullaniciID"].Value.ToString();

                    DialogResult dialogResult = MessageBox.Show("Silmek İstediğinize Eminmisiniz?", "Kullanıcı Sil", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        string lokantalarsil = "delete Lokantalar where KullaniciID = " + kullaniciid;
                        Data.ExecSql(lokantalarsil);

                        string kullanicisil = "delete Kullanicilar where KullaniciID = " + kullaniciid;
                        Data.ExecSql(kullanicisil);

                        MessageBox.Show("Silindi");
                        Goster();

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
