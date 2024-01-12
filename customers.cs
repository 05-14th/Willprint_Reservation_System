using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Willprint_Reservation_System
{
    public partial class customers : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        public customers()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string text = cusContact.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                cusContact.Text = string.Join("", text.Where(char.IsDigit));
                cusContact.SelectionStart = cusContact.Text.Length; 
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 11) 
            {
                return $"{phoneNumber.Substring(0, 4)}-{phoneNumber.Substring(4, 3)}-{phoneNumber.Substring(7)}";
            }
            else
            {
                return phoneNumber;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO customers(name, contact) VALUES (@customerName, @customerContact)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@customerName", cusName.Text);
                        command.Parameters.AddWithValue("@customerContact", FormatPhoneNumber(cusContact.Text));

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
