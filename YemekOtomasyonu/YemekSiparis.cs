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
    public partial class YemekSiparis : Form
    {
        public YemekSiparis()
        {
            InitializeComponent();
        }
        public int lokantaid;
        public int kullaniciid;
        decimal toplam = 0;
        private void YemekSiparis_Load(object sender, EventArgs e)
        { 
           

            string corbasql = "Select * from Yemekler where KategoriID=1";
            DataTable table2 = Data.select(corbasql);
            comboBox1.DataSource = table2;
            comboBox1.DisplayMember = "YemekAdi";
            comboBox1.ValueMember = "YemekID";
            comboBox1.SelectedIndex = -1;
            listView1.Columns[3].Width = -1;


            string anayemeksql = "Select * from Yemekler where KategoriID=2";
            DataTable table3 = Data.select(anayemeksql);
            cmb_anayemek.DataSource = table3;
            cmb_anayemek.DisplayMember = "YemekAdi";
            cmb_anayemek.ValueMember = "YemekID";
            cmb_anayemek.SelectedIndex = -1;

            string arayemek = "Select * from Yemekler where KategoriID=3";
            DataTable table4 = Data.select(arayemek);
            cmb_arayemek.DataSource = table4;
            cmb_arayemek.DisplayMember = "YemekAdi";
            cmb_arayemek.ValueMember = "YemekID";
            cmb_arayemek.SelectedIndex = -1;

            string tatlisql = "Select * from Yemekler where KategoriID=4";
            DataTable table5 = Data.select(tatlisql);
            cmb_tatli.DataSource = table5;
            cmb_tatli.DisplayMember = "YemekAdi";
            cmb_tatli.ValueMember = "YemekID";
            cmb_tatli.SelectedIndex = -1;


            string icecklersql = "Select * from Yemekler where KategoriID=5";
            DataTable table6 = Data.select(icecklersql);
            cmb_icecek.DataSource = table6;
            cmb_icecek.DisplayMember = "YemekAdi";
            cmb_icecek.ValueMember = "YemekID";
            cmb_icecek.SelectedIndex = -1;


        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (numericUpDown1.Value==0 || comboBox1.SelectedIndex<0)
            {
                MessageBox.Show("Lutfen Alanı veya Adeti uygun biçimde giriniz.");
            }
            
            else
            {
                string corbasql = "Select Fiyat from Yemekler where KategoriID=1 and YemekID=" + comboBox1.SelectedValue;
                DataTable tbl = Data.select(corbasql);
                int fiyat = Convert.ToInt32(tbl.Rows[0]["Fiyat"]);

                decimal islem = fiyat * numericUpDown1.Value;

                string[] row = { comboBox1.Text, numericUpDown1.Value.ToString(), islem.ToString() ,comboBox1.SelectedValue.ToString()};

                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);

                ToplamFiyat(islem);
            }

            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (nmr_anayemek.Value == 0 || cmb_anayemek.SelectedIndex < 0)
            {
                MessageBox.Show("Lutfen Alanı veya Adeti uygun biçimde giriniz.");
            }

            else
            {
                string anayemeksql = "Select Fiyat from Yemekler where KategoriID=2 and YemekID=" + cmb_anayemek.SelectedValue;
                DataTable tbl = Data.select(anayemeksql);
                int fiyat = Convert.ToInt32(tbl.Rows[0]["Fiyat"]);
                decimal islem = fiyat * nmr_anayemek.Value;

                string[] row = { cmb_anayemek.Text, nmr_anayemek.Value.ToString(), islem.ToString(),cmb_anayemek.SelectedValue.ToString() };
                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);
                ToplamFiyat(islem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nmr_arayemek.Value == 0 || cmb_arayemek.SelectedIndex < 0)
            {
                MessageBox.Show("Lutfen Alanı veya Adeti uygun biçimde giriniz.");
            }

            else
            {
                string arayemek = "Select Fiyat from Yemekler where KategoriID=3 and YemekID=" + cmb_arayemek.SelectedValue;
                DataTable tbl = Data.select(arayemek);
                int fiyat = Convert.ToInt32(tbl.Rows[0]["Fiyat"]);
                decimal islem = fiyat * nmr_arayemek.Value;

                string[] row = { cmb_arayemek.Text, nmr_arayemek.Value.ToString(),islem.ToString(), cmb_arayemek.SelectedValue.ToString() };
                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);
                ToplamFiyat(islem);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (nmr_tatli.Value == 0 || cmb_tatli.SelectedIndex < 0)
            {
                MessageBox.Show("Lutfen Alanı veya Adeti uygun biçimde giriniz.");
            }

            else
            {
                string tatli = "Select Fiyat from Yemekler where KategoriID=4 and YemekID=" + cmb_tatli.SelectedValue;
                DataTable tbl = Data.select(tatli);
                int fiyat = Convert.ToInt32(tbl.Rows[0]["Fiyat"]);
                decimal islem = fiyat * nmr_tatli.Value;

                string[] row = { cmb_tatli.Text, nmr_tatli.Value.ToString(), islem.ToString(),cmb_tatli.SelectedValue.ToString() };
                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);
                ToplamFiyat(islem);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (nmr_icecek.Value == 0 || cmb_icecek.SelectedIndex < 0)
            {
                MessageBox.Show("Lutfen Alanı veya Adeti uygun biçimde giriniz.");
            }

            else
            {
                string iceceksql = "Select Fiyat from Yemekler where KategoriID=5 and YemekID=" + cmb_icecek.SelectedValue;
                DataTable tbl = Data.select(iceceksql);
                int fiyat = Convert.ToInt32(tbl.Rows[0]["Fiyat"]);
                decimal islem = fiyat * nmr_icecek.Value;

                string[] row = { cmb_icecek.Text, nmr_icecek.Value.ToString(), islem.ToString(),cmb_icecek.SelectedValue.ToString() };
                var satir = new ListViewItem(row);
                listView1.Items.Add(satir);
                ToplamFiyat(islem);
            }
        }
        
        public void ToplamFiyat(decimal fiyat)
        {
            toplam = toplam + fiyat;
            label8.Text = toplam.ToString();
        }
        decimal cikar=0;
        public void FiyatCikar(decimal fiyat,decimal fiyat2)
        {
            try
            {
                cikar = fiyat - fiyat2;
                label8.Text = cikar.ToString();
            }
            catch (Exception)
            {

                return;
            }
            
        }
        decimal secilen;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
               secilen=Convert.ToDecimal(listView1.SelectedItems[0].SubItems[2].Text);
                

            }
        }

        string siparisdetayekle;
        string siparistoplamekle;
        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count<=0)
            {
                MessageBox.Show("Lutfen sepetinize yemek ekleyin.");
                return;
            }
            siparistoplamekle = "INSERT INTO SiparisToplam(KullaniciID,LokantaID,ToplamFiyat,AktifMi) VALUES('" + kullaniciid + "','" + lokantaid + "','" + Convert.ToDecimal(label8.Text) + "','1')SELECT SCOPE_IDENTITY();";
            string id=Data.execScalar(siparistoplamekle);

            for (int i = 0; i < listView1.Items.Count; i++)
            {
               

                siparisdetayekle = "INSERT INTO SiparisDetay(KullaniciID,LokantaID,YemekID,Adet,AktifMi,SiparisToplamID) VALUES('" + kullaniciid + "','" + lokantaid +  "','" + listView1.Items[i].SubItems[3].Text + "','" + listView1.Items[i].SubItems[1].Text + "','1','"+id+"')";
                Data.ExecSql(siparisdetayekle);
            }

            MessageBox.Show("Siparişiniz Onaylandı.En Kısa Surede Adresinize Gelicek✌");
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        decimal a = 0;
        private void button7_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.Items.RemoveAt(listView1.SelectedItems[0].Index);
                FiyatCikar(Convert.ToDecimal(label8.Text),secilen);
                toplam = toplam - secilen;

            }
            else
            {
                return;
            }
               
        }
    }
}
