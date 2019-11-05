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
    public partial class SiparisDetay : Form
    {
        public int siparisid;
        public SiparisDetay()
        {
            InitializeComponent();
        }
        public void Yemekler()
        {
            string sql = @"select ymk.YemekAdi,sparis.Adet,sparis.SiparisToplamID
                        from 
                        SiparisDetay sparis,Yemekler ymk,SiparisToplam stoplam
                        where 
                         ymk.YemekID=sparis.YemekID and sparis.SiparisToplamID=stoplam.SiparisToplamID
                        and sparis.SiparisToplamID="+siparisid;
            DataTable tbl = Data.select(sql);

            dataGridView1.DataSource = tbl;
            dataGridView1.Columns[2].Visible = false;
        }
        DataTable dataTable;
        private void SiparisDetay_Load(object sender, EventArgs e)
        {
            string sqlmusteribilgileri = @"select sparis.SiparisID,kl.Adi,kl.Soyadi,lkn.LokantaAdi,ymk.YemekAdi,sparis.Adet,sparis.Tarih 
	                   ,kl.Adres,kl.Telefon,kl.MailAdresi,sparis.SiparisToplamID
                from 
                SiparisDetay sparis,Kullanicilar kl,Lokantalar lkn,Yemekler ymk,SiparisToplam stoplam
                where 
                sparis.KullaniciID=kl.KullaniciID and lkn.LokantaID=sparis.LokantaID 
                and ymk.YemekID=sparis.YemekID and sparis.SiparisToplamID=stoplam.SiparisToplamID
                and sparis.SiparisToplamID=" +siparisid;

            dataTable = Data.select(sqlmusteribilgileri);
            if (dataTable.Rows.Count>0)
            {
                adilbl.Text = dataTable.Rows[0]["Adi"].ToString();
                lblsoyadi.Text = dataTable.Rows[0]["Soyadi"].ToString();
                telefonlbl.Text = dataTable.Rows[0]["Telefon"].ToString();
                maillbl.Text = dataTable.Rows[0]["MailAdresi"].ToString();
                adreslbl.Text = dataTable.Rows[0]["Adres"].ToString();
            }

            Yemekler();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string yemekonayla = "update SiparisDetay set AktifMi=2 where SiparisToplamID=" + siparisid;
            Data.ExecSql(yemekonayla);

            string yemektoplamonayla = "update SiparisToplam set AktifMi=2 where SiparisToplamID=" + siparisid;
            Data.ExecSql(yemektoplamonayla);

            MessageBox.Show("Yemek Gonderildi.");
            this.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string yemekonayla = "update SiparisDetay set AktifMi=3 where SiparisToplamID=" + siparisid;
            Data.ExecSql(yemekonayla);

            string yemektoplamonayla = "update SiparisToplam set AktifMi=3 where SiparisToplamID=" + siparisid;
            Data.ExecSql(yemektoplamonayla);

            MessageBox.Show("Yemek Reddedildi.");
            this.Hide();
        }
    }
}
