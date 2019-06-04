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
    public partial class Add_Crew : UserControl
    {

        MySqlConnection conn;
        string sex;
        List<string> idpost = new List<string>();
        List<string> idaircraft = new List<string>();
        List<string> idcrew = new List<string>();
        List<string> idworker = new List<string>();
        public Add_Crew()
        {
            InitializeComponent();

            string connString = "Server=localhost; Database = 16063_airport; port=3306; user=root; password = 0000;";
            conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
        }


        private void LoadPost()
        {
            conn.Open();
            string sql = "select post_.idPost_, post_.name_post from post_;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                comboBox1.Items.Clear();
                idpost.Clear();
                while (reader.Read())
                {
                    idpost.Add(reader[0].ToString());
                    comboBox1.Items.Add(reader[1].ToString());
                }
            }

            conn.Close();
        }
        private void LoadAircraft()
        {
            conn.Open();
            string sql = "SELECT aircraft_.idAircraft_, aircraft_.bort_number from aircraft_";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                comboBox2.Items.Clear();
                idaircraft.Clear();
                while (reader.Read())
                {
                    idaircraft.Add(reader[0].ToString());
                    comboBox2.Items.Add(reader[1].ToString());
                }
            }

            conn.Close();
        }
        private void LoadCrew()
        {
            conn.Open();
            string sql = "SELECT crew_.idCrew_, aircraft_.bort_number from aircraft_, crew_ where (crew_.Aircraft__idAircraft_=aircraft_.idAircraft_);";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                comboBox3.Items.Clear();
                idcrew.Clear();
                while (reader.Read())
                {
                    idcrew.Add(reader[0].ToString());
                    comboBox3.Items.Add(reader[1].ToString());
                }
            }

            conn.Close();
        }
        private void LoadWorker()
        {
            conn.Open();
            string sql = "select worker_.idWorker_, name_worker from worker_";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                comboBox4.Items.Clear();
                idworker.Clear();
                while (reader.Read())
                {
                    idworker.Add(reader[0].ToString());
                    comboBox4.Items.Add(reader[1].ToString());
                }
            }

            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand("insert into worker_ (name_worker, lastname_worker, patronymic_worker, date_born_worker, adress_worker, Phone_namber_worker, Pasport_data_worker, sex_worker_idsex_worker, Post__idPost_) values (\"" + textBox1.Text + "\",\"" + textBox2.Text + "\",\"" + textBox3.Text + "\",\"" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "\",\"" + textBox4.Text + "\",\"" + textBox5.Text + "\",\"" + textBox6.Text + "\", \"" + sex + "\", \"" + idpost[comboBox1.SelectedIndex] + "\");", conn);
            cmd1.ExecuteNonQuery();
            conn.Close();

            conn.Open();
            cmd1 = new MySqlCommand("INSERT INTO `16063_airport`.`crew_` (`idCrew_`, `count_crew`, `date_coming`, `date_out`, `Aircraft__idAircraft_`) VALUES ('" + textBox6.Text + "', '1', '" + dateTimePicker2.Value.Date.ToString("yyyy-MM-dd") + "', '" + dateTimePicker3.Value.ToString("yyyy-MM-dd HH:MM") + "',  '" + idaircraft[comboBox2.SelectedIndex] + "');", conn);
            cmd1.ExecuteNonQuery();
            conn.Close();
          
            LoadCrew();
            LoadPost();
            LoadAircraft();
            LoadWorker();

        }

        private void Add_Crew_Load(object sender, EventArgs e)
        {
            LoadCrew();
            LoadPost();
            LoadAircraft();
            LoadWorker();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand("insert into crew__has_worker_ Values (\""+ comboBox3.SelectedItem.ToString() +"\",\""+ comboBox4.SelectedItem.ToString() +"\");", conn);
            cmd1.ExecuteNonQuery();
            conn.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            sex = "1";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sex = "2";
        }

        private void Add_Crew_Enter(object sender, EventArgs e)
        {
            LoadCrew();
            LoadPost();
            LoadAircraft();
            LoadWorker();
        }
    }
}
