﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;

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
            CheckUserRoles();
        }

        private void CheckUserRoles()
        {
            if (Login.roles != "Admin")
            {
                delete.Enabled = false;
                update.Enabled = false;
            }
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

        public async Task UpdateChangesToDatabase()
        {
            DataTable dataTableToUpdate = new DataTable();
            string tableName = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter())
                {
                    MySqlCommandBuilder commandBuilder;

                    switch (state)
                    {
                        case 1:
                            tableName = "customers";
                            break;
                        case 2:
                            tableName = "employee";
                            break;
                        case 3:
                            tableName = "sales_order";
                            break;
                        case 4:
                            tableName = "payment";
                            break;
                        case 5:
                            tableName = "purchase_order";
                            break;
                        case 6:
                            tableName = "inventory";
                            break;
                        case 7:
                            tableName = "order_line_item";
                            break;
                        case 8:
                            tableName = "sales_line_item";
                            break;
                        case 9:
                            tableName = "product_and_services";
                            break;
                        default:
                            break;
                    }

                    dataAdapter.SelectCommand = new MySqlCommand($"SELECT * FROM {tableName}", connection);
                    dataTableToUpdate = ((DataTable)dataGridView1.DataSource).GetChanges();

                    if (dataTableToUpdate != null)
                    {
                        commandBuilder = new MySqlCommandBuilder(dataAdapter);
                        dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                        dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                        dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                        try
                        {
                            await connection.OpenAsync();

                            // Start a transaction
                            using (MySqlTransaction transaction = connection.BeginTransaction())
                            {
                                dataAdapter.SelectCommand.Transaction = transaction;
                                dataAdapter.InsertCommand.Transaction = transaction;
                                dataAdapter.UpdateCommand.Transaction = transaction;
                                dataAdapter.DeleteCommand.Transaction = transaction;
                                dataAdapter.Update(dataTableToUpdate);
                                transaction.Commit();
                                dataGridView1.DataSource = null;
                                LoadDataIntoDataGridView();
                            }
                        }
                        catch (DBConcurrencyException ex)
                        {
                            MessageBox.Show("Concurrency conflict occurred. Please avoid simultaneous update and try again.", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            dataTableToUpdate.RejectChanges();
                            LoadDataIntoDataGridView();
                        }
                    }
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
                        if (dynaSearch.Checked)
                        {
                            string searchQuery = "SELECT customer_id FROM customers WHERE name LIKE @keyword";
                            using (MySqlCommand cmd = new MySqlCommand(searchQuery, connection))
                            {
                                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                                using (MySqlDataReader reader = cmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        int id = reader.GetInt32("customer_id");
                                        query = $"SELECT * FROM customers LEFT JOIN sales_order ON customers.customer_id = sales_order.customer_id " +
                                            $"LEFT JOIN payment ON customers.customer_id = payment.customer_id WHERE customers.name LIKE @keyword OR customers.customer_id = '{id}'";
                                    }
                                }
                            } 
                        }
                        else
                        {
                            query = $"SELECT * FROM customers WHERE name LIKE @keyword";
                        }
                    }
                    else if (state == 2)
                    {
                        if (dynaSearch.Checked)
                        {
                            query = $"SELECT * FROM employee LEFT JOIN purchase_order ON employee.employee_id = purchase_order.employee_id WHERE employee.name LIKE @keyword";
                        }
                        else
                        {
                            query = $"SELECT * FROM employee WHERE name LIKE @keyword";
                        }
                    }
                    else if (state == 3)
                    {
                        if (dynaSearch.Checked)
                        {
                            query = $" SELECT * FROM sales_order LEFT JOIN sales_line_item ON sales_order.sales_order_id = sales_line_item.sales_order_id " +
                                $"LEFT JOIN customers ON customers.customer_id = sales_order.customer_id " +
                                $"LEFT JOIN product_and_services ON product_and_services.ps_id = sales_order.ps_id WHERE customers.customer_id LIKE @keyword OR customers.name LIKE @keyword";
                        }
                        else
                        {
                            query = "SELECT * FROM sales_order WHERE customer_id LIKE @keyword";
                        }
                    }
                    else if (state == 4)
                    {
                        if (dynaSearch.Checked)
                        {
                            query = "SELECT * FROM payment LEFT JOIN customers ON payment.customer_id = customers.customer_id WHERE customers.name LIKE @keyword OR payment.amount LIKE @keyword OR payment.customer_id LIKE @keyword";
                        }
                        else
                        {
                            query = $"SELECT * FROM payment WHERE customer_id LIKE @keyword OR amount LIKE @keyword";
                        }
                    }
                    else if (state == 5)
                    {
                        if (dynaSearch.Checked)
                        {
                            query = $"SELECT * FROM purchase_order LEFT JOIN employee ON purchase_order.employee_id = employee.employee_id WHERE employee.name LIKE @keyword OR employee.employee_id LIKE @keyword";
                        }
                        else {
                            query = $"SELECT * FROM purchase_order WHERE employee_id LIKE @keyword";
                        }
                    }
                    else if (state == 6)
                    {
                        query = $"SELECT * FROM inventory WHERE name LIKE @keyword";
                    }
                    else if (state == 7)
                    {
                        if (dynaSearch.Checked)
                        {
                            query = "SELECT * FROM order_line_item LEFT JOIN inventory ON order_line_item.product_id = inventory.product_id " +
                                "LEFT JOIN purchase_order ON order_line_item.purchase_order_id = purchase_order.purchase_order_id " +
                                "WHERE order_line_item.product_id LIKE @keyword OR inventory.name LIKE @keyword OR order_line_item.purchase_order_id LIKE @keyword";
                        }
                        else
                        {
                            query = $"SELECT * FROM order_line_item WHERE product_id LIKE @keyword OR purchase_order_id LIKE @keyword";
                        }
                    }
                    else if (state == 8)
                    {
                        if (dynaSearch.Checked)
                        {
                            query = $"SELECT * FROM sales_order LEFT JOIN sales_line_item ON sales_order.sales_order_id = sales_line_item.sales_order_id WHERE sales_line_item.sales_order_id LIKE @keyword OR sales_line_item.quantity LIKE @keyword";
                        }
                        else
                        {
                            query = "SELECT * FROM sales_line_item WHERE sales_order_id LIKE @keyword OR quantity LIKE @keyword";
                        }
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
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void dynaSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (dynaSearch.Checked)
            {
                delete.Enabled = false;
                update.Enabled = false;
                dataGridView1.ReadOnly = true;
                dataGridView1.KeyDown += dataGridView1_KeyDown;
            }
            else
            {
                delete.Enabled = true;
                update.Enabled = true;
                dataGridView1.ReadOnly = false;
                dataGridView1.KeyDown -= dataGridView1_KeyDown;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true; 
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Saving Changes will overwrite existing data. Are you sure to save changes?", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                UpdateChangesToDatabase();
            }
            LoadDataIntoDataGridView();
        }

        private void generate_Click(object sender, EventArgs e)
        {
            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                    for (int i = 1; i <= dataGridView1.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i].Value = dataGridView1.Columns[i - 1].HeaderText;
                    }

                
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = dataGridView1.Rows[i].Cells[j].Value;
                        }
                    }

                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel files (*.xlsx)|*.xlsx",
                        Title = "Save Excel File",
                        FileName = "YourFileName.xlsx" 
                    };

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        excelPackage.SaveAs(new System.IO.FileInfo(saveFileDialog.FileName));
                        MessageBox.Show("Export successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting to Excel: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            Delete deleteModal = new Delete();
            deleteModal.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void logout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string filePath = "login.txt";
                try
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        fileStream.SetLength(0);
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}

