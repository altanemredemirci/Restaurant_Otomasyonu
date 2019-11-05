using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YemekOtomasyonu
{
    public partial class YemekEkle : Form
    {
        public YemekEkle()
        {
            InitializeComponent();
        }
        public int adminid;
        private void YemekEkle_Load(object sender, EventArgs e)
        {
            string sql = "select * from Lokantalar where KullaniciID="+adminid;
            DataTable table = Data.select(sql);
            comboBox2.DataSource = table;
            comboBox2.DisplayMember = "LokantaAdi";
            comboBox2.ValueMember = "LokantaID";
            comboBox2.SelectedIndex = -1;


            string kategorisql = "select * from Kategoriler";
            DataTable kategoritable = Data.select(kategorisql);
            comboBox1.DataSource = kategoritable;
            comboBox1.DisplayMember = "KategoriAdi";
            comboBox1.ValueMember = "KategoriID";
            comboBox1.SelectedIndex = -1;
        }
        string dosyaYolu;
        string dosyaAdi;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dosya = new OpenFileDialog();
            dosya.Filter = "Resim Dosyası |*.jpg;*.nef;*.png ;*.jpeg;*.gif|  Tüm Dosyalar |*.*";
            dosya.ShowDialog();
            dosyaYolu = dosya.FileName;
            textBox3.Text = dosyaYolu;
            pictureBox1.ImageLocation = dosyaYolu;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            string sql = "INSERT INTO Yemekler(YemekAdi,KategoriID,LokantaID,Fiyat) VALUES('" + textBox1.Text + "','" + comboBox1.SelectedValue + "','" + comboBox2.SelectedValue + "','" + textBox2.Text + "')SELECT SCOPE_IDENTITY();";
            string id=Data.execScalar(sql);


            dosyaAdi = Path.GetFileName(dosyaYolu); //Dosya adını alma
            string kaynak = dosyaYolu;
            string hedef = Application.StartupPath + @"\resimler\";
            string yeniad = id +textBox1.Text+".jpg";

            string resimupdate = "Update Yemekler set Resmi='" + yeniad + "' where YemekID=" + id;
            Data.ExecSql(resimupdate);

            File.Copy(kaynak, hedef + yeniad);
            MessageBox.Show("Yemek Eklendi");

        }
    }
}
