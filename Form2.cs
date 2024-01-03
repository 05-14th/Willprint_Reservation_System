using System;
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
    public partial class Form2 : Form
    {
        private const string connectionString = "server=localhost;database=willprint;user=root;password=";
        private int state = 1;
        public Form2()
        {
            InitializeComponent();
            InitializeUI();
            LoadDataIntoDataGridView();
        }

        private void InitializeUI()
        {
            panel2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = !panel2.Visible;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            state = 6;
            LoadDataIntoDataGridView();
            panel2.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            state = 2;
            LoadDataIntoDataGridView();
            panel2.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            state = 3;
            LoadDataIntoDataGridView();
            panel2.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            state = 4;
            LoadDataIntoDataGridView();
            panel2.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            state = 5;
            LoadDataIntoDataGridView();
            panel2.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            state = 1;
            LoadDataIntoDataGridView();
            panel2.Visible = false;
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
            panel2.Visible = false;
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            state = 8;
            LoadDataIntoDataGridView();
            panel2.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            state = 9;
            LoadDataIntoDataGridView();
            panel2.Visible = false;
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
                            Form3 form3 = new Form3();
                            form3.ShowDialog();
                            panel2.Visible = false;
                        }
                        else if (state == 2)
                        {
                            Form4 form4 = new Form4();
                            form4.ShowDialog();
                            panel2.Visible = false;
                        }
                        else if (state == 3)
                        {
                            Form5 form5 = new Form5();
                            form5.ShowDialog();
                            panel2.Visible = false;
                        }
                        else if (state == 4)
                        {
                            Form6 form6 = new Form6();
                            form6.ShowDialog();
                            panel2.Visible = false;
                        }
                        else if (state == 5)
                        {
                            Form7 form7 = new Form7();
                            form7.ShowDialog();
                            panel2.Visible = false;
                        }
                        else if (state == 6)
                        {
                            Form11 form11 = new Form11();
                            form11.ShowDialog();
                            panel2.Visible = false;
                        }
                        else if (state == 7)
                        {
                            Form8 form8 = new Form8();
                            form8.ShowDialog();
                            panel2.Visible = false;
                        }
                        else if (state == 8)
                        {
                            Form9 form9 = new Form9();
                            form9.ShowDialog();
                            panel2.Visible = false;
                        }
                        else if (state == 9)
                        {
                            Form10 form10 = new Form10();
                            form10.ShowDialog();
                            panel2.Visible = false;
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

        }
    }
}
