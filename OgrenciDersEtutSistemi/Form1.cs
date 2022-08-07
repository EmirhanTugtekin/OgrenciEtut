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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-6TUVH6H;Initial Catalog=EtutTest;Integrated Security=True");

        void dersListesi()
        {
            
            SqlDataAdapter sda = new SqlDataAdapter("select * from tbldersler", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbDers.ValueMember = "DERSID";
            cmbDers.DisplayMember = "DERSAD";
            cmbDers.DataSource = dt;
            
        }

        void etutListesi()
        {
            SqlDataAdapter sda = new SqlDataAdapter("execute etut", connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dersListesi();
            etutListesi();
        }

        private void cmbDers_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            SqlDataAdapter sda=new SqlDataAdapter("select * from tblogretmen where BRANSID="+cmbDers.SelectedValue,connection);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            cmbOgretmen.ValueMember = "OGRTID";
            cmbOgretmen.DisplayMember = "AD";
            cmbOgretmen.DataSource = dt;
            
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("insert into tbletut (DERSID,OGRETMENID,TARIH,SAAT) values (@p1,@p2,@p3,@p4)",connection);
            command.Parameters.AddWithValue("@p1", cmbDers.SelectedValue);
            command.Parameters.AddWithValue("@p2", cmbOgretmen.SelectedValue);
            command.Parameters.AddWithValue("@p3", mskTarih.Text);
            command.Parameters.AddWithValue("@p4", mskSaat.Text);
            
            

            command.ExecuteNonQuery();
            MessageBox.Show("etüt eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            etutListesi();
            connection.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            txtEtutId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnEtutVer_Click(object sender, EventArgs e)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("update tbletut set OGRENCIID=@p1,durum=@p2 where ID=@p3", connection);
            command.Parameters.AddWithValue("@p1", txtOgrenci.Text);
            command.Parameters.AddWithValue("@p2", "true");
            command.Parameters.AddWithValue("@p3", txtEtutId.Text);

            command.ExecuteNonQuery();
            MessageBox.Show("etüt öğrenciye verildi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            connection.Close();
        }

        private void btnOgrenciEkle_Click(object sender, EventArgs e)
        {
            FrmOgrenciEkle frmOgrenciEkle = new FrmOgrenciEkle();
            frmOgrenciEkle.Show();
            this.Hide();
        }

        private void btnOgretmenMenu_Click(object sender, EventArgs e)
        {
            FrmOgretmen frmOgretmen = new FrmOgretmen();
            frmOgretmen.Show();
            this.Hide();
        }
    }
}
