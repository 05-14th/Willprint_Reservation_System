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

namespace Willprint_Reservation_System
{
    public partial class salerOrder : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        public salerOrder()
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

                    string query = "SELECT name FROM customers";
                    string productQuery = "SELECT pas FROM product_and_services";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlCommand pcommand = new MySqlCommand(productQuery, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        soCusID.Items.Clear();

                        while (reader.Read())
                        {
                            string valueToAdd = reader.GetString(0);
                            soCusID.Items.Add(valueToAdd);
                        }
                    }

                    using (MySqlDataReader reader = pcommand.ExecuteReader())
                    {
                        idNum.Items.Clear();

                        while (reader.Read())
                        {
                            string valueToAdd = reader.GetString(0);
                            idNum.Items.Add(valueToAdd);
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

        private void InsertMissingIDs(MySqlConnection connection)
        {
            for (long i = 1; i <= 1000000; i++) 
            {
                string id = "a" + i;

                bool idExists = CheckIDExists(connection, id);

                if (!idExists)
                {
                    InsertID(connection, id, long.Parse(userView.Text), dateTimePicker1.Value, long.Parse(productView.Text));
                    break;
                }
            }
        }

        private bool CheckIDExists(MySqlConnection connection, string id)
        {
            string query = "SELECT COUNT(*) FROM sales_order WHERE sales_order_id = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            long count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        private void InsertID(MySqlConnection connection, string soid, long soCusId, DateTime soDate, long soPsId)
        {
            try
            {
                string query = "INSERT INTO sales_order VALUES (@soid, @soCusId, @soDate, @soPsId)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    // Add parameters and their values
                    command.Parameters.AddWithValue("@soid", soid);
                    command.Parameters.AddWithValue("@soCusId", soCusId);
                    command.Parameters.AddWithValue("@soDate", soDate);
                    command.Parameters.AddWithValue("@soPsId", soPsId);

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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                InsertMissingIDs(connection);
                connection.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void userView_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string userSelected = soCusID.Text;
                    string query = $"SELECT customer_id FROM customers WHERE name = '{userSelected}'";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string userName = reader.GetString(0);
                            userView.Text = userName;
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

        private void idNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string userSelected = idNum.Text;
                    string query = $"SELECT ps_id FROM product_and_services WHERE pas = '{userSelected}'";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productName = reader.GetString(0);
                            productView.Text = productName;
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
