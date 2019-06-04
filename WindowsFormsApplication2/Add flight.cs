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
    public partial class Add_flight : UserControl
    {
        MySqlConnection conn;
        List<string> idcrew = new List<string>();
        List<string> idaircraft = new List<string>();

        public Add_flight()
        {
            InitializeComponent();
            string connString = "Server=localhost; Database = 16063_airport; port=3306; user=root; password = 0000;";
            conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
        }

        private void LoadCrew()
        {
            conn.Open();
            string sql = "SELECT aircraft_.idAircraft_, aircraft_.bort_number, crew_.idCrew_ from aircraft_, crew_ where (crew_.Aircraft__idAircraft_=aircraft_.idAircraft_);";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                comboBox1.Items.Clear();
                idaircraft.Clear();
                idcrew.Clear();
                while (reader.Read())
                {
                    idaircraft.Add(reader[0].ToString());
                    comboBox1.Items.Add(reader[1].ToString());
                    idcrew.Add(reader[2].ToString());
                }
            }

            conn.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO `16063_airport`.`flight_` (`time_flying`, `city_out`, `city_in`, `Crew__Aircraft__idAircraft_`, `Crew__idCrew_`) VALUES ('"+ textBox1.Text +"', '"+textBox2.Text+"', '"+textBox3.Text+"', '"+idaircraft[comboBox1.SelectedIndex]+"', '"+idcrew[comboBox1.SelectedIndex]+"');", conn);
            cmd.ExecuteNonQuery();

            string sql = "select LAST_INSERT_ID(), aircraft_.number_of_seats from aircraft_ where aircraft_.idAircraft_ = " + idaircraft[comboBox1.SelectedIndex];
            cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader1 = cmd.ExecuteReader();
            string id = null;
            int count = 0;
            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    id = reader1[0].ToString();
                    count = int.Parse(reader1[1].ToString());
                }
            }
            conn.Close();
            if (id != null && count != 0)
            {
                
                for (int i = 1; i <= count; ++i)
                {
                    conn.Open();
                    cmd = new MySqlCommand("INSERT INTO `16063_airport`.`flight__has_ticket_` (`Flight__idFlight_`, `place`, `price`) VALUES ('" + id + "', '" + i + "', '" + textBox4.Text + "');", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                
            }
        }

        private void Add_flight_Load(object sender, EventArgs e)
        {
            LoadCrew();
        }

        private void Add_flight_Enter(object sender, EventArgs e)
        {
            LoadCrew();
        }
    }
}
