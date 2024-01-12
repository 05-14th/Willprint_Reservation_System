using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Willprint_Reservation_System
{
    public partial class main : Form
    {
        public static string connectionString = "server=localhost;database=willprint;user=root;password=";
        public static int state = 1;
       
        public main()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
            InitializeUI();
        }

        private void InitializeUI()
        {
           
            search.Dock = DockStyle.Top;
            insert.Dock = DockStyle.Top;
            update.Dock = DockStyle.Top;
            delete.Dock = DockStyle.Top;
            generate.Dock = DockStyle.Top;
            logout.Dock = DockStyle.Top;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            state = 6;
            LoadDataIntoDataGridView();
             
        }

        private void button8_Click(object sender, EventArgs e)
        {
            state = 2;
            LoadDataIntoDataGridView();
             
        }

        private void button9_Click(object sender, EventArgs e)
        {
            state = 3;
            LoadDataIntoDataGridView();
             
        }

        private void button10_Click(object sender, EventArgs e)
        {
            state = 4;
            LoadDataIntoDataGridView();
             
        }

        private void button11_Click(object sender, EventArgs e)
        {
            state = 5;
            LoadDataIntoDataGridView();
             
        }

        private void button7_Click(object sender, EventArgs e)
        {
            state = 1;
            LoadDataIntoDataGridView();
             
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void LoadDataIntoDataGridView()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = null;

                    // Query to fetch data from a table (Replace TableName with your table name)
                    if (state == 1)
                    {
                        query = "SELECT * FROM customers";
                    }
                    else if(state == 2)
                    {
                        query = "SELECT * FROM employee";
                    }
                    else if (state == 3)
                    {
                        query = "SELECT * FROM sales_order";
                    }
                    else if (state == 4)
                    {
                        query = "SELECT * FROM payment";
                    }
                    else if (state == 5)
                    {
                        query = "SELECT * FROM purchase_order";
                    }
                    else if (state == 6)
                    {
                        query = "SELECT * FROM inventory";
                    }
                    else if (state == 7)
                    {
                        query = "SELECT * FROM order_line_item";
                    }
                    else if (state == 8)
                    {
                        query = "SELECT * FROM sales_line_item";
                    }
                    else if (state == 9)
                    {
                        query = "SELECT * FROM product_and_services";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the data to the DataGridView
                    dataGridView1.DataSource = null; 
                    dataGridView1.DataSource = dataTable;
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        // This assumes you have a DataGridView named dataGridView1
        // You need to handle inserts, updates, and deletes separately

        // For updating changes
        private void UpdateChangesToDatabase(string db)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT * FROM {db}", connection);
                    MySqlCommandBuilder commandBuilder = new MySqlCommandBuilder(adapter);

                    DataTable changes = ((DataTable)dataGridView1.DataSource).GetChanges();

                    if (changes != null)
                    {
                        adapter.Update(changes);
                        ((DataTable)dataGridView1.DataSource).AcceptChanges();
                        MessageBox.Show("Changes saved to the database.");
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void button13_Click(object sender, EventArgs e)
        {
            state = 7;
            LoadDataIntoDataGridView();
             
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            state = 8;
            LoadDataIntoDataGridView();
             
        }

        private void button15_Click(object sender, EventArgs e)
        {
            state = 9;
            LoadDataIntoDataGridView();
             
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Query to fetch data from a table (Replace TableName with your table name)
                        if (state == 1)
                        {
                            customers form3 = new customers();
                            form3.ShowDialog();
                             
                        }
                        else if (state == 2)
                        {
                            employee form4 = new employee();
                            form4.ShowDialog();
                             
                        }
                        else if (state == 3)
                        {
                            salerOrder form5 = new salerOrder();
                            form5.ShowDialog();
                             
                        }
                        else if (state == 4)
                        {
                            payment form6 = new payment();
                            form6.ShowDialog();
                             
                        }
                        else if (state == 5)
                        {
                            purchase_order form7 = new purchase_order();
                            form7.ShowDialog();
                             
                        }
                        else if (state == 6)
                        {
                            inventory form11 = new inventory();
                            form11.ShowDialog();
                             
                        }
                        else if (state == 7)
                        {
                            orderLineItem form8 = new orderLineItem();
                            form8.ShowDialog();
                             
                        }
                        else if (state == 8)
                        {
                            salesLineItem form9 = new salesLineItem();
                            form9.ShowDialog();
                             
                        }
                        else if (state == 9)
                        {
                            products form10 = new products();
                            form10.ShowDialog();
                             
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void refreshTable()
        {
            DataTable dataTable = new DataTable();
            dataGridView1.DataSource = null;
            LoadDataIntoDataGridView();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(state == 1)
            {
                UpdateChangesToDatabase("customers");
                refreshTable();
            }
            else if (state == 2)
            {
                UpdateChangesToDatabase("employee");
                refreshTable();
            }
            else if (state == 3)
            {
                UpdateChangesToDatabase("sales_order");
                refreshTable();
            }
            else if (state == 4)
            {
                UpdateChangesToDatabase("payment");
                refreshTable();
            }
            else if (state == 5)
            {
                UpdateChangesToDatabase("purchase_order");
                refreshTable();
            }
            else if (state == 6)
            {
                UpdateChangesToDatabase("inventory");
                refreshTable();
            }
            else if (state == 7)
            {
                UpdateChangesToDatabase("order_line_item");
                refreshTable();
            }
            else if (state == 8)
            {
                UpdateChangesToDatabase("sales_line_item");
                refreshTable();
            }
            else if (state == 9)
            {
                UpdateChangesToDatabase("product_and_services");
                refreshTable();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (state == 1)
            {
                customers customerUi = new customers();
                customerUi.ShowDialog();
                LoadDataIntoDataGridView();
            }
            else if (state == 2)
            {
                employee employeeUi = new employee();
                employeeUi.ShowDialog();
                LoadDataIntoDataGridView();
            }
            else if (state == 3)
            {
                salerOrder salesOrderUi = new salerOrder();
                salesOrderUi.ShowDialog();
                LoadDataIntoDataGridView();
            }
            else if (state == 4)
            {
                payment paymentUi = new payment();
                paymentUi.ShowDialog();
                LoadDataIntoDataGridView();
            }
            else if (state == 5)
            {
                purchase_order poUi = new purchase_order();
                poUi.ShowDialog();
                LoadDataIntoDataGridView();
            }
            else if (state == 6)
            {
                inventory inventoryUi = new inventory();
                inventoryUi.ShowDialog();
                LoadDataIntoDataGridView();
            }
            else if (state == 7)
            {
                orderLineItem oliUi = new orderLineItem();
                oliUi.ShowDialog();
                LoadDataIntoDataGridView();
            }
            else if (state == 8)
            {
                salesLineItem sliUi = new salesLineItem();
                sliUi.ShowDialog();
                LoadDataIntoDataGridView();
            }
            else if (state == 9)
            {
                products productsUi = new products();
                productsUi.ShowDialog();
                LoadDataIntoDataGridView();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
           
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.Close();
            Login loginUi = new Login();
            loginUi.Show();
        }

        private void search_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string keyword = searchbar.Text;
                try
                {
                    connection.Open();

                    string query = null;

                    if (state == 1)
                    {
                        query = $"SELECT * FROM customers WHERE name LIKE @keyword";
                    }
                    else if (state == 2)
                    {
                        query = $"SELECT * FROM employee WHERE name LIKE @keyword";
                    }
                    else if (state == 3)
                    {
                        query = $"SELECT * FROM sales_order WHERE customer_id LIKE @keyword";
                    }
                    else if (state == 4)
                    {
                        query = $"SELECT * FROM payment WHERE customer_id LIKE @keyword OR amount LIKE @keyword";
                    }
                    else if (state == 5)
                    {
                        query = $"SELECT * FROM purchase_order WHERE employee_id LIKE @keyword";
                    }
                    else if (state == 6)
                    {
                        query = $"SELECT * FROM inventory WHERE name LIKE @keyword";
                    }
                    else if (state == 7)
                    {
                        query = $"SELECT * FROM order_line_item WHERE product_id LIKE @keyword OR purchase_order_id LIKE @keyword";
                    }
                    else if (state == 8)
                    {
                        query = $"SELECT * FROM sales_line_item WHERE sales_order_id LIKE @keyword OR quantity LIKE @keyword";
                    }
                    else if (state == 9)
                    {
                        query = $"SELECT * FROM product_and_services WHERE pas LIKE @keyword OR price LIKE @keyword";
                    }

                    if (!string.IsNullOrEmpty(query))
                    {
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@keyword", $"%{keyword}%");

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the data to the DataGridView
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dataTable;
                    }

                    searchbar.Text = "";
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
