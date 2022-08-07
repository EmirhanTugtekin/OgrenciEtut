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

namespace OgrenciDersEtutSistemi
{
    public partial class FrmOgrenciEkle : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6TUVH6H;Initial Catalog=EtutTest;Integrated Security=True");
        public FrmOgrenciEkle()
        {
            InitializeComponent();
        }

        void ogrenciListesi()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from TBLOGRENCI",connection);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void btnOgrEkle_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("insert into TBLOGRENCI (AD,SOYAD,SINIF,MAIL) values (@p1,@p2,@p3,@p4)", connection);
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", txtSinif.Text);
            command.Parameters.AddWithValue("@p4", txtMail.Text);


            /*string query ="select OGRID from TBLOGRENCI where AD='"+txtAd.Text.Trim()+"'";

            SqlDataAdapter sda = new SqlDataAdapter(query, connection);

            DataTable dt = new DataTable();

            sda.Fill(dt);*/

            command.ExecuteNonQuery();
            MessageBox.Show("öğrenci eklendi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information) ;

            ogrenciListesi();

            connection.Close();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm1 = new Form1();
            frm1.Show();
        }

        private void FrmOgrenciEkle_Load(object sender, EventArgs e)
        {
            ogrenciListesi();
        }
    }
}
