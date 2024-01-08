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
    public partial class payment : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        public payment()
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

                    string query = "SELECT customer_id, name FROM customers";
                    MySqlCommand command = new MySqlCommand(query, connection);
                

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        customerID.Items.Clear(); 

                        while (reader.Read())
                        {
                            string valueToAdd = reader.GetString(0);
                            customerID.Items.Add(valueToAdd); 
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
            for (int i = 1; i <= 1000000; i++)
            {
                string id = "p" + i;

                bool idExists = CheckIDExists(connection, id);

                if (!idExists)
                {
                    InsertID(connection, id, long.Parse(paymentAmount.Text), paymentDate.Value, int.Parse(customerID.Text));
                    break;
                }
            }
        }

        private bool CheckIDExists(MySqlConnection connection, string id)
        {
            string query = "SELECT COUNT(*) FROM payment WHERE payment_id = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        private void InsertID(MySqlConnection connection, string paymentID, long amount, DateTime paymentDate, int cusId)
        {
            try
            {
                string query = "INSERT INTO payment VALUES (@param1, @param2, @param3, @param4)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
              
                    command.Parameters.AddWithValue("@param1", paymentID);
                    command.Parameters.AddWithValue("@param2", amount);
                    command.Parameters.AddWithValue("@param3", paymentDate);
                    command.Parameters.AddWithValue("@param4", cusId);

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string text = paymentAmount.Text;

            if (!string.IsNullOrEmpty(text) && !text.All(char.IsDigit))
            {
                paymentAmount.Text = string.Join("", text.Where(char.IsDigit));
                paymentAmount.SelectionStart = paymentAmount.Text.Length;
            }
        }

        private void userView_Click(object sender, EventArgs e)
        {

        }

        private void customerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string userSelected = customerID.Text;
                    string query = $"SELECT name FROM customers WHERE customer_id = {userSelected}";

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
    }
}
