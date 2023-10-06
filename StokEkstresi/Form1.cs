using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokEkstresi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // grid view açılışta gizlendi.
            dataGridView1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SQL Server Connection String
            String constring = "server=.\\SQLEXPRESS;database=Test;Integrated Security=true;";
            using (SqlConnection con = new SqlConnection(constring)) 
            {
                using (SqlCommand cmd = new SqlCommand("EXEC GetStockTransactions @StokKodu,@BaslangicTarihi,@BitisTarihi"))
                {
                    //SQL Adapter oluşturuldu.
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        // Form'dan alınan veriler Sorguya eklendi.
                        cmd.Parameters.AddWithValue("@StokKodu", textBox3.Text);
                        cmd.Parameters.AddWithValue("@BaslangicTarihi", dateTimePicker1.Value.Date.ToShortDateString());
                        cmd.Parameters.AddWithValue("@BitisTarihi", dateTimePicker2.Value.Date.ToShortDateString());
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            // Dönen sonuç grid'e yüklendi.
                            dataGridView1.Show();
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
        }
    }
}
