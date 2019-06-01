using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApplication2
{
    public partial class Add_aircraft : UserControl
    {
        MySqlConnection conn;
        List<string> idbrand = new List<string>();
        public Add_aircraft()
        {
            InitializeComponent();
            string connString = "Server=l228-teacher; Database = 16063_airport; port=3306; user=student; password = student;";
            conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
        }

        private void LoadBrand()
        {
            conn.Open();
            string sql = "SELECT idBrand_,name_ticket FROM brand_;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                comboBox1.Items.Clear();
                idbrand.Clear();
                while (reader.Read())
                {
                    idbrand.Add(reader[0].ToString());
                    comboBox1.Items.Add(reader[1].ToString());
                    
                }
            }

            conn.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `16063_airport`.`brand_` (`name_ticket`) VALUES ('"+ textBox3.Text +"');", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            LoadBrand();
        }

        private void Add_aircraft_Load(object sender, EventArgs e)
        {
            LoadBrand();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex >=0)
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `16063_airport`.`aircraft_` (`number_of_seats`, `date_coming`, `time_is_use`, `date_last_repair`, `Brand__idBrand`, `bort_number`) VALUES ('" + textBox1.Text + "', '" + dateTimePicker1.Value.Date.ToString("yyyy-mm-dd") + "', '" + textBox2.Text + "', '" + dateTimePicker2.Value.Date.ToString("yyyy-mm-dd") + "', '" + idbrand[comboBox1.SelectedIndex] + "', '" + textBox4.Text + "');", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            LoadBrand();
        }

        private void Add_aircraft_Enter(object sender, EventArgs e)
        {
            LoadBrand();
        }
    }
}
