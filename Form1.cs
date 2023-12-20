using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Willprint_Reservation_System
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public Form1()
        {
            InitializeComponent();
            InitializeDB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (CheckLogin(username, password))
            {
                this.Hide();
                Form2 form2 = new Form2();
                form2.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
            }
        }

        private void InitializeDB()
        {
            server = "localhost";
            database = "willprintdb";
            uid = "root";
            password = "";

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            connection = new MySqlConnection(connectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
        }

        private bool CheckLogin(string username, string password)
        {
            bool result = false;

            string query = "SELECT * FROM users WHERE username=@username AND password=@password";

            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                connection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = true;
                }

                reader.Close();
            }

            return result;
        }

    }
}
