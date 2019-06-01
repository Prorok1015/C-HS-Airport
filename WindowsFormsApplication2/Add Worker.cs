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
    public partial class Add_Worker : UserControl
    {
        MySql.Data.MySqlClient.MySqlConnection conn;
        string sex = "1";
        List<string> postID = new List<string>();
        List<string> workerID = new List<string>();
        
        public Add_Worker()
        {
            InitializeComponent();
            string connString = "Server=l228-teacher; Database = 16063_airport; port=3306; user=student; password = student;";
            conn = new MySql.Data.MySqlClient.MySqlConnection(connString);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                label2.ForeColor = Color.Red;
                
            }else{
                label2.ForeColor = Color.Black;
               
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                label3.ForeColor = Color.Red;
               
            }
            else
            {
                label3.ForeColor = Color.Black;
                
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                label4.ForeColor = Color.Red;
               
            }
            else
            {
                label4.ForeColor = Color.Black;
                
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                Label5.ForeColor = Color.Red;
               
            }
            else
            {
                Label5.ForeColor = Color.Black;
                
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text == "")
            {
               label6.ForeColor = Color.Red;
                
            }
            else
            {
                label6.ForeColor = Color.Black;
                
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            sex = "1";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            sex = "2";
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
                
        }

        private void Add_Worker_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
                

                    MySqlCommand cmd = new MySqlCommand("inset into user_ (login_user,password_user, Worker_idWorker) values (\"" + textBox6.Text + "\", \"" + textBox7.Text + "\", \"" + workerID[comboBox2.SelectedIndex] + "\")",conn);
                   
                    int error = cmd.ExecuteNonQuery();
                    if (error == 0)
                    {
                        label13.Text = "Error insert user";
                    }
                
                    
            
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
                postID.Clear();
                while (reader.Read())
                {
                    postID.Add(reader[0].ToString());
                    comboBox1.Items.Add(reader[1].ToString());
                }
            }

            conn.Close();
        }
        private void LoadWorker()
        {
            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand("select worker_.idWorker_, name_worker from worker_", conn);
            MySqlDataReader reader2 = cmd1.ExecuteReader();

            if (reader2.HasRows)
            {
                comboBox2.Items.Clear();
                workerID.Clear();
                while (reader2.Read())
                {
                    workerID.Add(reader2[0].ToString());
                    comboBox2.Items.Add(reader2[1].ToString());
                }
            }

            conn.Close();
        }

        private void Add_Worker_Enter(object sender, EventArgs e)
        {

            LoadPost();
            LoadWorker();
            
        }


        private void Click_worker(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand("insert into worker_ (name_worker, lastname_worker, patronymic_worker, date_born_worker, adress_worker, Phone_namber_worker, Pasport_data_worker, sex_worker_idsex_worker, Post__idPost_) values (\"" + textBox1.Text + "\",\"" + textBox2.Text + "\",\"" + textBox3.Text + "\",\"" + dateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "\",\"" + textBox4.Text + "\",\"" + textBox5.Text + "\",\"" + textBox6.Text + "\", \"" + sex + "\", \"" + postID[comboBox1.SelectedIndex] + "\");", conn);
            cmd1.ExecuteNonQuery();
            conn.Close();
            LoadPost();
            LoadWorker();
        }
    }
}
