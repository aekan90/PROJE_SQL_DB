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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-OA1OLIM;Initial Catalog=SatisVT;Integrated Security=True");
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // KRİTİK SEVİYEDEKİ ÜRÜNLERİ GETİR
            SqlCommand komut = new SqlCommand("EXEC KRITIK", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            // CHART1'A VERİ EKLEME İŞLEMİ
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand(" EXEC URUNSAYISI_KATEGORIYEGORE", baglanti);
            SqlDataReader dr=  komut1.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Kategoriler"].Points.AddXY(dr[0],dr[1]);
            }
            baglanti.Close();
            
            
            // CHART2'A VERİ EKLEME İŞLEMİ
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand(" EXEC MUSTERISAYISI_SEHIREGORE", baglanti);
            SqlDataReader dr2=  komut2.ExecuteReader();
            while (dr2.Read())
            {
                chart2.Series["Musteriler"].Points.AddXY(dr2[0],dr2[1]);
            }
            baglanti.Close();
        }


        private void BtnKategori_Click(object sender, EventArgs e)
        {
            FrmUrunler fr = new FrmUrunler();
            fr.Show();
        }


        private void BtnMusteri_Click(object sender, EventArgs e)
        {
            FrmMusteri fr2 = new FrmMusteri();
            fr2.Show();
        }
    }
}
