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
    public partial class YorumYap : Form
    {
        public YorumYap()
        {
            InitializeComponent();
        }
        public string lokantaid;
        public int kullaniciid;
        private void YorumYap_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO Yorumlar(LokantaID,KullaniciID,Yorum,AktifMi) VALUES('" + lokantaid+"','"+kullaniciid+"','"+textBox1.Text+"','0')";
            Data.ExecSql(sql);
            MessageBox.Show("Yorumunuz İçin Teşekkurler :)");
            this.Hide();
        }
    }
}
