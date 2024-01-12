using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Willprint_Reservation_System
{
    public partial class employee : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        public employee()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string text = empCon.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                empCon.Text = string.Join("", text.Where(char.IsDigit));
                empCon.SelectionStart = empCon.Text.Length;
            }
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

                    string query = "INSERT INTO employee(name,position,contact) VALUES (@empName, @empPos,@empContact)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@empName", empName.Text);
                        command.Parameters.AddWithValue("@empPos", empPos.Text);
                        command.Parameters.AddWithValue("@empContact", FormatPhoneNumber(empCon.Text));

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
    }
    
}
