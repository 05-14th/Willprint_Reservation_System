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
        }

        private void InsertMissingIDs(MySqlConnection connection)
        {
            for (int i = 1; i <= 1000000; i++) 
            {
                string id = "a" + i;

                bool idExists = CheckIDExists(connection, id);

                if (!idExists)
                {
                    InsertID(connection, id, soCusId.Text, dateTimePicker1.Value, int.Parse(idNum.Text));
                    break;
                }
            }
        }

        private bool CheckIDExists(MySqlConnection connection, string id)
        {
            string query = "SELECT COUNT(*) FROM sales_order WHERE sales_order_id = @id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);

            int count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }

        private void InsertID(MySqlConnection connection, string soid, string soCusId, DateTime soDate, int soPsId)
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
    }
}
