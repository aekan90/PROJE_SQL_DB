using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJE_SQL_DB
{
    public partial class FrmMusteri : Form
    {
        public FrmMusteri()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-OA1OLIM;Initial Catalog=SatisVT;Integrated Security=True");
       
        void listele()
        {
            SqlCommand komut = new SqlCommand("Select * From TBLMUSTERI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void FrmMusteri_Load(object sender, EventArgs e)
        {
            listele();

            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From TBLSEHIR", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbSehir.Items.Add(dr["SEHIRAD"]);
            }
            baglanti.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtId.Text=dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text=dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text=dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbSehir.Text=dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtBakiye.Text=dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();



        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            // TxtAd dan alınan veri TBLMUSTERI ye EKLENDİ
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("INSERT INTO TBLMUSTERI(MUSTERIAD,MUSTERISOYAD,MUSTERISEHIR,MUSTERIBAKIYE) VALUES (@p1,@p2,@p3,@p4)", baglanti);
            komut2.Parameters.AddWithValue("p1", (TxtAd.Text).ToUpper());
            komut2.Parameters.AddWithValue("p2", (TxtSoyad.Text).ToUpper());
            komut2.Parameters.AddWithValue("p3", (CmbSehir.Text).ToUpper());
            komut2.Parameters.AddWithValue("p4", decimal.Parse(TxtBakiye.Text));
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Musteri Kaydetme İşlemi Başarıyla Gerçekleşti");

            // güncel tablo verisini LİSTELEME yapalım, datagridimiz dinamik görünsün
            listele();
            // güncel tablo verisi datagride basıldı
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            // MUSTERI silme işlemi başladı
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("DELETE FROM TBLMUSTERI WHERE MUSTERIID=@p1", baglanti);
            komut3.Parameters.AddWithValue("p1", TxtId.Text);
            komut3.ExecuteNonQuery();
            // silme işlemi tamamlandı

            // güncel tablo verisini LİSTELEME yapalım
            listele();
            // güncel tablo verisi datagride basıldı


            baglanti.Close();
            MessageBox.Show("Kategori Silme İşlemi Başarıyla Tamamlandı");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            // MÜŞTERİ UPDATE işlemi başladı, TxtBox dan alınan veriler ile update yapıldı
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE TBLMUSTERI SET MUSTERIAD=@P1,MUSTERISOYAD=@P3,MUSTERIBAKIYE=@P4,MUSTERISEHIR=@P5 WHERE MUSTERIID=@P2", baglanti);
            komut.Parameters.AddWithValue("P1", TxtAd.Text);
            komut.Parameters.AddWithValue("P2", TxtId.Text);
            komut.Parameters.AddWithValue("P3", TxtSoyad.Text);
            komut.Parameters.AddWithValue("P4", decimal.Parse(TxtBakiye.Text));
            komut.Parameters.AddWithValue("P5", CmbSehir.Text);
            komut.ExecuteNonQuery();
            MessageBox.Show("MÜŞTERİ Güncelleme İşlemi Başarıyla Tamamlandı");
            // UPDATE işlemi tamamlandı

            // güncel tablo verisini LİSTELEME yapalım
            listele();
            // güncel tablo verisi datagride basıldı

            baglanti.Close();
        }

        private void BtnaAra_Click(object sender, EventArgs e)
        {
            // ARAMA KISMINDA HATA VAR LİKE İLE ARAMA YAPAMADIM
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLMUSTERI WHERE MUSTERIAD LIKE '%@P1%'", baglanti);
            komut.Parameters.AddWithValue("P1", TxtAd.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
