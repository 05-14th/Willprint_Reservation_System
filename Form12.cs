using System;
using System.Collections;
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
    public partial class Form12 : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        public int StateFromForm1 { get; set; }
        public Form12()
        {
            InitializeComponent();
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
                string deleteQuery = null;

                // Specify your DELETE SQL command
                if (StateFromForm1 == 1)
                {
                    deleteQuery = $"DELETE FROM clients WHERE client_id = {textBox1.Text}";
                }
                else if (StateFromForm1 == 2)
                {
                    deleteQuery = $"DELETE FROM employee WHERE employee_id = '{textBox1.Text}'";
                }
                else if (StateFromForm1 == 3)
                {
                    deleteQuery = $"DELETE FROM items WHERE item_no = {textBox1.Text}";
                }
                else if (StateFromForm1 == 4)
                {
                    deleteQuery = $"DELETE FROM supplier WHERE supplier_id = {textBox1.Text}";
                }
                else if (StateFromForm1 == 5)
                {
                    deleteQuery = $"DELETE FROM purchase_request WHERE pr_number = '{textBox1.Text}'";
                }
                else if (StateFromForm1 == 6)
                {
                    deleteQuery = $"DELETE FROM purchase_order WHERE po_number = '{textBox1.Text}'";
                }

                MySqlCommand command = new MySqlCommand(deleteQuery, connection);

                // Execute the DELETE command
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Data deleted successfully!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No data deleted.");
                }
            }
        }
    }
}
