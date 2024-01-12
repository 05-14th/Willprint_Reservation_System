using Google.Protobuf.WellKnownTypes;
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
    public partial class orderLineItem : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        public orderLineItem()
        {
            InitializeComponent();
            PopulateComboBox();
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateComboBox()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT name FROM inventory";
                    string poQuery = "SELECT purchase_order_id FROM purchase_order";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlCommand poCommand = new MySqlCommand(poQuery, connection);


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        productName.Items.Clear();

                        while (reader.Read())
                        {
                            string valueToAdd = reader.GetString(0);
                            productName.Items.Add(valueToAdd);
                        }
                    }

                    using (MySqlDataReader reader = poCommand.ExecuteReader())
                    {
                        purchaseOrder.Items.Clear();

                        while (reader.Read())
                        {
                            string valueToAdd = reader.GetString(0);
                            purchaseOrder.Items.Add(valueToAdd);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string value1 = productName.Text;
            string value2 = prodQuantity.Text;
            string value3 = price.Text;
            string value4 = purchaseOrder.Text;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {

                    connection.Open();
                    string query = $"SELECT product_id FROM inventory WHERE name = '{value1}'";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        string[] row = { result.ToString(), value2, value3, value4 };
                        dataGridView1.Rows.Add(row);
                    }
                    else
                    {
                        MessageBox.Show("No Data Found", "Null Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

            private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string text = price.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                price.Text = string.Join("", text.Where(char.IsDigit));
                price.SelectionStart = price.Text.Length;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string text = prodQuantity.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                prodQuantity.Text = string.Join("", text.Where(char.IsDigit));
                prodQuantity.SelectionStart = prodQuantity.Text.Length;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string value1 = row.Cells["product"].Value.ToString();
                        string value2 = row.Cells["quantity"].Value.ToString();
                        string value3 = row.Cells["Budget"].Value.ToString();
                        string value4 = row.Cells["poid"].Value.ToString();
                        
                        string insertQuery = $"INSERT INTO order_line_item(product_id,quantity,price,purchase_order_id) VALUES ('{value1}', '{value2}', '{value3}', '{value4}')";
                        MySqlCommand command = new MySqlCommand(insertQuery, connection);

                        try
                        {
                            command.ExecuteNonQuery();
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
                connection.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                e.CellStyle.ForeColor = Color.Black;
            }
        }

        private void productName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value1 = productName.Text;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = $"SELECT product_id FROM inventory WHERE name = '{value1}'";
                    MySqlCommand command = new MySqlCommand(query, connection);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        prodID.Text = result.ToString();
                    }
                    else
                    {
                        MessageBox.Show("No Data Found", "Null Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
