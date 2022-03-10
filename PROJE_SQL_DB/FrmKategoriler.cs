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
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("INSERT INTO TBLKATEGORI(KATEGORIAD) VALUES (@p1)",baglanti);
            komut2.Parameters.AddWithValue("p1", TxtKategoriAd.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori Kaydetme İşlemi Başarıyla Gerçekleşti");
        }
    }
}

// Data Source=DESKTOP-OA1OLIM;Initial Catalog=SatisVT;Integrated Security=True
