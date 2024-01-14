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
    public partial class Delete : Form
    {
        private static string connectionString = main.connectionString;
        public Delete()
        {
            InitializeComponent();
        }

        private void cancelDel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exeDeal_Click(object sender, EventArgs e)
        {
            string tableName = null;
            string idPlaceholder = null;
            string condition = idBox.Text;
            try
            {
                using(MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    switch (main.state)
                    {
                        case 1:
                            tableName = "customers";
                            idPlaceholder = "customer_id";
                            break;
                        case 2:
                            tableName = "employee";
                            idPlaceholder = "employee_id";
                            break;
                        case 3:
                            tableName = "sales_order";
                            idPlaceholder = "sales_order_id";
                            break;
                        case 4:
                            tableName = "payment";
                            idPlaceholder = "payment_id";
                            break;
                        case 5:
                            tableName = "purchase_order";
                            idPlaceholder = "purchase_order_id";
                            break;
                        case 6:
                            tableName = "inventory";
                            idPlaceholder = "product_id";
                            break;
                        case 7:
                            tableName = "order_line_item";
                            idPlaceholder = "oli_id";
                            break;
                        case 8:
                            tableName = "sales_line_item";
                            idPlaceholder = "sli_id";
                            break;
                        case 9:
                            tableName = "product_and_services";
                            idPlaceholder = "ps_id";
                            break;
                    }
                    string query = $"DELETE FROM {tableName} WHERE {idPlaceholder} = '{condition}'";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Record not found or deletion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
