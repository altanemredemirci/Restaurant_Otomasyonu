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
    public partial class Siparisler : Form
    {
        public Siparisler()
        {
            InitializeComponent();
        }
        public int adminid;
        DataTable dataTable;
        public void Yenile()
        {
            string sql = @"
                select spd.SiparisToplamID,lk.LokantaAdi,Count(spd.SiparisToplamID) as 'Toplam Sipariş' from SiparisDetay spd ,SiparisToplam stp,Lokantalar lk
                where spd.SiparisToplamID=stp.SiparisToplamID and lk.LokantaID=spd.LokantaID and spd.AktifMi=1
                and lk.KullaniciID="+adminid+" group by spd.SiparisToplamID,lk.LokantaAdi";
            dataTable = Data.select(sql);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;

        }

        private void Siparisler_Load(object sender, EventArgs e)
        {
            Yenile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Yenile();
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int row_index = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            if (row_index == null || row_index < 0 || row_index >= dataTable.Rows.Count) return;
            string siparistoplamid = dataGridView1.Rows[row_index].Cells["SiparisToplamID"].Value.ToString();

            if (siparistoplamid != "" || siparistoplamid != null)
            {
                SiparisDetay siparisDetay = new SiparisDetay();
                siparisDetay.siparisid = Convert.ToInt32(siparistoplamid);

                siparisDetay.Show();
            }
            else
            {
                return;
            }
        }
    }
}
