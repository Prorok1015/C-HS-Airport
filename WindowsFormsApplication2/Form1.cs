using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySql.Data.Common;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        Form forma;
        Point mouseOffset;
        bool isMouseDown = false;
        public Form1()
        {
            InitializeComponent();
            string connString = "Server=localhost; Database = 16063_airport; port=3306; user=root; password = 0000;";
            conn = new MySqlConnection(connString);           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT user_.idUser_, user_.Worker__idWorker_ , post_.Type_post__idType_post_ FROM user_,worker_,post_ where (login_user = \"" + textBox1.Text + "\" and password_user = \"" + textBox2.Text + "\" and user_.Worker__idWorker_ = worker_.idWorker_ and worker_.Post__idPost_ = post_.idPost_);";
            MySqlCommand cmd = new MySqlCommand(sql,conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int id = Convert.ToInt32(reader.GetValue(0));
                        
                        switch(reader[2].ToString()){
                            case "1":forma = new Form2(this, reader[1].ToString());                      
                                 forma.Show();
                                 this.SetVisibleCore(false);
                                break;
                            case "2":
                                break;
                            case "3":
                                break;
                        }
                            
                            
                        
                    }
                }
            reader.Close();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
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

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            forma = new Client();
            forma.Show();
        }
    }
}
