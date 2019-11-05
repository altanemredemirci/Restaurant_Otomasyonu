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
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }
        public int KullaniciID;
        private void Anasayfa_Load(object sender, EventArgs e)
        {
            string sql = "select * from iller";
            DataTable table = Data.select(sql);
            comboBox2.DataSource = table;
            comboBox2.DisplayMember = "sehir";
            comboBox2.ValueMember = "id";
            comboBox2.SelectedIndex = -1;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(comboBox2.SelectedValue) > 0)
                {
                    string sql = "select * from ilceler where sehir=" + comboBox2.SelectedValue;
                    DataTable table = Data.select(sql);
                    comboBox1.DataSource = table;
                    comboBox1.DisplayMember = "ilce";
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
        DataTable table;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(comboBox2.SelectedValue) > 0)
                {
                    string sql = @"select L.LokantaID,L.LokantaAdi,İL.sehir,kl.UstYetki,
                                i.ilce,L.Adres,L.Aktif from Lokantalar L,iller il,Kullanicilar kl,
                                ilceler i  where L.IlceID=i.id and L.ILID=il.id 
                                and L.Aktif=1  and l.KullaniciID=kl.KullaniciID and kl.UstYetki=1 and IlceID=" + comboBox1.SelectedValue;
                    table = Data.select(sql);
                    dataGridView1.DataSource = table;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                }
                else
                {
                    return;
                }

            }
            catch (Exception hata)
            {

                MessageBox.Show(hata.Message);
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (row_index == null || row_index < 0 || row_index >= table.Rows.Count) return;
            //Sağ Tık
            string lokantaid;


            
           

                int[] selectedIndexes = new int[dataGridView1.SelectedRows.Count];
                if (table.Rows.Count < 1) return;
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu menu = new ContextMenu();
                    MenuItem item = new MenuItem("Yorumları Gor");
                    if (row_index == null
                        || row_index < 0
                        || row_index >= table.Rows.Count) return;

                    item.Click += new EventHandler(delegate (object s, EventArgs args)
                    {


                        lokantaid = dataGridView1.Rows[row_index].Cells["LokantaID"].Value.ToString();



                        LokantaYorumlari lokantaYorumlari = new LokantaYorumlari();
                        lokantaYorumlari.lokantaid = lokantaid;

                        lokantaYorumlari.Show();
                        

                    });

                    menu.MenuItems.Add(item);

                ////YORUM YAPME

                item = new MenuItem("Yorum Yap");



                item.Click += new EventHandler(delegate (object s, EventArgs args)
                {


                    if (row_index == null  || row_index < 0 || row_index >= table.Rows.Count) return;
                    string lokantayorum = dataGridView1.Rows[row_index].Cells["LokantaID"].Value.ToString();

                    YorumYap yorumYap = new YorumYap();
                    yorumYap.lokantaid = lokantayorum;
                    yorumYap.kullaniciid = KullaniciID;
                    yorumYap.Show();
                });

                menu.MenuItems.Add(item);

                menu.Show(dataGridView1, new Point(e.X, e.Y));



            }
            
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (row_index == null || row_index < 0 || row_index >= table.Rows.Count) return;
            string lokantaid = dataGridView1.Rows[row_index].Cells["LokantaID"].Value.ToString();

            if (lokantaid != "" || lokantaid != null)
            {
                YemekSiparis yemekSiparis = new YemekSiparis();
                yemekSiparis.lokantaid = Convert.ToInt32(lokantaid);
                yemekSiparis.kullaniciid = KullaniciID;
                yemekSiparis.Show();
            }
            else
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void tumSiparişlerimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KullaniciSiparis kullaniciSiparis = new KullaniciSiparis();
            kullaniciSiparis.kid = KullaniciID;
            kullaniciSiparis.Show();
            this.Hide();
        }

        private void siparişlerimToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bilgilerimiGuncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KullaniciBilgileriGuncelle kullaniciBilgileriGuncelle = new KullaniciBilgileriGuncelle();
            kullaniciBilgileriGuncelle.kid = KullaniciID;
            kullaniciBilgileriGuncelle.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}