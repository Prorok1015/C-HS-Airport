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
    public partial class Add_post : UserControl
    {
        MySqlConnection conn;
        List<string> idpost= new List<string>();
        List<string> idtype_post= new List<string>();

        private void LoadTypePost()
        {
            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand("select idType_post_, name_type from type_post_;", conn);
            MySqlDataReader reader2 = cmd1.ExecuteReader();

            if (reader2.HasRows)
            {
                comboBox2.Items.Clear();
                idtype_post.Clear();
                while (reader2.Read())
                {
                    idtype_post.Add(reader2[0].ToString());
                    comboBox2.Items.Add(reader2[1].ToString());
                }
            }

            conn.Close();
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

        public Add_post()
        {
            InitializeComponent();
            string connString = "Server=localhost; Database = 16063_airport; port=3306; user=root; password = 0000;";
            conn = new MySql.Data.MySqlClient.MySqlConnection(connString);


        }

        private void Add_post_Load(object sender, EventArgs e)
        {
            LoadTypePost();

            LoadPost();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand cmd1 = new MySqlCommand("insert into post_ (name_post, salary_post, duties_post, Type_post__idType_post_) values (\"" + textBox1.Text + "\",\"" + textBox2.Text + "\",\"" + textBox3.Text + "\", 2);", conn);
            cmd1.ExecuteNonQuery();
            conn.Close();
            LoadPost(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            //string utfLine = "";
            //Encoding utf = Encoding.UTF8.GetString(utfLine);
            string s = textBox4.Text.ToString();
            byte[] text = Encoding.UTF8.GetBytes(s);
            MySqlCommand cmd1 = new MySqlCommand("insert into type_post_ (name_type) values (\""+ Encoding.UTF8.GetString(text) +"\");", conn);  
            cmd1.ExecuteNonQuery();
            conn.Close();
            LoadTypePost();
        }

        private void Add_post_Enter(object sender, EventArgs e)
        {
            LoadPost();
            LoadTypePost();
        }
    }
}
