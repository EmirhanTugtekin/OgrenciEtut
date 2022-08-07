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

namespace OgrenciDersEtutSistemi
{
    public partial class FrmOgretmen : Form
    {
        public FrmOgretmen()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6TUVH6H;Initial Catalog=EtutTest;Integrated Security=True");
        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm1 = new Form1();
            frm1.Show();
        }

        void ogretmenListesi()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from TBLOGRETMEN", connection);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        void dersListesi()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select * from tbldersler", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbBrans.ValueMember = "DERSID";
            cmbBrans.DisplayMember = "DERSAD";
            cmbBrans.DataSource = dt;
        }

        private void btnOgretmenEkle_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("insert into TBLOGRETMEN (AD,SOYAD,BRANSID) values (@p1,@p2,@p3)", connection);
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", cmbBrans.SelectedValue);
            

            /*string query ="select OGRID from TBLOGRENCI where AD='"+txtAd.Text.Trim()+"'";

            SqlDataAdapter sda = new SqlDataAdapter(query, connection);

            DataTable dt = new DataTable();

            sda.Fill(dt);*/

            command.ExecuteNonQuery();
            MessageBox.Show("öğretmen eklendi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ogretmenListesi();

            connection.Close();
        }

        private void FrmOgretmen_Load(object sender, EventArgs e)
        {
            ogretmenListesi();
            dersListesi();
        }
    }
    
}
