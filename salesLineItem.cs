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
    public partial class salesLineItem : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        public salesLineItem()
        {
            InitializeComponent();
            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT sales_order_id FROM sales_order";
                    MySqlCommand command = new MySqlCommand(query, connection);


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        salesOrderID.Items.Clear();

                        while (reader.Read())
                        {
                            string valueToAdd = reader.GetString(0);
                            salesOrderID.Items.Add(valueToAdd);
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
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO sales_line_item(sales_order_id, quantity) VALUES (@soi, @quantity)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@soi", salesOrderID.Text);
                        command.Parameters.AddWithValue("@quantity", quantity.Text);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
