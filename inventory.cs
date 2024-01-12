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

namespace Willprint_Reservation_System
{
    public partial class inventory : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        public inventory()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string text = inventoryStock.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                inventoryStock.Text = string.Join("", text.Where(char.IsDigit));
                inventoryStock.SelectionStart = inventoryStock.Text.Length;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO inventory(name, description, stock, price) VALUES (@inName, @inDesc, @inStock, @inPrice)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@inName", itemName.Text);
                        command.Parameters.AddWithValue("@inDesc", inventoryDesc.Text);
                        command.Parameters.AddWithValue("@inStock", inventoryStock.Text);
                        command.Parameters.AddWithValue("@inPrice", totalPrice.Text);

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string text = totalPrice.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                totalPrice.Text = string.Join("", text.Where(char.IsDigit));
                totalPrice.SelectionStart = totalPrice.Text.Length;
            }
        }
    }
}
