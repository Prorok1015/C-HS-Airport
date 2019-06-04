using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication2
{
    public partial class Form2 : Form
    {
        MySql.Data.MySqlClient.MySqlConnection conDB;
        Form main;
        Point mouseOffset;
        bool isMouseDown = false;

        string idUser;

        public Form2(Form m, string idworker)
        {
            InitializeComponent();
            string connString = "Server=localhost; Database = 16063_airport; port=3306; user=root; password = 0000;";
            conDB = new MySqlConnection(connString);
             
            main = m;

            add_Worker1.BringToFront();

            
            conDB.Open();
            string sql = "select idWorker_, concat(name_worker, \" \", lastname_worker, \" \", patronymic_worker), post_.name_post from worker_, post_ where (idWorker_ = \"" + idworker + "\" and worker_.Post__idPost_ = post_.idPost_);";
            MySqlCommand cmd = new MySqlCommand(sql, conDB);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idUser = reader[0].ToString();
                    label2.Text = reader[1].ToString();
                    label3.Text = reader[2].ToString();
                }
            }

            conDB.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            main.Close();
        }

        private void add_Worker1_Load(object sender, EventArgs e)
        {

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

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if(isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add_Worker1.BringToFront();
            panel4.Location = button1.Location;
            button1.BackColor = Color.FromArgb(200,190,240);
            button2.BackColor = Color.FromArgb(170, 173, 242);
            button3.BackColor = Color.FromArgb(170, 173, 242);
            button4.BackColor = Color.FromArgb(170, 173, 242);
            button5.BackColor = Color.FromArgb(170, 173, 242);
            button6.BackColor = Color.FromArgb(170, 173, 242);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            add_post1.BringToFront();
            panel4.Location = button3.Location;
            button3.BackColor = Color.FromArgb(200, 190, 240);
            button2.BackColor = Color.FromArgb(170, 173, 242);
            button1.BackColor = Color.FromArgb(170, 173, 242);
            button4.BackColor = Color.FromArgb(170, 173, 242);
            button5.BackColor = Color.FromArgb(170, 173, 242);
            button6.BackColor = Color.FromArgb(170, 173, 242);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            add_Crew1.BringToFront();
            panel4.Location = button4.Location;
            button4.BackColor = Color.FromArgb(200, 190, 240);
            button2.BackColor = Color.FromArgb(170, 173, 242);
            button3.BackColor = Color.FromArgb(170, 173, 242);
            button1.BackColor = Color.FromArgb(170, 173, 242);
            button5.BackColor = Color.FromArgb(170, 173, 242);
            button6.BackColor = Color.FromArgb(170, 173, 242);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            add_flight1.BringToFront();
            panel4.Location = button5.Location;
            button5.BackColor = Color.FromArgb(200, 190, 240);
            button2.BackColor = Color.FromArgb(170, 173, 242);
            button3.BackColor = Color.FromArgb(170, 173, 242);
            button4.BackColor = Color.FromArgb(170, 173, 242);
            button1.BackColor = Color.FromArgb(170, 173, 242);
            button6.BackColor = Color.FromArgb(170, 173, 242);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            add_aircraft1.BringToFront();
            panel4.Location = button6.Location;
            button6.BackColor = Color.FromArgb(200, 190, 240);
            button2.BackColor = Color.FromArgb(170, 173, 242);
            button3.BackColor = Color.FromArgb(170, 173, 242);
            button4.BackColor = Color.FromArgb(170, 173, 242);
            button5.BackColor = Color.FromArgb(170, 173, 242);
            button1.BackColor = Color.FromArgb(170, 173, 242);
        }

        private void add_post1_Load(object sender, EventArgs e)
        {

        }

        private void add_flight1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            main.Visible = true;
            this.Close();
        }

        private void workerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fint1.BringToFront();
        }
    }
}
