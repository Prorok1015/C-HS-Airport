using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApplication2
{
    public partial class Fint : UserControl
    {
        MySqlConnection conn;
        public Fint()
        {
            InitializeComponent();
            string connString = "Server=localhost; Database = 16063_airport; port=3306; user=root; password = 0000;";
            conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
        }

        private void Fint_Enter(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT * FROM 16063_airport.worker_;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                int i = 0;
                dataGridView1.Rows.Clear();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[i].Cells[0].Value = reader[0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = reader[1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = reader[2].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = reader[3].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = reader[4].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = reader[5].ToString();
                    dataGridView1.Rows[i].Cells[6].Value = reader[6].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = reader[7].ToString();
                    dataGridView1.Rows[i].Cells[8].Value = reader[8].ToString();
                    dataGridView1.Rows[i].Cells[9].Value = reader[9].ToString();
                   // dataGridView1.Rows[i].Cells[10].Value = reader[10].ToString();
                    ++i;
                }
            }

            conn.Close();
        }

        private void Fint_Load(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT * FROM 16063_airport.worker_;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                int i = 0;
                dataGridView1.Rows.Clear();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[i].Cells[0].Value = reader[0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = reader[1].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = reader[2].ToString();
                    dataGridView1.Rows[i].Cells[3].Value = reader[3].ToString();
                    dataGridView1.Rows[i].Cells[4].Value = reader[4].ToString();
                    dataGridView1.Rows[i].Cells[5].Value = reader[5].ToString();
                    dataGridView1.Rows[i].Cells[6].Value = reader[6].ToString();
                    dataGridView1.Rows[i].Cells[7].Value = reader[7].ToString();
                    dataGridView1.Rows[i].Cells[8].Value = reader[8].ToString();
                    dataGridView1.Rows[i].Cells[9].Value = reader[9].ToString();
                    // dataGridView1.Rows[i].Cells[10].Value = reader[10].ToString();
                    ++i;
                }
            }

            conn.Close();
        }
    }
}
