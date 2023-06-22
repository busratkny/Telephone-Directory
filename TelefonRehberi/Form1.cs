using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace TelefonRehberi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        SqlConnection connection=new SqlConnection("Data Source=\"Bš\u009eRA\\SQLEXPRESS\";Initial Catalog=Rehber;Integrated Security=True");
        SqlDataAdapter da;
        DataTable db = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            da= new SqlDataAdapter("select*from Kisiler", connection);
            connection.Open();
            da.Fill(db);
            dataGridView1.DataSource = db;
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataView dv = db.DefaultView;
            dv.RowFilter = "Ad like'" + textBox1.Text + "%'";
            dataGridView1.DataSource = dv;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int chosen = dataGridView1.SelectedCells[0].RowIndex;
            string name = dataGridView1.Rows[chosen].Cells[1].Value.ToString();
            string telephone = dataGridView1.Rows[chosen].Cells[2].Value.ToString();
            textBox2.Text = name;
            textBox4.Text = telephone;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            connection.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Kisiler(Ad,Telefon) VALUES(@P1,@P3)", connection);
            cmd.Parameters.AddWithValue("@P1",textBox2.Text);
            cmd.Parameters.AddWithValue("@P3",textBox4.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("New record added.");
            connection.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand sql = new SqlCommand("UPDATE Kisiler SET Ad=@p1,Telefon=@p3 WHERE ID=@Id", connection);
            sql.Parameters.AddWithValue("@p1",textBox2.Text);
            sql.Parameters.AddWithValue("@p3", textBox4.Text);
            sql.Parameters.AddWithValue("@Id", dataGridView1.SelectedCells[0].Value);
            sql.ExecuteNonQuery();
            MessageBox.Show("Record has been updated.");
            connection.Close();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand sql = new SqlCommand("DELETE FROM Kisiler WHERE ID=@ıd", connection);
            sql.Parameters.AddWithValue("@ıd", dataGridView1.SelectedCells[0].Value);
            sql.ExecuteNonQuery();
            MessageBox.Show("Record has been deleted.");
            connection.Close();
        }
    }
}
