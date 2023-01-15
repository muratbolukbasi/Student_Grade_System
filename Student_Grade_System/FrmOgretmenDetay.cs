using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Student_Grade_System
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayitDataSet.TBL_DERS' table. You can move, or remove it, as needed.
            this.tBL_DERSTableAdapter.Fill(this.dbNotKayitDataSet.TBL_DERS);

        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=MURAT\TEW_SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBL_DERS (OGRNUMARA,OGRAD,OGRSOYAD) values (@P1,@P2,@P3)", baglanti);
            komut.Parameters.AddWithValue("@P1", MskNumara.Text);
            komut.Parameters.AddWithValue("@P2", TxtAd.Text);
            komut.Parameters.AddWithValue("@P3", TxtSoyad.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Öğrenci Sisteme Eklendi");

            this.tBL_DERSTableAdapter.Fill(this.dbNotKayitDataSet.TBL_DERS);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
            //CellClick komutu tıklanan içerikleri text te göstermeye yarıyor
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            MskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            TxtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        public void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;           

            s1 = Convert.ToDouble(TxtSinav1.Text);
            s2 = Convert.ToDouble(TxtSinav2.Text);
            s3 = Convert.ToDouble(TxtSinav3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text = ortalama.ToString();

            if (ortalama >= 50 )
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglanti.Open();

            SqlCommand komut2 = new SqlCommand("update TBL_DERS set OGRS1=@P1,OGRS2=@P2,OGRS3=@P3,ORTALAMA=@P4,DURUM=@P5 WHERE OGRNUMARA=@P6", baglanti);
            komut2.Parameters.AddWithValue("@P1",TxtSinav1.Text);
            komut2.Parameters.AddWithValue("@P2",TxtSinav2.Text);
            komut2.Parameters.AddWithValue("@P3",TxtSinav3.Text);
            komut2.Parameters.AddWithValue("@P4",decimal.Parse(LblOrtalama.Text));
            komut2.Parameters.AddWithValue("@P5",durum);
            komut2.Parameters.AddWithValue("@P6",MskNumara.Text);
            komut2.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Öğrenci Notları Güncellendi");

            this.tBL_DERSTableAdapter.Fill(this.dbNotKayitDataSet.TBL_DERS);
        }
    }
}
