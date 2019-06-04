using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApplication2
{
    public partial class Client : Form
    {
        MySqlConnection conn;
        Point mouseOffset;
        bool isMouseDown = false;
        List<string> idBilet = new List<string>();
        string sqlBilet = "";
        string sqlPrice = "";
        string sqlNum =   "";
        public Client()
        {
            InitializeComponent();

            string connString = "Server=localhost; Database = 16063_airport; port=3306; user=root; password = 0000;";
            conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
        }

        private void Client_Load(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "select aircraft_.bort_number, flight__has_ticket_.place, flight__has_ticket_.price, flight__has_ticket_.idBilet from flight_,flight__has_ticket_, aircraft_ where (flight_.idFlight_ = flight__has_ticket_.Flight__idFlight_ and flight_.Crew__Aircraft__idAircraft_ = aircraft_.idAircraft_ and flight__has_ticket_.Ticket__idMark_ is null);";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = reader[0].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = reader[1].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = reader[2].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = "Купить";
                    idBilet.Add(reader[3].ToString());
                    i++;
                }
            }
            reader.Close();
            conn.Close();

            conn.Open();
            sql = "select aircraft_.bort_number from aircraft_ group by bort_number;";
            cmd = new MySqlCommand(sql, conn);
            reader = cmd.ExecuteReader();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader[0].ToString());                    
                }
            }
            reader.Close();
            conn.Close();

            conn.Open();
            sql = "SELECT price FROM flight__has_ticket_ group by price;";
            cmd = new MySqlCommand(sql, conn);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    comboBox3.Items.Add(reader[0].ToString());
                }
            }
            reader.Close();
            conn.Close();

            conn.Open();
            sql = "SELECT place FROM flight__has_ticket_ group by place;";
            cmd = new MySqlCommand(sql, conn);
            reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader[0].ToString());
                }
            }
            reader.Close();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                dataGridView1.Rows[e.RowIndex].Cells[3].Value = "Куплено";

                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO `16063_airport`.`ticket_` ( `name_`) VALUES ( 'client');", conn);
                cmd.ExecuteNonQuery();
                
                string sql = "select LAST_INSERT_ID();";
                cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader1 = cmd.ExecuteReader();
                string id = null;
                
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        id = reader1[0].ToString();                
                    }
                }

                conn.Close();
                if (id != null)
                {
                    conn.Open();
                    cmd = new MySqlCommand(" UPDATE `16063_airport`.`flight__has_ticket_` SET `Ticket__idMark_` = '" + id + "' WHERE (`idBilet` = '" + idBilet[e.RowIndex] + "');", conn);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X;
                yOffset = -e.Y;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                sqlNum = " and aircraft_.bort_number = " + comboBox1.SelectedItem.ToString();
            }
            else
            {
                comboBox1.Text = "Рейс";
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "select aircraft_.bort_number, flight__has_ticket_.place, flight__has_ticket_.price, flight__has_ticket_.idBilet from flight_,flight__has_ticket_, aircraft_ where (flight_.idFlight_ = flight__has_ticket_.Flight__idFlight_ and flight_.Crew__Aircraft__idAircraft_ = aircraft_.idAircraft_ "+ sqlNum + sqlPrice + sqlBilet +" and flight__has_ticket_.Ticket__idMark_ is null );";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            if (reader.HasRows)
            {
                dataGridView1.Rows.Clear();
                idBilet.Clear();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = reader[0].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = reader[1].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = reader[2].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = "Купить";
                    idBilet.Add(reader[3].ToString());
                    i++;
                }
            }
            reader.Close();
            conn.Close();
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                sqlBilet = " and flight__has_ticket_.place = " + comboBox2.SelectedItem.ToString();
            }
            else
            {
                comboBox2.Text = "Место";
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                sqlPrice = " and flight__has_ticket_.price = " + comboBox3.SelectedItem.ToString();
            }
            else
            {
                comboBox3.Text = "Цена";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            conn.Open();
            string sql = "select aircraft_.bort_number, flight__has_ticket_.place, flight__has_ticket_.price, flight__has_ticket_.idBilet from flight_,flight__has_ticket_, aircraft_ where (flight_.idFlight_ = flight__has_ticket_.Flight__idFlight_ and flight_.Crew__Aircraft__idAircraft_ = aircraft_.idAircraft_ and flight__has_ticket_.Ticket__idMark_ is null);";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            int i = 0;
            if (reader.HasRows)
            {
                dataGridView1.Rows.Clear();
                idBilet.Clear();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = reader[0].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = reader[1].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = reader[2].ToString();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = "Купить";
                    idBilet.Add(reader[3].ToString());
                    i++;
                }
            }

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            reader.Close();
            conn.Close();
        }
    }
}
