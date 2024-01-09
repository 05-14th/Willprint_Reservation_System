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
    public partial class purchase_order : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        public purchase_order()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string formatDate()
        {
            DateTime selectedDate = poDateSelector.Value; 
            string formattedDate = selectedDate.ToString("MMddyy");

            return formattedDate;
        }

        private void InsertMissingIDs(MySqlConnection connection)
        {
            
            for (int i = 1; i <= 1000000; i++)
            {
                Login login = new Login();
                string id = "PO-" + formatDate() + "-" + i;

                bool idExists = CheckIDExists(connection, id);

                if (!idExists)
                {
                    InsertID(connection, id, poDateSelector.Value, poStatus.Text, login.loginUser);
                    break;
                }
            }
        }

        private bool CheckIDExists(MySqlConnection connection, string id)
        {
            string query = "SELECT COUNT(*) FROM purchase_order WHERE purchase_order_id = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        private void InsertID(MySqlConnection connection, string poID, DateTime poDate, string poStatus, int empId)
        {
            try
            {
                string query = "INSERT INTO purchase_order VALUES (@param1, @param2, @param3, @param4)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@param1", poID);
                    command.Parameters.AddWithValue("@param2", poDate);
                    command.Parameters.AddWithValue("@param3", poStatus);
                    command.Parameters.AddWithValue("@param4", empId);

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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
