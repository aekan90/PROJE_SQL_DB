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

namespace PROJE_SQL_DB
{
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-OA1OLIM;Initial Catalog=SatisVT;Integrated Security=True");
        private void BtnListele_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From TBLKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            // TxtKategoriAd dan alınan veri TBLKATEGORI ye EKLENDİ
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("INSERT INTO TBLKATEGORI(KATEGORIAD) VALUES (@p1)", baglanti);
            komut2.Parameters.AddWithValue("p1", TxtKategoriAd.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori Kaydetme İşlemi Başarıyla Gerçekleşti");

            // güncel tablo verisini LİSTELEME yapalım, datagridimiz dinamik görünsün
            SqlCommand komut = new SqlCommand("Select * From TBLKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            // güncel tablo verisi datagride basıldı
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // e : hücrenin değerini tutacak
            TxtKategoriId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtKategoriAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            // KATEGORİ silme işlemi başladı
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("DELETE FROM TBLKATEGORI WHERE KATEGORIID=@p1", baglanti);
            komut3.Parameters.AddWithValue("p1", TxtKategoriId.Text);
            komut3.ExecuteNonQuery();
            // silme işlemi tamamlandı

            // güncel tablo verisini LİSTELEME yapalım
            SqlCommand komut = new SqlCommand("Select * From TBLKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            // güncel tablo verisi datagride basıldı


            baglanti.Close();
            MessageBox.Show("Kategori Silme İşlemi Başarıyla Tamamlandı");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            // KATEGORİ UPDATE işlemi başladı, TxtBox dan alınan veriler ile update yapıldı
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE TBLKATEGORI SET KATEGORIAD=@P1 WHERE KATEGORIID=@P2", baglanti);
            komut.Parameters.AddWithValue("P1", TxtKategoriAd.Text);
            komut.Parameters.AddWithValue("P2", TxtKategoriId.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("Kategori Güncelleme İşlemi Başarıyla Tamamlandı");
            // UPDATE işlemi tamamlandı

            // güncel tablo verisini LİSTELEME yapalım
            SqlCommand komut2 = new SqlCommand("Select * From TBLKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            // güncel tablo verisi datagride basıldı

            baglanti.Close();
        }

        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From TBLKATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}

// Data Source=DESKTOP-OA1OLIM;Initial Catalog=SatisVT;Integrated Security=True
