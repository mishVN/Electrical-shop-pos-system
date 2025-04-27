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
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Seya
{
    public partial class cheque : Form
    {
        string connstring = "Server=localhost;Database=Seya;User Id=sa;Password=3323;";
        SqlConnection conn;

        public cheque()
        {
            InitializeComponent();
            conn = new SqlConnection(connstring);
            LoadDataIntoDataGridViewincome();
            LoadDataIntoDataGridViewout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();

            
            if (selectedValue == "INCOME")
            {
                if(txtchequenumber.Text==""||
                    txtamount.Text==""||
                    txtcompany.Text==""||
                    dateTimePickergive.Text==""||
                    dateTimePickerpass.Text == "")
                {
                    MessageBox.Show("Fill Blanks", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connstring))
                        {
                            conn.Open();
                            string insert = @"INSERT INTO cheque(cheque_number,customer_nic,amount,receved_date,passing_date) VALUES(@cheque_number,@customer_name,@amount,@receved_date,@passing_date)";
                            using (SqlCommand cmd = new SqlCommand(insert, conn))
                            {
                                if (double.TryParse(txtchequenumber.Text.Trim(), out double number))
                                {
                                    cmd.Parameters.AddWithValue("@cheque_number", number);
                                }
                                else
                                {
                                    MessageBox.Show("Invalid cheque Number. Please enter a valid number.");
                                    return;
                                }

                                cmd.Parameters.AddWithValue("@customer_name", txtcompany.Text.Trim());
                                cmd.Parameters.AddWithValue("@amount", txtamount.Text.Trim());
                                cmd.Parameters.AddWithValue("@receved_date", dateTimePickergive.Value.Date);
                                cmd.Parameters.AddWithValue("@passing_date", dateTimePickerpass.Value.Date);

                                int row = cmd.ExecuteNonQuery();
                                if (row > 0)
                                {
                                    MessageBox.Show("Cheque successfully added to the database.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadDataIntoDataGridViewincome();
                                }
                                else
                                {
                                    MessageBox.Show("No rows were inserted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }

                }
            }
            else if (selectedValue == "OUTGOING")
            {
                if (txtchequenumber.Text == "" ||
                    txtamount.Text == "" ||
                    txtcompany.Text == "" ||
                    dateTimePickergive.Text == "" ||
                    dateTimePickerpass.Text == "")
                {
                    MessageBox.Show("Fill Blanks", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connstring))
                        {
                            conn.Open();
                            string insert = @"INSERT INTO outgoing_cheque(Cheque_number,Company_name,Amount,Date,Passing_date) VALUES(@Cheque_number,@Company_name,@Amount,@Date,@Passing_date)";
                            using (SqlCommand cmd = new SqlCommand(insert, conn))
                            {
                                if (double.TryParse(txtchequenumber.Text.Trim(), out double number))
                                {
                                    cmd.Parameters.AddWithValue("@Cheque_number", number);
                                }
                                else
                                {
                                    MessageBox.Show("Invalid cheque Number. Please enter a valid number.");
                                    return;
                                }

                                cmd.Parameters.AddWithValue("@Company_name", txtcompany.Text.Trim());
                                cmd.Parameters.AddWithValue("@Amount", txtamount.Text.Trim());
                                cmd.Parameters.AddWithValue("@Date", dateTimePickergive.Value.Date);
                                cmd.Parameters.AddWithValue("@Passing_date", dateTimePickerpass.Value.Date);

                                int row = cmd.ExecuteNonQuery();
                                if (row > 0)
                                {
                                    MessageBox.Show("Cheque successfully added to the database.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadDataIntoDataGridViewout();
                                }
                                else
                                {
                                    MessageBox.Show("No rows were inserted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }

                }
            }
        }

        private void LoadDataIntoDataGridViewincome()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    // Open the connection
                    connection.Open();

                    // Query to select all data from a table (replace with your table name)
                    string query = "SELECT * FROM cheque";

                    // Use SqlDataAdapter to fetch data
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Fill a DataTable with the fetched data
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridViewincome.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the connection or query
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void LoadDataIntoDataGridViewout()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    // Open the connection
                    connection.Open();

                    // Query to select all data from a table (replace with your table name)
                    string query = "SELECT * FROM outgoing_cheque";

                    // Use SqlDataAdapter to fetch data
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Fill a DataTable with the fetched data
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Bind the DataTable to the DataGridView
                    dataGridViewoutgoin.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the connection or query
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    // Open the connection
                    connection.Open();

                    // Query with parameters to filter by passing_date
                    string query = "SELECT * FROM cheque WHERE passing_date BETWEEN @StartDate AND @EndDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@StartDate", dateTimePickerstartincome.Value.Date);
                        command.Parameters.AddWithValue("@EndDate", dateTimePickerendincome.Value.Date);

                        // Use SqlDataAdapter to fetch data
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridViewincome.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the connection or query
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connstring))
                {
                    // Open the connection
                    connection.Open();

                    // Query with parameters to filter by passing_date
                    string query = "SELECT * FROM outgoing_cheque WHERE passing_date BETWEEN @StartDate AND @EndDate";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@StartDate", dateTimePickerstartout.Value.Date);
                        command.Parameters.AddWithValue("@EndDate", dateTimePickerendout.Value.Date);

                        // Use SqlDataAdapter to fetch data
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridViewoutgoin.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the connection or query
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem.ToString();

            // Perform the filtering based on the selected value
            if (selectedValue == "OUTGOING")
            {
                if (txtchequenumber.Text == "")
                {
                    MessageBox.Show("Fill Blanks","Information Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM outgoing_cheque WHERE cheque_number = @Id";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            // Add parameter to prevent SQL injection
                            command.Parameters.AddWithValue("@Id", txtchequenumber.Text);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("SuccessFull ", "Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataIntoDataGridViewout();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                
            }
            else if (selectedValue == "INCOME")
            {
                if (txtchequenumber.Text == "")
                {
                    MessageBox.Show("Fill Blanks", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM cheque WHERE cheque_number = @Id";

                        using (SqlCommand command = new SqlCommand(query, conn))
                        {
                            // Add parameter to prevent SQL injection
                            command.Parameters.AddWithValue("@Id", txtchequenumber.Text);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("SuccessFull ", "Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataIntoDataGridViewincome();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void txtchequenumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();

                if (selectedValue == "OUTGOING")
                {
                    if (txtchequenumber.Text == "")
                    {
                        MessageBox.Show("Fill Blanks","Information Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(connstring))
                            {
                                conn.Open();

                                string fetchQuery = @"SELECT * FROM outgoing_cheque WHERE cheque_number=@cheque_number";

                                using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                                {
                                    if (double.TryParse(txtchequenumber.Text.Trim(), out double id))
                                    {

                                        cmd.Parameters.AddWithValue("@cheque_number", id);

                                        using (SqlDataReader reader = cmd.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                txtcompany.Text = reader["company_name"] == DBNull.Value ? string.Empty : reader["company_name"].ToString();
                                                txtamount.Text = reader["amount"] == DBNull.Value ? string.Empty : reader["amount"].ToString();
                                               

                                            }
                                            else
                                            {
                                                MessageBox.Show("No item found with the specified code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid item code. Please enter a valid number.");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (selectedValue == "INCOME")
                {
                    if (txtchequenumber.Text == "")
                    {
                        MessageBox.Show("Fill Blanks", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(connstring))
                            {
                                conn.Open();

                                string fetchQuery = @"SELECT * FROM cheque WHERE cheque_number=@cheque_number";

                                using (SqlCommand cmd = new SqlCommand(fetchQuery, conn))
                                {
                                    if (double.TryParse(txtchequenumber.Text.Trim(), out double id))
                                    {

                                        cmd.Parameters.AddWithValue("@cheque_number", id);

                                        using (SqlDataReader reader = cmd.ExecuteReader())
                                        {
                                            if (reader.Read())
                                            {
                                                txtcompany.Text = reader["customer_nic"] == DBNull.Value ? string.Empty : reader["customer_nic"].ToString();
                                                txtamount.Text = reader["amount"] == DBNull.Value ? string.Empty : reader["amount"].ToString();


                                            }
                                            else
                                            {
                                                MessageBox.Show("No item found with the specified code.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid item code. Please enter a valid number.");
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtamount.Clear();
            txtchequenumber.Clear();
            txtcompany.Clear();

        }

        private void txtcompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtamount.Clear();
            }
        }

        private void dataGridViewincome_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dataGridViewincome_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked row index is valid
                if (e.RowIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridViewincome.Rows[e.RowIndex];

                    // Get the item_code from the selected row and fill txtcode
                    if (selectedRow.Cells["cheque_number"].Value != null)
                    {
                        txtchequenumber.Text = selectedRow.Cells["cheque_number"].Value.ToString();
                        txtchequenumber.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewoutgoin_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Check if the clicked row index is valid
                if (e.RowIndex >= 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridViewoutgoin.Rows[e.RowIndex];

                    // Get the item_code from the selected row and fill txtcode
                    if (selectedRow.Cells["cheque_number"].Value != null)
                    {
                        txtchequenumber.Text = selectedRow.Cells["cheque_number"].Value.ToString();
                        txtchequenumber.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
    

}
