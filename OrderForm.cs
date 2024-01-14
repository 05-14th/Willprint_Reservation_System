using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Willprint_Reservation_System
{
    public partial class OrderForm : Form
    {
        public static string connectionString = "server=localhost;database=willprint;user=root;password=";
        private int totalPrice = 0;
        private int cusID = 0;
        public OrderForm()
        {
            InitializeComponent();
            InitializeContents();
            dataGridView1.CellValueChanged += DataGridView_CellValueChanged;
        }

        private void InitializeContents()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT name FROM inventory WHERE stock > 0";
                string newQuery = "SELECT pas FROM product_and_services";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string itemName = reader.GetString(0);
                            checkedListBox1.Items.Add(itemName);
                        }
                    }
                }

                using (MySqlCommand pasCommand = new MySqlCommand(newQuery, connection))
                {
                    using (MySqlDataReader reader = pasCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string itemName = reader.GetString(0);
                            checkedListBox2.Items.Add(itemName);
                        }
                    }
                }

                connection.Close();
            }
        }

        private void UpdatePrice()
        {
            int updatedSum = GetTotalSum(dataGridView1, 2);
            totalPrice = updatedSum;
            totalAmount.Text = totalPrice.ToString();
        }

        private int GetTotalSum(DataGridView dataGridView, int columnIndex)
        {
            int totalSum = 0;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[columnIndex].Value != null && int.TryParse(row.Cells[columnIndex].Value.ToString(), out int cellValue))
                {
                    totalSum += cellValue;
                }
            }

            return totalSum;
        }

        private void AddSelectedItemsToDataGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (var checkedItem in checkedListBox1.CheckedItems)
            {
                dataGridView1.Rows.Add(checkedItem.ToString());
            }
            
            foreach (var checkedItem in checkedListBox2.CheckedItems)
            {
                dataGridView1.Rows.Add(checkedItem.ToString());
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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

        private void confirm_Click(object sender, EventArgs e)
        {
            try
            {
                using(MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string cusQuery = $"INSERT INTO customers(name,contact) VALUES ('{cusName.Text}'. '{cusContact.Text}')";
                    string getCusID = $"SELECT customer_id FROM customers WHERE customer_id = '{cusName.Text}'";
                    string putSalesOrder = $"INSERT INTO sales_order(customer";

                    using (MySqlCommand command = new MySqlCommand(cusQuery, connection))
                    {
                        int result = command.ExecuteNonQuery();

                        if (result <= 0)
                        {
                            MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }

                    using (MySqlCommand command = new MySqlCommand(getCusID, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               cusID = reader.GetInt32(0);
                            }
                        }
                    }

                    using (MySqlCommand command = new MySqlCommand(putSalesOrder, connection))
                    {
                        int result = command.ExecuteNonQuery();

                        if (result <= 0)
                        {
                            MessageBox.Show("Something Went Wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                        else
                        {
                            for (long i = 1; i <= 1000000; i++)
                            {
                                string id = "a" + i;

                                bool idExists = CheckIDExists(connection, id);

                                if (!idExists)
                                {
                                    DateTime currentDate = DateTime.Now;
                                    string formattedDate = currentDate.ToString("yyyy-MM-dd");
                                    //InsertID(connection, id, cusID, formattedDate, long.Parse(productView.Text));
                                    break;
                                }
                            }

                        }
                    }

                    connection.Close();
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Something went wrong. " + ex.Message, "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ComputeAndSetValues(e.RowIndex);
        }
        private void ComputeAndSetValues(int rowIndex)
        {
            UpdatePrice();
            string query = $"SELECT price, stock FROM inventory WHERE name = '{dataGridView1.Rows[rowIndex].Cells[0].Value}'";
            string newQuery = $"SELECT price FROM product_and_services WHERE pas = '{dataGridView1.Rows[rowIndex].Cells[0].Value}'";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int price = reader.GetInt32(0);
                                int stock = reader.GetInt32(1);

                                int value1 = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[1].Value);
                                int result = value1 * price;
                                dataGridView1.Rows[rowIndex].Cells[2].Value = result;
                            }
                        }
                    }

                    using (MySqlCommand command = new MySqlCommand(newQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int price = reader.GetInt32(0);

                                int value1 = Convert.ToInt32(dataGridView1.Rows[rowIndex].Cells[1].Value);
                                int result = value1 * price;
                                dataGridView1.Rows[rowIndex].Cells[2].Value = result;
                            }
                        }


                    }
                    connection.Close();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Something went wrong. " + ex.Message, "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void insertService_Click(object sender, EventArgs e)
        {
            AddSelectedItemsToDataGridView();
            insertService.Enabled = false;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            checkedListBox1.ClearSelected();
            checkedListBox2.ClearSelected();
            totalPrice = 0;
            UpdatePrice();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                checkedListBox2.SetItemChecked(i, false);
            }
            insertService.Enabled = true;
            
        }
    }
}